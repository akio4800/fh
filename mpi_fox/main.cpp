
#include "header/tools.h"

typedef std::pair<MPI_Comm, MPI_Comm> MPI_comm_pair;

namespace fn {


	

	void distribute() {




	}



	MPI_comm_pair create_comm(int size, MPI_Comm comm) {



		MPI_Comm grid_comm {};
		MPI_Comm row_comm {};
		MPI_Comm col_comm {};

		int sizes[2]{ size,size };
		int warp[2]{ false,false };
		int free_coords_row[2]{ false,true };
		int free_coords_col[2]{ true,false };


		MPI_Cart_create(comm, 2, sizes, warp, false, &grid_comm);
		MPI_Cart_sub(grid_comm, free_coords_row, &row_comm);
		MPI_Cart_sub(grid_comm, free_coords_row, &col_comm);

		return  MPI_comm_pair(row_comm, col_comm);

	}



}