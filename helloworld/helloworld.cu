#include <cuda_runtime.h>
#include <device_launch_parameters.h>
#include "pfc_cuda_device_info.h"
#include "pfc_cuda_memory.h"
#include "pfc_random.h"
#include "pfc_timed_run.h"
#include <iostream>
#include <string>


using namespace std::literals;

__constant__ auto const g_block_size = 128;
__constant__ auto const g_points = 50000;

dim3 grid_size(dim3 const & block, int3 const & size) {

	dim3 s;
	s.x = (size.x + block.x - 1) / block.x;
	s.y = (size.y + block.y - 1) / block.y;
	s.z = (size.z + block.z - 1) / block.z;

	return s;


}



auto const g_grid_size = grid_size(
	g_block_size, { g_points , 1, 1 }
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




__host__ __device__ __forceinline__
int find_closest(float3 * p_points, float3 const * p_point) {
	int index = -1;
	float min_so_far = FLT_MAX;

	for (int to = 0; to < g_points; ++p_points, ++to) {
		if (p_points != p_point) {
			auto const dist = norm(*p_point, *p_points);

			if (dist < min_so_far) {
				min_so_far = dist; index = to;

			}

		}

	}

	return index;

}


void generate_points(float3 * const hp_points)
{




	for (size_t i = 0; i < g_points; i++)
	{
		hp_points[i].x = pfc::get_random_uniform(0, 5000)*1.0f;
		hp_points[i].y = pfc::get_random_uniform(0, 5000)*1.0f;
		hp_points[i].z = pfc::get_random_uniform(0, 5000)*1.0f;
	}
}


__global__ void find_all_closest_GPU(
	float3 * const dp_points, int * const dp_indices
) {
	auto const from = global_thread_idx_x();

	if (from < g_points)
	{
		dp_indices[from] = find_closest(dp_points, dp_points + from);
	}



}

void find_all_closest_CPU(
	float3 * const hp_points, int * const hp_indices_h
) {


	for (size_t i = 0; i < g_points - 1; i++)
	{
		hp_indices_h[i] = find_closest(hp_points, hp_points + i);
	}


}










void allocate_memory(
	int * & hp_indices_d, int * & hp_indices_h,
	float3 * & hp_points, int * & dp_indices,
	float3 * & dp_points
) {

	hp_indices_d = new int[g_points] {};
	hp_indices_h = new int[g_points] {};
	hp_points = new float3[g_points]{};


	dp_indices = PFC_CUDA_MALLOC(int, g_points);
	dp_points = PFC_CUDA_MALLOC(float3, g_points);

}

void free_memory(
	int * & hp_indices_d, int * & hp_indices_h,
	float3 * & hp_points, int * & dp_indices,
	float3 * & dp_points
) {

	PFC_CUDA_FREE(dp_points);
	PFC_CUDA_FREE(dp_indices);

	delete[] hp_points; hp_points = nullptr;
	delete[] hp_indices_h; hp_indices_h = nullptr;
	delete[] hp_indices_d; hp_indices_d = nullptr;




}


/*
__global__ void copy_string_kernel(char * dp_dest, char * dp_src, int size) {
	int const t = blockIdx.x * blockDim.x + threadIdx.x;

	if (t < size)
	{
		dp_dest[t] = dp_src[t];
	}

}
*/


int main() {

	try {
		int count = 0;
		PFC_CUDA_CHECK(cudaGetDeviceCount(&count));
		if (count > 0)
		{
			PFC_CUDA_CHECK(cudaSetDevice(0));

			auto deviceinfo = pfc::cuda::get_device_info();
			auto deviceprops = pfc::cuda::get_device_props();

			std::cout << "name: " << deviceprops.name << "\ncc: " << deviceinfo.cc_major << "." << deviceinfo.cc_minor << " \narch: " << deviceinfo.uarch << std::endl;


			int *  hp_indices_d = nullptr;
			int *  hp_indices_h = nullptr;
			float3 *  hp_points = nullptr;
			int * dp_indices = nullptr;
			float3 *  dp_points = nullptr;




			allocate_memory(hp_indices_d, hp_indices_h, hp_points, dp_indices, dp_points);

			generate_points(hp_points);

			std::cout << "CPU" << std::endl;


			auto const duration_cpu = pfc::timed_run([&] {

				find_all_closest_CPU(hp_points, hp_indices_h);
			});

			auto cpu_time = std::chrono::duration_cast<std::chrono::milliseconds>(duration_cpu).count();
			


			std::cout << "GPU" << std::endl;
			auto const duration_gpu = pfc::timed_run([&] {

				PFC_CUDA_MEMCPY(dp_points, hp_points, g_points, cudaMemcpyHostToDevice);

				//int const big = (g_points + g_block_size + -1) / g_block_size;


				find_all_closest_GPU << <g_block_size,g_grid_size >> > (dp_points, dp_indices);

				//copy_string_kernel << <big, tib >> > (dp_dest, dp_src, size);

				
				PFC_CUDA_MEMCPY(hp_indices_d, dp_indices, g_points, cudaMemcpyDeviceToHost);
				PFC_CUDA_CHECK(cudaDeviceSynchronize());
				PFC_CUDA_CHECK(cudaGetLastError());

			});

			auto gpu_time =  std::chrono::duration_cast<std::chrono::milliseconds>(duration_gpu).count();

			std::cout << "CPU Time: " << cpu_time << " GPU Time: " << gpu_time << " Speedup: " << (cpu_time*1.0f) / (gpu_time*1.0f) << std::endl;
			

		

			free_memory(hp_indices_d, hp_indices_h, hp_points, dp_indices, dp_points);

		}



	}
	catch (std::exception const & x) {
		std::cerr << "ERROR: " << x.what() << std::endl;

	}

	cudaDeviceReset();

}