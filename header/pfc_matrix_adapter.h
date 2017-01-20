//       $Id: pfc_matrix_adapter.h 33348 2016-12-15 12:24:51Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_matrix_adapter.h $
// $Revision: 33348 $
//     $Date: 2016-12-15 13:24:51 +0100 (Do., 15 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_MATRIX_ADAPTER_H
#define      PFC_MATRIX_ADAPTER_H

#include "./pfc_vector_adapter.h"

namespace pfc {

// -------------------------------------------------------------------------------------------------

template <typename T> class matrix_adapter final {
   public:
      using value_t          = T;
      using self             = pfc::matrix_adapter <value_t>;
      using vector_adapter_t = pfc::vector_adapter <value_t>;

      friend std::ostream & operator << (std::ostream & lhs, self const & rhs) {
         return rhs.write (lhs);
      }

      friend self operator * (self const & lhs, self const & rhs) {
         assert (lhs.cols () == rhs.rows ()); self result {lhs.rows (), rhs.cols ()};

         for (int r {0}; r < lhs.rows (); ++r) {
            for (int c {0}; c < rhs.cols (); ++c) {
               result.get (r, c) = lhs.vector_for_row (r) * rhs.vector_for_column (c);
            }
         }

         return result;
      }

      matrix_adapter () = default;

      explicit matrix_adapter (int const r, int const c, value_t * const p_data = nullptr) : m_cols {c}, m_owner {false}, m_p_data {p_data}, m_rows {r} {
         assert (0 < r);
         assert (0 < c);

         if (p_data == nullptr) {
            allocate (r, c);
         }
      }

      matrix_adapter (matrix_adapter const &) = delete;   // no copy construction

      matrix_adapter (matrix_adapter && tmp) noexcept {   // move construction
         move_from (tmp);
      }

      ~matrix_adapter () {
         clear ();
      }

      matrix_adapter & operator = (matrix_adapter const &) = delete;   // no copy assignment

      matrix_adapter & operator = (matrix_adapter && tmp) noexcept {
         clear (); move_from (tmp); return *this;
      }

      matrix_adapter & operator += (matrix_adapter const & rhs) {
         assert (rhs.m_rows == m_rows);
         assert (rhs.m_cols == m_cols);

         for (int i {0}; i < size (); ++i) {
            m_p_data[i] += rhs.m_p_data[i];
         }

         return *this;
      }

      void allocate (int const r, int const c, value_t const & v = {}) {
         assert (0 < r);
         assert (0 < c);

         clear ();

         m_cols   = c;
         m_owner  = true;
         m_rows   = r;
         m_p_data = new value_t [size ()];

         fill (v);
      }

      void clear () {
         if (m_owner) {
            delete [] m_p_data;
         }

         m_cols   = 0;
         m_owner  = false;
         m_p_data = nullptr;
         m_rows   = 0;
      }

      int const & cols () const {
         return m_cols;
      }

      value_t * data () {
         return m_p_data;
      }

      value_t const * data () const {
         return m_p_data;
      }

      void fill (value_t const & v) {
         std::fill (m_p_data, m_p_data + size (), v);
      }

      value_t & get (int const r, int const c) {
         assert (m_p_data != nullptr); return m_p_data[index_from_coord (r, c)];
      }

      value_t const & get (int const r, int const c) const {
         assert (m_p_data != nullptr); return m_p_data[index_from_coord (r, c)];
      }

      int index_from_coord (int const r, int const c) const {
         assert ((0 <= r) && (r < m_rows));
         assert ((0 <= c) && (c < m_cols));

         return r * m_cols + c;
      }

      bool is_quadratic () const {
         return m_rows == m_cols;
      }

      bool read (std::istream & in) {
         assert (m_p_data != nullptr); for (int i {0}; (i < size ()) && (in >> m_p_data[i]); ++i); return in.good ();
      }

      int const & rows () const {
         return m_rows;
      }

      int size () const {
         return m_rows * m_cols;
      }

      vector_adapter_t vector_for_column (int const c) const {
         assert ((0 <= c) && (c < m_cols)); return vector_adapter_t {m_rows, m_cols, m_p_data + c};
      }

      vector_adapter_t vector_for_row (int const r) const {
         assert ((0 <= r) && (r < m_rows)); return vector_adapter_t {m_cols, m_p_data + r * m_cols};
      }

      std::ostream & write (std::ostream & out = std::cout) const {
         bool first {true}; out << '{';

         for (int r {0}; r < m_rows; ++r) {
            if (!first) { out << ','; } out << vector_for_row (r); first = false;
         }

         return out << '}';
      }

   private:
      void move_from (matrix_adapter & tmp) {
         m_cols   = tmp.m_cols;   tmp.m_cols   = 0;
         m_owner  = tmp.m_owner;  tmp.m_owner  = false;
         m_p_data = tmp.m_p_data; tmp.m_p_data = nullptr;
         m_rows   = tmp.m_rows;   tmp.m_rows   = 0;
      }

      int       m_cols   {0};         // number of columns
      bool      m_owner  {false};     // I'm the m_owner of the allocated memory
      value_t * m_p_data {nullptr};   // pointer to allocated memory
      int       m_rows   {0};         // number of m_rows
};

// -------------------------------------------------------------------------------------------------

}   // namespace pfc

#endif   // PFC_MATRIX_ADAPTER_H
