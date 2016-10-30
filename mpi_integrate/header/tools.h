#pragma once

#include <mpi.h>
#include <iostream>
#include <stdexcept>

namespace mpi {


	class mpi_execption : public std::runtime_error
	{
	public:
		mpi_execption(char * text);
		~mpi_execption();
	private:
	};

	mpi_execption::mpi_execption(char * text) :runtime_error(text) {

	}
	
	mpi_execption::~mpi_execption()
	{
	}
	
	static int check(int error) throw () {
			if (error != MPI_SUCCESS)
			{
			char errortext[MPI_MAX_ERROR_STRING];
			int max_len{};
			MPI_Error_string(error,errortext,&max_len);
				throw mpi_execption(errortext);
			}
		}
}