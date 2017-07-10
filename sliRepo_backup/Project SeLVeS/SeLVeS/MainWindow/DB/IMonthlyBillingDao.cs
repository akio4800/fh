using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SelvesSoftware.DB
{
    public interface IMonthlyBillingDAO
    {
        List<MonthlyBilling> selectAllMB(); //ok
        MonthlyBillingPerPa selectMBEntry(MonthlyBillingPerPa mb);
        List<MonthlyBilling> SelectPeriod(DateTime from, DateTime to);
        List<MonthlyBilling> selectSpecificMB(MonthlyBilling mb);
        List<MonthlyBilling> selectSpecificMB(Purchaser pur);
        List<MonthlyBilling> selectAllMB(MonthlyBilling pur);
        MonthlyBilling InsertMonthlyBilling(MonthlyBilling mb);
        MonthlyBillingPerPa InsertMonthlyBilling(MonthlyBillingPerPa mb);
        bool UpdateMonthlyBilling(MonthlyBilling mb);
        bool UpdateMonthlyBillingEntry(MonthlyBillingPerPa mb);

        List<MonthlyBillingPerPa> SelectMBperPa(MonthlyBilling mb);

        List<MonthlyBilling> selectAllFrom(DateTime? nullable);
        void DeleteReha(int month, int year, long agid, int days);
        void InsertReha(long agid, int month, int year, int days);
        int selectReha(long agid, int month, int year);
    }
}
