//       $Id: pfc_compiler_macros.h 32633 2016-10-11 10:28:42Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_compiler_macros.h $
// $Revision: 32633 $
//     $Date: 2016-10-11 12:28:42 +0200 (Di., 11 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PCF_COMPILER_MACROS_H
#define      PCF_COMPILER_MACROS_H

// -------------------------------------------------------------------------------------------------

#undef COMP_CL
#undef COMP_CLANG
#undef COMP_GCC
#undef COMP_INTEL
#undef COMP_MICROSOFT
#undef COMP_NVCC

// -------------------------------------------------------------------------------------------------

#if defined __clang__
   #define COMP_CLANG
#endif

#if defined __CUDACC__
   #define COMP_NVCC
#endif

#if defined __GNUC__
   #define COMP_GCC
#endif

#if defined __INTEL_COMPILER
   #define COMP_INTEL
#endif

#if defined _MSC_VER
   #define COMP_CL
   #define COMP_MICROSOFT
#endif

// -------------------------------------------------------------------------------------------------

#endif   // PCF_COMPILER_MACROS_H
