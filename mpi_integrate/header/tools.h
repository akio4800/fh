#pragma once

#include <mpi.h>
#include <iostream>
#include <stdexcept>
#include <functional>
#include <string>
#include <vector>
#include <sstream>
#include<map>

namespace mpi {


	class mpi_exeception : public std::runtime_error
	{
	public:
		mpi_exeception(char * text);
		~mpi_exeception();
	private:
	};

	mpi_exeception::mpi_exeception(char * text) :runtime_error(text) {

	}
	
	mpi_exeception::~mpi_exeception()
	{
	}
	
	static int check(int error) throw () {
			if (error != MPI_SUCCESS)
			{
			char errortext[MPI_MAX_ERROR_STRING];
			int max_len{};
			MPI_Error_string(error,errortext,&max_len);
				throw mpi_exeception(errortext + __LINE__);
			}
		}
}