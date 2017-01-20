//       $Id: pfc_random.h 33348 2016-12-15 12:24:51Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_random.h $
// $Revision: 33348 $
//     $Date: 2016-12-15 13:24:51 +0100 (Do., 15 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PCF_RANDOM_H
#define      PCF_RANDOM_H

#include "./pfc_cuda_macros.h"

#include <ctime>
#include <random>
#include <type_traits>

namespace pfc {

// -------------------------------------------------------------------------------------------------

template <typename T> inline T get_random_uniform (T const l, T const u) {
   static_assert (std::is_integral <T>::value, "");

   static auto generator = std::mt19937_64 (static_cast <std::mt19937_64::result_type> (std::time (nullptr)));

   return std::uniform_int_distribution <T> (l, u) (generator);
}

// -------------------------------------------------------------------------------------------------

}   // namespace pfc

#endif   // PCF_RANDOM_H
