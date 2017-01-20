//       $Id: pfc_easy_logger.h 33348 2016-12-15 12:24:51Z p20068 $
//      $URL: https://svn01.fh-hagenberg.at/bin/cepheiden/vocational/teaching/SE/MPV3/2016-WS/ILV/src/handouts/pfc_easy_logger.h $
// $Revision: 33348 $
//     $Date: 2016-12-15 13:24:51 +0100 (Do., 15 Dez 2016) $
//   Creator: peter.kulczycki<AT>fh-hagenberg.at
//   $Author: p20068 $
//
//   Remarks: Do not forget to use the macro 'INITIALIZE_EASYLOGGINGPP' at global scope.

#if !defined PFC_EASY_LOGGER_H
#define      PFC_EASY_LOGGER_H

// -------------------------------------------------------------------------------------------------

#include <pfc_windows.h>

#undef  ELPP_THREAD_SAFE
#define ELPP_THREAD_SAFE

#include <easylogging++.h>

#include <iomanip>

// -------------------------------------------------------------------------------------------------

#undef  PFC_EASY_LOG
#define PFC_EASY_LOG \
   LOG (INFO) << std::setw (3) << std::setfill ('0') << PFC_EASY_LOGGER.next () << ": "

#undef  PFC_EASY_LOGGER
#define PFC_EASY_LOGGER \
   pfc::easy_logger::instance ()

// -------------------------------------------------------------------------------------------------

namespace pfc {

class easy_logger final {
   public:
      static easy_logger & instance () {
         static easy_logger i; return i;
      }

      easy_logger (easy_logger const &) = delete;
      easy_logger (easy_logger &&) = delete;

     ~easy_logger () = default;

      easy_logger & operator = (easy_logger const &) = delete;
      easy_logger & operator = (easy_logger &&) = delete;

      void initialize (int argc, char * argv []) {
         static bool initialized {false};

         if (!initialized) {
            START_EASYLOGGINGPP (argc, argv);

            el::Configurations cfg;

            cfg.setToDefault ();
            cfg.setGlobally  (el::ConfigurationType::Format, "%datetime{%H:%m:%s.%g}-%msg");

            el::Loggers::reconfigureLogger ("default", cfg);

            initialized = true;
         }

         m_count = 0;
      }

      auto const & next () {
         return ++m_count;
      }

   private:
      easy_logger () = default;

      int m_count {0};
};

}   // namespace pfc

// -------------------------------------------------------------------------------------------------

#endif   // PFC_EASY_LOGGER_H
