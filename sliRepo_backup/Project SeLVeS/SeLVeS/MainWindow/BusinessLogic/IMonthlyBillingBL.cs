using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware.BusinessLogic
{
    //-----------------------------------------------------------------------------------------------------
    public interface IMonthlyBillingBl
    {
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Abfrage einer Monatsabrechnung mit einer bestimmten id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>alle Daten des PA</returns>
        MonthlyBillingPerPa SelectMb(MonthlyBillingPerPa mb);
        
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// alle Monatsabrechnungen aus der DB holen
        /// </summary>
        /// <returns></returns>
        List<MonthlyBilling> SelectAllMB();
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// alle Monatsabrechnungen für eine bestimmte Periode aus der DB holen
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        List<MonthlyBilling> SelectMbPeriod(DateTime from, DateTime to);
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// holt alle Monatsabrechnungen eines bestimmten AG aus der DB
        /// </summary>
        /// <param name="pur"></param>
        /// <returns></returns>
        List<MonthlyBilling> SelectAllMB(Purchaser pur);
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// holt eine bestimmte Monatsabrechnung aus der DB (bestimmtes Monat für bestimmten AG)
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>

        bool IsNightShift(Activity activity);
        bool IsNight(DateTime dateTime1, DateTime dateTime2);
        bool IsHoliday(DateTime currentDate);
        bool UpdateMb(MonthlyBilling mb);

        
        MonthlyBilling InsertMonthlyBilling(MonthlyBilling mb);
        MonthlyBillingPerPa InsertMonthlyBilling(MonthlyBillingPerPa mb);
        MonthlyBilling SelectAllMB(MonthlyBilling mb);
        bool UpdateMonthlyBillingEntry(MonthlyBillingPerPa mb);

        List<MonthlyBillingPerPa> SelectMBperPa(List<MonthlyBillingPerPa> mbppa);
        MonthlyBilling SelectMb(int selectedYear, int selectedMonth, PurchaserData selectedPurchaserData, List<PersonalAssistant> currentAssistents);
        void deleteReha(int month, int year, long agid, int days);
        void insertReha(long agid, int month, int year, int days);
    }
}
