//       $Id: pfc_mpi_exception.h 33348 2016-12-15 12:24:51Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/BIN/PAR1/2016-WS/ILV/src/handouts/pfc_mpi_exception.h $
// $Revision: 33348 $
//     $Date: 2016-12-15 13:24:51 +0100 (Do., 15 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_MPI_EXCEPTION_H
#define      PFC_MPI_EXCEPTION_H

#include <mpi.h>

#include <stdexcept>
#include <string>

// -------------------------------------------------------------------------------------------------

#undef  PFC_MPI_CHECK
#define PFC_MPI_CHECK(call) \
   pfc::mpi::check (call, __FILE__, __LINE__)

// -------------------------------------------------------------------------------------------------

namespace pfc { namespace mpi {

class exception final : public std::runtime_error {
   using inherited = std::runtime_error;

   public:
      exception () = delete;

      explicit exception (std::string message) : inherited (std::move (message)) {
      }

      explicit exception (int const error, std::string const & file, int const line) : inherited (make_message (error, file, line)) {
      }

      exception (exception const &) = default;
      exception (exception &&) = default;

     ~exception () = default;

      exception & operator = (exception const &) = default;
      exception & operator = (exception &&) = default;

   private:
      static char const * get_error_string (int const error) {
         static char buffer [MPI_MAX_ERROR_STRING + 1] {};
         static int  size                              {};

         MPI_Error_string (error, buffer, &size); return buffer;
      }

      static std::string make_message (int const error, std::string const & file, int const line) {
         static std::string message; message.clear ();

         message += "MPI error #";
         message += std::to_string (error);
         message += " '";
         message += get_error_string (error);
         message += "' occurred";

         if (!file.empty () && (0 < line)) {
            message += " in file '";
            message += file;
            message += "' on line ";
            message += std::to_string (line);
         }

         return std::move (message);
      }
};

// -------------------------------------------------------------------------------------------------

inline void check (int const error, std::string const & file, int const line) {
   if (error != MPI_SUCCESS) {
      throw pfc::mpi::exception (error, file, line);
   }
}

inline void check (int const error) {
   pfc::mpi::check (error, "", 0);
}

// -------------------------------------------------------------------------------------------------

} }   // namespace mpi, namespace pfc

#endif   // PFC_MPI_EXCEPTION_H
