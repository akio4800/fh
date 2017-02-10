#include<vector>
#include<iostream>
#include<thread>
#include<string>
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


enum mode {
	gpls,
	gslp,
	single,
	full


};

bmpdata color_map(int it) {

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


	return v;

};



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


pfc::complex<> mapping(picturesize p, int x, int y, area a) {

	double isize = abs(a.first.imag - a.second.imag);
	double rsize = abs(a.first.real - a.second.real);

	double ix = x*(rsize / p.first) + a.first.real;
	double iy = y*(isize / p.second) + a.first.imag;


	return pfc::complex<>(ix, iy);




};


bmpdata mandelbrot_serial(picturesize size, int it, size_t d, area a, bmpdata map) {


	bmpdata vec;

	for (int row = 0; row < size.second; row++)
	{
		for (int col = 0; col < size.first; col++)
		{

			int p = point(mapping(size, col, row, a), it, d);

			pfc::bitmap::pixel_t rgb = map.at(p);

			vec.push_back(rgb);

			int val = row * size.first + col;
			//std::cout << val << std::endl;
		



			
		}
	}
		
	return vec;


}


void write_bmp(pfc::bitmap bmp, bmpdata data, std::string filename) {


	for (int i = 0; i < data.size(); i++)
	{
		bmp.get_pixels()[i] = data.at(i);
	}



	bmp.to_file(filename);


}



void mandelbrot_paralell(mode m, size_t num, int it, size_t d, points p, picturesize size) {
	bmpdata map = color_map(it);

	std::vector<std::thread> group;
	switch (m)
	{
	case gpls:


		for (int i = 0; i < num; i++)
		{

			if (num == p.size()) {

				group.emplace_back([&]() {

					bmpdata data = mandelbrot_serial(size, it, d, p.at(i), map);
					pfc::bitmap bmp{ size.first,size.second };


					std::string filename = "gpls_" + std::to_string(i) + ".bmp";

					write_bmp(bmp, data, filename);

				});
			}
		}

		for (auto &t : group)
		{
			t.join();
		}

		break;
	case gslp:
		for (int j = 0; j < p.size(); j++)
		{

			area a = p.at(j);




			bmpdata data;

			datamap dm;

			std::vector<area> vec;


			double dist = abs(a.first.real - a.second.real) / num;

			pfc::complex<> c1 = a.first;
			pfc::complex<> c2 = a.second;
			area curr;
			curr.first = c1;
			curr.second = c2;
			curr.second.imag = curr.first.imag + dist;


			for (size_t i = 0; i < num; i++)
			{
				vec.push_back(curr);

				curr.first.imag += dist;
				curr.second.imag += dist;

			}
			size.first /= num;
			for (int k = 0; k < num; k++)
			{

				{

					group.emplace_back([&]() {




						pfc::bitmap bmp(size.first, size.second);
						data = mandelbrot_serial(size, it, d, vec.at(k - 1), map);


						std::string filename = "gslp_" + std::to_string(j) + std::to_string(k) + ".bmp";

						write_bmp(bmp, data, filename);

					});
				}

			}
			for (auto &t : group)
			{
				t.join();
			}

		}
		break;
	case single:

		for (int o = 0; o < num; o++)
		{
			bmpdata data = mandelbrot_serial(size, it, d, p.at(o), map);

			pfc::bitmap bmp{ size.first,size.second };


			std::string filename = "serial_" + std::to_string(o) + ".bmp";

			write_bmp(bmp, data, filename);

		}

		break;
	case full:
		break;
	default:
		break;
	}







}










int main() {



	SYSTEM_INFO sysinfo;
	GetSystemInfo(&sysinfo);
	int numCPU = sysinfo.dwNumberOfProcessors;

	picturesize size{ 5000,5000 };
	area a{ pfc::complex<>{-2,-2},pfc::complex<>{2,2} };
	std::cout << "Cores: " << numCPU << std::endl;

	points p{ a };



	mandelbrot_paralell(single, 1, 200, 4, p, size);




}