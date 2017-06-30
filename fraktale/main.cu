#include <cuda_runtime.h>
#include <device_launch_parameters.h>
#include "pfc_cuda_device_info.h"
#include "pfc_cuda_memory.h"
#include "pfc_random.h"
#include "pfc_timed_run.h"
#include <iostream>
#include <string>
#include <map>
#include "pfc_complex.h"
#include "pfc_rgb_from_wavelength.h"
#include "pfc_bitmap.h"
#include <Windows.h>


typedef std::vector<pfc::bitmap::pixel_t> bmpdata;
typedef std::pair <pfc::complex<>, pfc::complex<>> area;
typedef std::vector<area> points;
typedef std::pair<int, int> picturesize;
typedef std::map<int, bmpdata> datamap;

using namespace std::literals;

__constant__ auto const g_block_size = 128;
__constant__ auto const g_x = 5000;
__constant__ auto const g_y = 5000;
__constant__ auto const d = 4;
__constant__ auto const it = 100;
__constant__ area a = std::pair <pfc::complex<>, pfc::complex<>>((-2, 2), (2, 2));
__constant__ picturesize psize = std::pair<int, int>(g_x, g_y);











dim3 grid_size(dim3 const & block, int3 const & size) {

	dim3 s;
	s.x = (size.x + block.x - 1) / block.x;
	s.y = (size.y + block.y - 1) / block.y;
	s.z = (size.z + block.z - 1) / block.z;

	return s;


}



auto const g_grid_size = grid_size(
	g_block_size, { g_x , g_y, 1 }
);


__host__  __device__   __forceinline__
float norm(float3 const & p, float3 const & q) {
	auto x = p.x - q.x;
	auto y = p.y - q.y;
	auto z = p.z - q.z;

	return x*x + y*y + z*z;

}

__device__ int global_thread_idx_x() {

	return blockIdx.x * blockDim.x + threadIdx.x;


}
__device__ int global_thread_idx_y() {

	return blockIdx.y * blockDim.y + threadIdx.y;


}


void color_map(int it, pfc::bitmap::pixel_t *  hp_colors) {

	bmpdata v;

	double w = 1.0 / it;
	double x = 0;
	for (size_t i = 0; i < it + 1; i++)
	{
		x += w;

		pfc::bitmap::pixel_t t;

		pfc::rgb_from_wavelength(t, x);

		v.push_back(t);

	}

	hp_colors = v.data();


	

};


__device__ __forceinline__
int point(pfc::complex<> c, int it, size_t d) {

	pfc::complex<> start = 0;

	pfc::complex<> e = start;

	int i = 0;

	do
	{
		e = pfc::square(e) + c;
	} while ((++i < it) && pfc::norm(e) < d);


	return i;
};

__device__ __forceinline__
pfc::complex<> mapping(picturesize p, int x, int y, area a) {

	double isize = abs(a.first.imag - a.second.imag);
	double rsize = abs(a.first.real - a.second.real);

	double ix = x*(rsize / p.first) + a.first.real;
	double iy = y*(isize / p.second) + a.first.imag;


	return pfc::complex<>(ix, iy);




};


__global__ void fraktal_GPU(pfc::bitmap::pixel_t * const dp_colors, pfc::bitmap::pixel_t * const dp_picture) {

	int x = global_thread_idx_x();
	int y = global_thread_idx_y();


	if (x < g_x && y < g_y) {

		int p = point(mapping(psize, x, y, a), it, d);

		pfc::bitmap::pixel_t rgb = dp_colors[p];
		int val = x * g_x + y;

		dp_picture[val] = rgb;

	}


}


void allocate_memory(
	pfc::bitmap::pixel_t * & hp_colors, pfc::bitmap::pixel_t * & hp_picture,
	pfc::bitmap::pixel_t * & dp_colors,
	pfc::bitmap::pixel_t *& dp_picture){

	hp_colors = new pfc::bitmap::pixel_t[it] {};
	hp_picture = new  pfc::bitmap::pixel_t[g_x*g_y] {};
	
	

	dp_colors = PFC_CUDA_MALLOC(pfc::bitmap::pixel_t, it);
	dp_picture = PFC_CUDA_MALLOC(pfc::bitmap::pixel_t, g_x*g_y);

	double m = (((2.0 * sizeof(int)*it + sizeof(pfc::bitmap::pixel_t))*g_x*g_y) / 1024.0) / 1024.0;
	std::cout << "Memory allocated : " << m <<" Mib"<< std::endl;
}

void free_memory(
	pfc::bitmap::pixel_t * & hp_colors, pfc::bitmap::pixel_t * & hp_picture,
	pfc::bitmap::pixel_t * & dp_colors,
	pfc::bitmap::pixel_t *& dp_picture
) {

	PFC_CUDA_FREE(dp_colors);
	PFC_CUDA_FREE(dp_picture);

	delete[] hp_colors; hp_colors = nullptr;
	delete[] hp_picture; hp_picture = nullptr;
	double m = (((2.0 * sizeof(pfc::bitmap::pixel_t)*it + 2.0 * (sizeof(pfc::bitmap::pixel_t))*g_x*g_y) / 1024.0) / 1024.0;
	std::cout << "Memory freed : " << m << " Mib" << std::endl;


}



int main() {

	try {
		int count = 0;
		PFC_CUDA_CHECK(cudaGetDeviceCount(&count));
		if (count > 0)
		{
			PFC_CUDA_CHECK(cudaSetDevice(0));

			auto deviceinfo = pfc::cuda::get_device_info();
			auto deviceprops = pfc::cuda::get_device_props();

			std::cout << "Name: " << deviceprops.name << "\ncc: " << deviceinfo.cc_major << "." << deviceinfo.cc_minor << " \nArch: " << deviceinfo.uarch << std::endl;

			std::cout << "Points: " << g_x*g_y << std::endl;
			std::cout << "Threads: " << g_block_size << std::endl;
			std::cout << "Blocks: " << g_grid_size.x << std::endl;



			pfc::bitmap::pixel_t *  hp_colors = nullptr;
			pfc::bitmap::pixel_t *  hp_picture = nullptr;
			pfc::bitmap::pixel_t * dp_colors = nullptr;
			pfc::bitmap::pixel_t *  dp_picture = nullptr;
		

			color_map(it,hp_colors);

			std::cout << "allocating memory:" << std::endl;
			allocate_memory(hp_colors, hp_picture , dp_colors, dp_picture);
		


			std::cout << "GPU" << std::endl;
			std::cout << "coping to device ("<< sizeof(int)*it/1024.0/1024.0 <<" Mib)" << std::endl;
			auto const duration_gpu = pfc::timed_run([&] {

				
				PFC_CUDA_MEMCPY(dp_colors, hp_colors, it, cudaMemcpyHostToDevice);

				fraktal_GPU <<<g_grid_size,g_block_size >>> (dp_colors, dp_picture);

				
				PFC_CUDA_MEMCPY(hp_picture, dp_picture, g_x*g_y, cudaMemcpyDeviceToHost);
				PFC_CUDA_CHECK(cudaDeviceSynchronize());
				PFC_CUDA_CHECK(cudaGetLastError());

			});

			std::cout << "coping to host (" << sizeof(pfc::bitmap::pixel_t)*g_x*g_y / 1024.0 / 1024.0 << " Mib)" << std::endl;

			auto gpu_time =  std::chrono::duration_cast<std::chrono::milliseconds>(duration_gpu).count();

		
			
/*
			for (size_t i = 0; i < g_points; i++)
			{
				std::cout << hp_indices_d[i] << "-" << hp_indices_h[i] << std::endl;
			}

			*/
	free_memory(hp_colors, hp_picture, dp_colors, dp_picture);
	std::cout <<" GPU Time: " << gpu_time  << std::endl;

	pfc::bitmap bmp;

	bmp.get_pixels = hp_picture;

	bmp.to_file("fraktal.bmp");

		}



	}
	catch (std::exception const & x) {
		std::cerr << "ERROR: " << x.what() << std::endl;

	}

	cudaDeviceReset();

}