#include <iostream>
#include <string>



int power2(int s) {

	return 1 << s;
}



int ceil_log2(int s) {
	int i = 0;
	--s;
	while (s > 0) {
		s >>= 1; ++i;
	}
	return i;
}





int must_receive(int s, int rank, int root, int size) {


	if (rank >= power2(s) && rank < power2(s + 1)) {

		return (rank - power2(s) + root ) % size;
	}

	return -1;

}



int must_send(int s, int rank, int root, int size) {

	if (rank < power2(s) && rank + power2(s) < size) {

		return (rank + power2(s) +root ) %size;
	}


	return -1;

}




void main() {

	size_t size = 100;
	int root = 10;




	for (int s = 0; s < ceil_log2(size); ++s) {



		std::string send = "";
		std::string rec = "";

		for (size_t n = 0; n < size; n++)
		{
			int buddy = -1;

			if ((buddy = must_receive(s, n, root, size)) > -1) {
				// receive(data, rank, buddy);

				send  += std::to_string((n + root) % size) + "<-" + std::to_string(buddy) + " ";



			}
			else if ((buddy = must_send(s, n, root, size)) > -1) {
				rec += std::to_string((n + root)% size) + "->" + std::to_string(buddy) + " ";

			}

		}



		std::cout << "send: " << send << std::endl ;
		std::cout << "rec:  " << rec << std::endl << std::endl;

	}
}



