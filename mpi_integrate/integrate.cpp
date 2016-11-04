#include "header\tools.h"


namespace fn {
	typedef  std::function<double(double)> integratee_t;
	typedef  std::function<double(integratee_t, double, double)> integrator_t;
	double x2(double x) { return 2 * x; };
	double x_2(double x) { return x*x; };
	double sinf(double x) { return sin(x); }
	double cosf(double x) { return cos(x); }
	double x_3(double x) { return x*x*x; };
	double poly(double x) { return 2 * (x*x*x) + 3 * (x*x) - 2 * x + 3; }







	double integrate_serial(integratee_t func_integratee, integrator_t func_integrate, double start, double end, int num) {

		double thick = abs(start - end) / num;
		double istart = start;
		double iend = start + thick;

		double sum = 0;

		for (size_t i = 0; i < num; i++)
		{
			sum += func_integrate(func_integratee, istart, iend);
			istart = iend;
			iend = istart + thick;

		}





		return sum;
	}
	int getparts(int nodes, int num, int part) {
		int n = num / nodes;
		int r = num % nodes;
		return r == 0 ? n : r > part ? n + 1 : n;
	}

	double integrate_simpson(integratee_t func, double start, double end) {

		return (end - start) / 6 * (func(start) + 4 * func((start + end) / 2) + func(end));



	}

	double integrate_trapez(integratee_t func, double start, double end) {



		return (end - start)*((func(start) + func(end)) / 2);

	}


	void integrate_parallel(int argc, char* argv[]) {

		std::map<int, integratee_t> funcs;
		std::map<int, integrator_t> funcs2;


		funcs.insert(std::pair<int, integratee_t>(1, x2));
		funcs.insert(std::pair<int, integratee_t>(2, x_2));
		funcs.insert(std::pair<int, integratee_t>(3, sinf));
		funcs.insert(std::pair<int, integratee_t>(4, cosf));
		funcs.insert(std::pair<int, integratee_t>(5, poly));

		funcs2.insert(std::pair<int, integrator_t>(1, integrate_simpson));
		funcs2.insert(std::pair<int, integrator_t>(2, integrate_trapez));





		try {
			mpi::check(MPI_Init(&argc, &argv));
			int rank{ -1 };
			int size{ -1 };

			char name[MPI_MAX_PROCESSOR_NAME];
			mpi::check(MPI_Comm_rank(MPI_COMM_WORLD, &rank));
			mpi::check(MPI_Comm_size(MPI_COMM_WORLD, &size));
			int name_len{};
			mpi::check(MPI_Get_processor_name(name, &name_len));



			if (size > 1) {
				if (rank == 0) {
					int num = 0;
					double start = 0;
					double end = 0;
					int parts = 0;
					int method = 0;


					std::cout << "func num: ";
					std::cin >> num;

					std::cout << "start: ";
					std::cin >> start;

					std::cout << "end: ";
					std::cin >> end;

					std::cout << "parts:";
					std::cin >> parts;

					std::cout << "method:";
					std::cin >> method;
					double thick = abs(end - start) / parts;

					double pstart = start + getparts(size, parts, 0)*thick;

					double sum = integrate_serial(funcs.at(num), funcs2.at(method), start, pstart, getparts(size, parts, 0));

					for (int i = 1; i < size; i++)
					{
						double pend = pstart + getparts(size, parts, 0)*thick;

						double params[5] = { num,pstart,pend,getparts(size, parts, i),method };
						mpi::check(MPI_Send(params, 5, MPI_DOUBLE, i, 0, MPI_COMM_WORLD));

						pstart = pend;
					}


					std::cout << sum << " ";
					MPI_Status status{};
					double buffer;

					for (int j = 1; j < size; j++)
					{
						mpi::check(MPI_Recv(&buffer, 1, MPI_DOUBLE, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
						std::cout << buffer << " ";
						sum += buffer;
					}
					std::cout << "sum:" << sum << std::endl;
				}
				else {

					MPI_Status status{};
					double buffer[5]{};
					mpi::check(MPI_Recv(buffer, 5, MPI_DOUBLE, 0, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
					for (size_t i = 0; i < 5; i++)
					{
						std::cout << buffer[i] << " ";
					}
					std::cout << std::endl;

					double a = integrate_serial(funcs.at(buffer[0]), funcs2.at(buffer[4]), buffer[1], buffer[2], buffer[3]);



					mpi::check(MPI_Send(&a, 1, MPI_DOUBLE, 0, 0, MPI_COMM_WORLD));


				}
			}
			else {
				std::cout << "more than one node requiered" << std::endl;
			}

		}
		catch (mpi::mpi_exeception &e) {
			std::cerr << e.what() << std::endl;
		};

		mpi::check(MPI_Finalize());
	}
}



int main(int argc, char* argv[]) {




	//	std::cout<<fn::integrate_serial(fn::x2, fn::integrate_trapez, -10, 10, 10);
	fn::integrate_parallel(argc, argv);


}


