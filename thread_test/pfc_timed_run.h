//       $Id: pfc_timed_run.h 33283 2016-12-08 11:41:43Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_timed_run.h $
// $Revision: 33283 $
//     $Date: 2016-12-08 12:41:43 +0100 (Do., 08 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
// Copyright: (c) 2016 Peter Kulczycki (peter.kulczycki<AT>fh-hagenberg.at)
//   License: Distributed under the Boost Software License, Version 1.0 (see
//            http://www.boost.org/LICENSE_1_0.txt).

#if !defined PFC_TIMED_RUN
#define      PFC_TIMED_RUN

#include "./pfc_cuda_macros.h"

#include <algorithm>
#include <chrono>
#include <functional>
#include <future>
#include <thread>
#include <vector>

using namespace std::literals;

// --------------------------------------------------------------------------------------------------

#if defined _WINDOWS_
   #undef  PFC_WINDOWS_H_INCLUDED
   #define PFC_WINDOWS_H_INCLUDED
#endif

// --------------------------------------------------------------------------------------------------

namespace pfc {

template <typename unit_t, typename duration_t> constexpr inline auto duration_in (duration_t const & duration) {
   return std::chrono::duration_cast <unit_t> (duration);   //.count ();
}

template <typename duration_t> constexpr inline auto duration_in_ms (duration_t const & duration) {
   return pfc::duration_in <std::chrono::milliseconds> (duration);
}

template <typename duration_t> constexpr inline auto duration_in_s (duration_t const & duration) {
   return pfc::duration_in <std::chrono::seconds> (duration);
}

template <typename duration_t> constexpr inline double speedup (duration_t const & serial, duration_t const & parallel) {
   constexpr static typename duration_t::rep const zero = 0; return (parallel.count () != zero) ? 1.0 * serial.count () / parallel.count () : 0;
}

template <typename duration_t> constexpr inline double efficiency (duration_t const & serial, duration_t const & parallel, int const cores) {
   return (cores > 0) ? pfc::speedup (serial, parallel) / cores : 0;
}

// --------------------------------------------------------------------------------------------------

#if defined PFC_WINDOWS_H_INCLUDED

struct tsc_clock final {   // time stamp counter (cannot be converted to seconds since this is meaningless)
   using duration   = std::chrono::duration <decltype (__rdtsc ()), std::ratio <1, 1>>;
   using period     = duration::period;
   using rep        = duration::rep;
   using time_point = std::chrono::time_point <pfc::tsc_clock>;

   constexpr static bool const is_steady {false};

   static time_point now () {
      return time_point (duration (__rdtsc ()));
   }
};

#endif   // PFC_WINDOWS_H_INCLUDED

using default_timer = std::chrono::high_resolution_clock;   // pfc::tsc_clock, std::chrono::high_resolution_clock

// --------------------------------------------------------------------------------------------------

inline bool set_priority_to_realtime () {
   #if defined PFC_WINDOWS_H_INCLUDED
      return SetPriorityClass (GetCurrentProcess (), REALTIME_PRIORITY_CLASS) != 0;
   #else
      return true;
   #endif
}

inline bool warm_up_cpu (std::chrono::seconds const how_long = 5s) {
   auto const cores {std::max <int> (1, std::thread::hardware_concurrency ())};
   auto const start {std::chrono::high_resolution_clock::now ()};

   std::vector <std::thread> group;

   for (int i {0}; i < cores; ++i) {
      group.emplace_back ([how_long, start] {
         while (
            pfc::duration_in_s (
               std::chrono::high_resolution_clock::now () - start   // busy waiting for how_long seconds
            ) < how_long
         );
      });
   }

   for (auto & t : group) {   // join_all
      if (t.joinable ()) {
         t.join ();
      }
   }

   return true;
}

// --------------------------------------------------------------------------------------------------

template <typename timer_t = pfc::default_timer, typename fun_t, typename ...args_t> inline auto /*typename timer_t::duration*/ timed_run (int const n, fun_t && fun, args_t && ...args) {
   auto       f     {std::bind (std::forward <fun_t> (fun), std::forward <args_t> (args)...)};
   auto const start {timer_t::now ()};

   for (int i {0}; i < n; ++i) {
      f ();
   }

   auto const stop {timer_t::now ()}; return (stop - start) / std::max (1, n);
}

template <typename timer_t = pfc::default_timer, typename fun_t, typename ...args_t> inline auto /*typename timer_t::duration*/ timed_run (fun_t && fun, args_t && ...args) {
   return pfc::timed_run <timer_t, fun_t, args_t...> (1, std::forward <fun_t> (fun), std::forward <args_t> (args)...);
}

}   // namespace pfc

// --------------------------------------------------------------------------------------------------

#endif   // PFC_TIMED_RUN
