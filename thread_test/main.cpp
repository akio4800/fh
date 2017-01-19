#include<vector>
#include<iostream>
#include<thread>
#include<string>
#include <map>
#include "pfc_complex.h"
#include "pfc_rgb_from_wavelength.h"
#include "pfc_bitmap.h"
#include <Windows.h>



enum mode {
	gpls,
	gslp,
	single,
	full


};

std::vector<pfc::bitmap::pixel_t> color_map(int it) {

	std::vector<pfc::bitmap::pixel_t> v;

	double w = 1.0 / it;
	double x = 1;
	for (size_t i = 0; i < it + 1; i++)
	{
		x -= w;

		pfc::bitmap::pixel_t t;

		pfc::rgb_from_wavelength(t, x);

		v.push_back(t);

	}


	return v;

};

int point(pfc::complex<> c, int it, int d) {

	pfc::complex<> start = 0;

	pfc::complex<> e = start;

	int i = 0;

	do
	{
		e = pfc::square(e) + c;
	} while ((++i < it) && std::sqrt(pfc::norm(e) < d));


	return i;
};


pfc::complex<> mapping(int sizex, int sizey, int x, int y, pfc::complex<> c1, pfc::complex<> c2) {

	double isize = abs(c1.imag - c2.imag);
	double rsize = abs(c1.real - c2.real);

	double ix = x*(rsize / sizex) + c1.real;
	double iy = y*(isize / sizey) + c1.imag;


	return pfc::complex<>(ix, iy);




};



void mandelbrot_main(int xsize, int ysize, int it, int d) {

std::vector<pfc::bitmap::pixel_t>colormap = color_map(it);
pfc::bitmap bmp(xsize,ysize);

for (size_t row = 0; row < xsize; row++)
{
	for (size_t col = 0; col < ysize; col++)
	{

		int p = point(mapping(xsize, ysize, col, row, pfc::complex<>(-0.55, -0.55), pfc::complex<>(-0.54, -0.54)),it,d);

		pfc::bitmap::pixel_t rgb = colormap.at(p);

		int val = row * xsize + col;
		//std::cout << val << std::endl;

		bmp.get_pixels()[val] = rgb;

		

	}
	std::cout << row << std::endl;
}


bmp.to_file("p.bmp");

}













	









int main() {
	mandelbrot_main(200,200,200,10);
/*

	SYSTEM_INFO sysinfo;
	GetSystemInfo(&sysinfo);
	int numCPU = sysinfo.dwNumberOfProcessors;


	std::cout <<"Cores: "<< numCPU  <<  std::endl;


	std::vector<std::thread> group;

	for (int i = 0; i < numCPU; i++)
	{
		group.emplace_back([] {

			int j = 0;
			for (size_t k = 0; k < 50000; k++)
			{
				j += rand();
			}
		});
	};

	for (auto &i : group)
	{
		i.join();
	}
	*/


}