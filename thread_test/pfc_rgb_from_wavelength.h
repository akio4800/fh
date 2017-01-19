//       $Id: pfc_rgb_from_wavelength.h 32878 2016-10-30 10:15:26Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_rgb_from_wavelength.h $
// $Revision: 32878 $
//     $Date: 2016-10-30 11:15:26 +0100 (So., 30 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#include <algorithm>
#include <cmath>
#include <limits>
#include <type_traits>

namespace pfc {

template <typename T> inline void rgb_from_wavelength_pm (T & color, int wavelength, bool const gray = false, double alpha = 0.8) {
   using blue_t     = decltype (T::blue);
   using col_comp_t = blue_t;
   using green_t    = decltype (T::green);
   using red_t      = decltype (T::red);

   static_assert (std::is_same <col_comp_t, green_t>::value, "");
   static_assert (std::is_same <col_comp_t, red_t>::value,   "");
   static_assert (std::is_unsigned <col_comp_t>::value,      "");
   static_assert (!std::is_same <col_comp_t, bool>::value,   "");

   alpha      = std::min (std::max (alpha, 0.0), 1.0);
   wavelength = std::min (std::max (wavelength, 380000), 780000);

   unsigned b = 0;
   unsigned g = 0;
   unsigned r = 0;

   if (wavelength <  440000) { r = (440000 - wavelength) / 60; b = 1000; } else
   if (wavelength <  490000) { g = (wavelength - 440000) / 50; b = 1000; } else
   if (wavelength <  510000) { g = 1000; b = (510000 - wavelength) / 20; } else
   if (wavelength <  580000) { r = (wavelength - 510000) / 70; g = 1000; } else
   if (wavelength <  645000) { r = 1000; g = (645000 - wavelength) / 65; } else
   if (wavelength <= 780000) { r = 1000;                                 }

   double f = 0;

   if (wavelength <  420000) { f = -0.006350 + 0.00000001750 * wavelength; } else
   if (wavelength <  701000) { f =  0.001000;                              } else
   if (wavelength <= 780000) { f =  0.007125 - 0.00000000875 * wavelength; }

   constexpr auto const m = std::numeric_limits <col_comp_t>::max ();

   color.blue  = col_comp_t (m * std::min (std::pow (b * f, alpha), 1.0));
   color.green = col_comp_t (m * std::min (std::pow (g * f, alpha), 1.0));
   color.red   = col_comp_t (m * std::min (std::pow (r * f, alpha), 1.0));

   if (gray) {
      color.red = color.green = color.blue = col_comp_t ((color.red + color.green + color.blue) / 3);
   }
}

template <typename T> inline void rgb_from_wavelength (T & color, int const wavelength, bool const gray = false, double const alpha = 0.8) {
   rgb_from_wavelength_pm <T> (color, wavelength * 1000, gray, alpha);
}

template <typename T> inline void rgb_from_wavelength (T & color, double const x, bool const gray = false, double const alpha = 0.8) {
   rgb_from_wavelength_pm <T> (color, static_cast <int> (380000 + (780000 - 380000) * x), gray, alpha);
}

}   // namespace pfc
