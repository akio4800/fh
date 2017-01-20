//       $Id: pfc_vector_adapter.h 33348 2016-12-15 12:24:51Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_vector_adapter.h $
// $Revision: 33348 $
//     $Date: 2016-12-15 13:24:51 +0100 (Do., 15 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_VECTOR_ADAPTER_H
#define      PFC_VECTOR_ADAPTER_H

#include "./pfc_cuda_macros.h"

#include <cassert>
#include <functional>
#include <iostream>
#include <numeric>

namespace pfc {

// -------------------------------------------------------------------------------------------------

template <typename T> class vector_adapter final {
   public:
      using value_t = T;
      using self    = pfc::vector_adapter <value_t>;

      class iterator final : public std::iterator <std::bidirectional_iterator_tag, value_t> {
         using inherited = std::iterator <std::bidirectional_iterator_tag, value_t>;

         public:
            using pointer   = typename inherited::pointer;
            using reference = typename inherited::reference;

            iterator () = delete;

            explicit iterator (pointer const ptr) : m_ptr {ptr} {
            }

            explicit iterator (pointer const ptr, int const stride) : m_ptr {ptr}, m_stride {stride} {
            }

            iterator (iterator const &) = default;
            iterator (iterator &&) = default;

            ~iterator () = default;

            iterator & operator = (iterator const &) = default;
            iterator & operator = (iterator &&) = default;

            bool operator == (iterator const & rhs) const {
               return m_ptr == rhs.m_ptr;
            }

            bool operator != (iterator const & rhs) const {
               return !operator == (rhs);
            }

            iterator & operator ++ () {
               m_ptr += m_stride; return *this;
            }

            iterator & operator -- () {
               m_ptr -= m_stride; return *this;
            }

            iterator operator ++ (int) {
               auto const tmp {*this}; operator ++ (); return tmp;
            }

            iterator operator -- (int) {
               auto const tmp {*this}; operator -- (); return tmp;
            }

            reference operator * () {
               assert (m_ptr != nullptr); return *m_ptr;
            }

            pointer operator -> () {
               return m_ptr;
            }

         private:
            pointer m_ptr    {nullptr};
            int     m_stride {1};
      };

      iterator begin () const {
         return iterator {m_p_data, m_stride};
      }

      iterator end () const {
         return iterator {m_p_data + size () * m_stride};
      }

      friend std::ostream & operator << (std::ostream & lhs, self const & rhs) {
         return rhs.write (lhs);
      }

      friend value_t operator * (self const & lhs, self const & rhs) {
         assert (lhs.size () == rhs.size ()); return std::inner_product (std::begin (lhs), std::end (lhs), std::begin (rhs), value_t {});
      }

      vector_adapter () = default;

      explicit vector_adapter (int const size, value_t * const p_data = nullptr) : m_p_data {p_data}, m_size {size} {
         assert (0 < size);

         if (p_data == nullptr) {
            allocate (size);
         }
      }

      explicit vector_adapter (int const size, int const stride, value_t * const p_data = nullptr) : m_p_data {p_data}, m_size {size}, m_stride {stride} {
         assert (0 < size);
         assert (0 < stride);

         if (p_data == nullptr) {
            allocate (size);
         }
      }

      vector_adapter (vector_adapter const &) = delete;   // no copy construction

      vector_adapter (vector_adapter && tmp) noexcept {   // move construction
         move_from (tmp);
      }

      ~vector_adapter () {
         clear ();
      }

      vector_adapter & operator = (vector_adapter const &) = delete;   // no copy assignment

      vector_adapter & operator = (vector_adapter && tmp) noexcept {
         clear (); move_from (tmp); return *this;
      }

      void allocate (int const size, value_t const & v = {}) {
         assert (0 < size); clear ();

         m_owner  = true;
         m_size   = size;
         m_p_data = new value_t [size * m_stride];

         fill (v);
      }

      void clear () {
         if (m_owner) {
            delete [] m_p_data;
         }

         m_owner  = false;
         m_p_data = nullptr;
         m_size   = 0;
      }

      value_t * data () {
         return m_p_data;
      }

      value_t const * data () const {
         return m_p_data;
      }

      void fill (value_t const & v) {
         std::fill (begin (), end (), v);
      }

      value_t & get (int const i) {
         assert ((0 <= i) && (i < size ()) && (m_p_data != nullptr)); return m_p_data[i * m_stride];
      }

      value_t const & get (int const i) const {
         assert ((0 <= i) && (i < size ()) && (m_p_data != nullptr)); return m_p_data[i * m_stride];
      }

      bool read (std::istream & in) {
         for (auto & e : *this) {
            in >> e;
         }

         return in.good ();
      }

      int const & size () const {
         return m_size;
      }

      std::ostream & write (std::ostream & out = std::cout) const {
         bool first {true}; out << '{';

         for (auto const & e : *this) {
            if (!first) { out << ','; } out << e; first = false;
         }

         return out << '}';
      }

   private:
      void move_from (vector_adapter & tmp) {
         m_owner  = tmp.m_owner;  tmp.m_owner  = false;
         m_p_data = tmp.m_p_data; tmp.m_p_data = nullptr;
         m_size   = tmp.m_size;   tmp.m_size   = 0;
         m_stride = tmp.m_stride; tmp.m_stride = 1;
      }

      bool      m_owner  {false};     // I'm the m_owner of the allocated memory
      value_t * m_p_data {nullptr};   // pointer to allocated memory
      int       m_size   {0};         // number of elements
      int       m_stride {1};         // distance, in Ts, from an element to the next
};

// -------------------------------------------------------------------------------------------------

}   // namespace pfc

#endif   // PFC_VECTOR_ADAPTER_H
