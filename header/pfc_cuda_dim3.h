//       $Id: pfc_cuda_dim3.h 32893 2016-10-31 20:46:02Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_cuda_dim3.h $
// $Revision: 32893 $
//     $Date: 2016-10-31 21:46:02 +0100 (Mo., 31 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PCF_CUDA_DIM3_H
#define      PCF_CUDA_DIM3_H

#include "./pfc_cuda_macros.h"

#include <cuda_runtime.h>
#include <device_launch_parameters.h>

namespace pfc {

// -------------------------------------------------------------------------------------------------

template <typename T = unsigned, typename U = uint3> struct dim3 final {
   static_assert (std::is_integral <T>::value && !std::is_same <T, bool>::value, "");

   CATTR_HOST_DEV constexpr dim3 (T const x = 1, T const y = 1, T const z = 1) : x (x), y (y), z (z) {
   }

   CATTR_HOST_DEV constexpr dim3 (U const v) : x (v.x), y (v.y), z (v.z) {
   }

   CATTR_HOST_DEV dim3 (::dim3 const d) : x (d.x), y (d.y), z (d.z) {
   }

   CATTR_HOST_DEV operator U () const {
      return U (x, y, z);
   }

   CATTR_HOST_DEV operator ::dim3 () const {
      return ::dim3 (x, y, z);
   }

   T x = 1;
   T y = 1;
   T z = 1;
};

}   // namespace pfc

// -------------------------------------------------------------------------------------------------

#endif   // PCF_CUDA_DIM3_H
