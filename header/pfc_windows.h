//       $Id: pfc_windows.h 32633 2016-10-11 10:28:42Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_windows.h $
// $Revision: 32633 $
//     $Date: 2016-10-11 12:28:42 +0200 (Di., 11 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_WINDOWS_H
#define      PFC_WINDOWS_H

// -------------------------------------------------------------------------------------------------

#undef  NOMINMAX
#define NOMINMAX

#undef  STRICT
#define STRICT

#undef  VC_EXTRALEAN
#define VC_EXTRALEAN

#undef  WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN

#include <windows.h>

#undef  PFC_WINDOWS_H_INCLUDED
#define PFC_WINDOWS_H_INCLUDED

// -------------------------------------------------------------------------------------------------

#endif   // PFC_WINDOWS_H
