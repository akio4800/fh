//       $Id: pfc_cuda_exception.h 32685 2016-10-14 15:41:12Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_cuda_exception.h $
// $Revision: 32685 $
//     $Date: 2016-10-14 17:41:12 +0200 (Fr., 14 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_CUDA_EXCEPTION_H
#define      PFC_CUDA_EXCEPTION_H

#include "./pfc_cuda_macros.h"

#include <cuda_runtime.h>
#include <device_launch_parameters.h>

#include <stdexcept>
#include <string>

// -------------------------------------------------------------------------------------------------

#undef  PFC_CUDA_CHECK
#define PFC_CUDA_CHECK(call) \
   pfc::cuda::check (call, __FILE__, __LINE__)

// -------------------------------------------------------------------------------------------------

namespace pfc { namespace cuda {

class exception : public std::runtime_error {
   using inherited = std::runtime_error;

   public:
      explicit exception (cudaError const error, std::string const & file, int const line) : inherited (make_message (error, file, line)) {
      }

   private:
      static std::string make_message (cudaError const error, std::string const & file, int const line) {
         std::string message = "CUDA error #";

         message += std::to_string (error);
         message += " '";
         message += cudaGetErrorString (error);
         message += "' occurred";

         if (!file.empty () && (line > 0)) {
            message += " in file '";
            message += file;
            message += "' on line ";
            message += std::to_string (line);
         }

         return std::move (message);
      }
};

// -------------------------------------------------------------------------------------------------

inline void check (cudaError const error, std::string const & file, int const line) {
   if (error != cudaSuccess) {
      throw pfc::cuda::exception (error, file, line);
   }
}

inline void check (cudaError const error) {
   pfc::cuda::check (error, "", 0);
}

// -------------------------------------------------------------------------------------------------

} }   // namespace cuda, namespace pfc

#endif   // PFC_CUDA_EXCEPTION_H
