#include <fstream>
#include <iostream>
#include <vector>

template<typename T>
struct matrix {
	int col; 
	int row; 
	std::vector<T> data;
};


template<typename T>
using pmatrix = std::pair<matrix<T>, matrix<T>>;


template<typename T>
pmatrix<T> read_file()
{


	pmatrix<T> matr = new pmatrix();


	std::ifstream file("./input.txt");
	if (file.good())
	{
		int m, n, o = 0;
		file >> m >> n >> o;

		matr.first.row = m;
		matr.first.col = n;
		matr.second.row = o;
		matr.second.col = m;
		if (file.good())
		{
			int x = 0;
			for (int i = 0; file.good() && i<m*n; ++i)
			{
				file >> x;
			}
			for (int i = 0; file.good() && i<m*o; ++i)
			{
				file >> x;
			}
			if (file.good())
			{
				std::cout << "File successfully read\n";
			}
		}
		else
		{
			std::cout << "File corrupt\n";
		}
	}
	else
	{
		std::cout << "File not found!\n";
	}
}








int main(int argc, char* argv[]) {



	

}