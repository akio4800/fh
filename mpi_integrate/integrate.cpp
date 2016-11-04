#include "header\tools.h"


namespace fn {
	typedef  std::function<double(double)> integratee_t;
	typedef  std::function<double(integratee_t, double, double)> integrator_t;

	double x_2(double x) { return x*x; };
	double x2(double x) { return 2 * x; };
	double sinf(double x) { return sin(x); }
	double cosf(double x) { return cos(x); }
	double x_3(double x) { return x*x*x; };
	double poly(double x) { return 2 * (x*x*x) + 3 * (x*x) - 2 * x + 3; }









	//functions




	std::vector<std::string> split(std::string str, char delimiter) {
		std::vector<std::string> internal;
		std::stringstream ss(str); // Turn the string into a stream.
		std::string tok;

		while (std::getline(ss, tok, delimiter)) {
			internal.push_back(tok);
		}

		return internal;
	}






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
		funcs2.insert(std::pair<int, integrator_t>(1, integrate_trapez));





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



					for (int i = 1; i < size; i++)
					{
						double pend = pstart + getparts(size, parts, 0)*thick;


						std::string message = std::to_string(num) + ";"
							+ std::to_string(pstart) + ";"
							+ std::to_string(pend) + ";"
							+ std::to_string(getparts(size, parts, i)) + ";"
							+ std::to_string(method);
						mpi::check(MPI_Send(message.c_str(), message.size() + 1, MPI_CHAR, i, 0, MPI_COMM_WORLD));

						pstart = pend;
					}

					double sum = integrate_serial(funcs.at(num), funcs2.at(method), start, pstart, getparts(size, parts, 0));
					MPI_Status status{};
					char buffer[100]{};
					buffer[0] = '\0';
					for (int j = 1; j < size; j++)
					{
						mpi::check(MPI_Recv(buffer, sizeof(buffer) / sizeof(buffer[0]), MPI_CHAR, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
						std::string b = buffer;
						sum += std::stoi(b);
					}
					std::cout << "sum:" << sum <<std::endl;
				}
				else {

					MPI_Status status{};
					char buffer[100]{};
					buffer[0] = '\0';
					mpi::check(MPI_Recv(buffer, sizeof(buffer) / sizeof(buffer[0]), MPI_CHAR, 0, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
					std::string rec = buffer;
					std::vector<std::string> v = split(rec, ';');

					double a = integrate_serial(funcs.at(std::stoi(v[0])), funcs2.at(std::stoi(v[4])), std::stoi(v[1]), std::stoi(v[2]), std::stoi(v[3]));

					std::string message = std::to_string(a);

					mpi::check(MPI_Send(message.c_str(), message.size() + 1, MPI_CHAR, 0, 0, MPI_COMM_WORLD));


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





	fn::integrate_parallel(argc, argv);


}


