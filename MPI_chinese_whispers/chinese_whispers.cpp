
#include "tools.h"


void main(int argc, char* argv[]) {
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
				// create and send messages:
				std::string message = "clockwise";
				mpi::check(MPI_Send(message.c_str(), message.size() + 1, MPI_CHAR, 1, 0, MPI_COMM_WORLD));

				message = "counter-clockwise";
				mpi::check(MPI_Send(message.c_str(), message.size() + 1, MPI_CHAR, size - 1, 0, MPI_COMM_WORLD));

				// expect two messages, so get from 2 sources. Not made in a loop because with only 2 messages it looks more understandable to call Recv twice.
				MPI_Status status{};
				char buffer[100]{};
				buffer[0] = '\0';
				mpi::check(MPI_Recv(buffer, sizeof(buffer) / sizeof(buffer[0]), MPI_CHAR, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
				std::cout << "msg: " << buffer << std::endl;
				mpi::check(MPI_Recv(buffer, sizeof(buffer) / sizeof(buffer[0]), MPI_CHAR, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status));
				std::cout << "msg: " << buffer << std::endl;
			}
			else {
				for (int i = 0; i < 2; i++) {
					// two messages, one clockwise and one counter-clockwise

					MPI_Status status{};
					char buffer[100]{};
					buffer[0] = '\0';
					mpi::check(MPI_Recv(buffer, sizeof(buffer) / sizeof(buffer[0]), MPI_CHAR, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &status));


					int dest = -1;
					if (status.MPI_SOURCE != 0) {
						// sender other than 0
						if (status.MPI_SOURCE < rank) {
							// clockwise:
							if (rank == size - 1) {
								dest = 0;
							}
							else {
								dest = rank + 1;
							}
							mpi::check(MPI_Send(buffer, sizeof(buffer), MPI_CHAR, dest, 0, MPI_COMM_WORLD));
						}
						else {
							dest = rank - 1;
							mpi::check(MPI_Send(buffer, sizeof(buffer), MPI_CHAR, dest, 0, MPI_COMM_WORLD));
						}
					}
					else {
						// if sender is 0
						if (rank == 1) {
							// clockwise:
							if (rank == size - 1) {
								dest = 0;
							}
							else {
								dest = rank + 1;
							}
							mpi::check(MPI_Send(buffer, sizeof(buffer), MPI_CHAR, dest, 0, MPI_COMM_WORLD));
						}
						if (rank == size - 1) {
							dest = rank - 1;
							mpi::check(MPI_Send(buffer, sizeof(buffer), MPI_CHAR, dest, 0, MPI_COMM_WORLD));
						}
					}
				}
			}
		}
		else {
			std::cout << "more than one note requiered" << std::endl;
		}
		mpi::check(MPI_Finalize());



	}
	catch (mpi::mpi_exeception &e) {
		std::cerr << e.what() << std::endl;
	}
	;
}