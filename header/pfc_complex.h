//       $Id: pfc_complex.h 32873 2016-10-29 11:49:49Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_complex.h $
// $Revision: 32873 $
//     $Date: 2016-10-29 13:49:49 +0200 (Sa., 29 Okt 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PCF_COMPLEX_H
#define      PCF_COMPLEX_H

#include "./pfc_cuda_macros.h"

#include <type_traits>

namespace pfc {

// -------------------------------------------------------------------------------------------------

template <typename T = double> class complex final {
   public:
      static_assert (std::is_floating_point <T>::value, "");

      using real_t  = T;
      using value_t = T;

      CATTR_HOST_DEV constexpr complex () = default;
      
      CATTR_HOST_DEV constexpr complex (T const r) : real (r) {
      }
      
      CATTR_HOST_DEV constexpr complex (T const r, T const i) : real (r), imag (i) {
      }

      CATTR_HOST_DEV complex (complex const &) = default;
      CATTR_HOST_DEV complex (complex &&) = default;

      CATTR_HOST_DEV complex & operator = (complex const &) = default;
      CATTR_HOST_DEV complex & operator = (complex &&) = default;

      CATTR_HOST_DEV complex & operator += (complex const & rhs) {
         real += rhs.real;
         imag += rhs.imag;

         return *this;
      }

      CATTR_HOST_DEV complex operator + (complex const & rhs) const {
         auto x = *this; return x += rhs;
      }

      CATTR_HOST_DEV complex operator * (complex const & rhs) const {
         return complex (real * rhs.real - imag * rhs.imag, imag * rhs.real + real * rhs.imag);
      }

      CATTR_HOST_DEV T norm () const {
         return real * real + imag * imag;
      }

      CATTR_HOST_DEV complex & square () {
         auto const r = real * real - imag * imag;

         imag = real * imag * 2;
         real = r;

         return *this;
      }

      T real = 0;
      T imag = 0;
};

// -------------------------------------------------------------------------------------------------

template <typename T> CATTR_HOST_DEV inline auto operator + (T const lhs, pfc::complex <T> const & rhs) {
   return pfc::complex <T> (lhs + rhs.real, rhs.imag);
}

template <typename T> CATTR_HOST_DEV inline auto operator * (T const lhs, pfc::complex <T> const & rhs) {
   return pfc::complex <T> (lhs * rhs.real, lhs * rhs.imag);
}

template <typename T> CATTR_HOST_DEV inline auto norm (pfc::complex <T> const & x) {
   return x.norm ();
}

template <typename T> CATTR_HOST_DEV inline auto & square (pfc::complex <T> & x) {
   return x.square ();
}

namespace literals {

CATTR_HOST_DEV constexpr inline auto operator "" _imag_f (long double const lit) {
   return pfc::complex <float> (0.0f, static_cast <float> (lit));
}

CATTR_HOST_DEV constexpr inline auto operator "" _imag_d (long double const lit) {
   return pfc::complex <double> (0.0, static_cast <double> (lit));
}

CATTR_HOST_DEV constexpr inline auto operator "" _imag_l (long double const lit) {
   return pfc::complex <long double> (0.0l, lit);
}

CATTR_HOST_DEV constexpr inline auto operator "" _real_f (long double const lit) {
   return pfc::complex <float> (static_cast <float> (lit));
}

CATTR_HOST_DEV constexpr inline auto operator "" _real_d (long double const lit) {
   return pfc::complex <double> (static_cast <double> (lit));
}

CATTR_HOST_DEV constexpr inline auto operator "" _real_l (long double const lit) {
   return pfc::complex <long double> (lit);
}

}   // namespace literals

// -------------------------------------------------------------------------------------------------

}   // namespace pfc

#endif   // PCF_COMPLEX_H
