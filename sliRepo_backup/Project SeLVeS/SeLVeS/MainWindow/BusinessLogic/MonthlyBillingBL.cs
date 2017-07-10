using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.BusinessLogic;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware.BusinessLogic
{
    //-----------------------------------------------------------------------------------------------------
    public class MonthlyBillingBl : IMonthlyBillingBl
    {
        private IMonthlyBillingDAO _mbDao;
        //-----------------------------------------------------------------------------------------------------
        public IMonthlyBillingDAO MbDao
        {
            get
            {
                if (_mbDao == null)
                {
                    _mbDao = new MonthlyBillingDAO();
                }
                return _mbDao;
            }
            set
            {
                _mbDao = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------
       public MonthlyBilling mb { get; set; }
            
            
        /// <summary>
        /// die Monatsabrechnung für einen PA aus der DB holen
        /// Thomas: hier müssen Jahr, Monat und AG auch angegeben werden ansonsten brauchen wir hier eine Liste.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        public MonthlyBillingPerPa SelectMb(MonthlyBillingPerPa mb)
        {          
            return MbDao.selectMBEntry(mb);
        }
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Liste von Monatsabrechnungen für einen PA aus DB holen
        /// </summary>
        /// <returns></returns>
        public List<MonthlyBillingPerPa> SelectMbPerPa()
        {
            MonthlyBilling mb = new MonthlyBilling();
            List<MonthlyBillingPerPa> mbppa = new List<MonthlyBillingPerPa>();
            mbppa = MbDao.SelectMBperPa(mb);

            return mbppa;
        }

        public MonthlyBilling SelectMb(int selectedYear, int selectedMonth, PurchaserData selectedPurchaserData, List<PersonalAssistant> currentAssistents)
        {
            MonthlyBillingPerPaBL bl = new MonthlyBillingPerPaBL();
            List<MonthlyBillingPerPa> monthlyBillingList = new List<MonthlyBillingPerPa>();
            mb = new MonthlyBilling();
            mb.Purchaser =  new PurchaserData();
            mb.Purchaser = selectedPurchaserData;
            mb.Month = selectedMonth;
            mb.Year = selectedYear;
            //------ get all MonthlyBillingPerPa for this month
            foreach (PersonalAssistant pa in currentAssistents)
            {
                MonthlyBillingPerPa monthb = new MonthlyBillingPerPa(selectedMonth, selectedYear, pa, selectedPurchaserData, new List<EffortEntry>());
                monthlyBillingList.Add(monthb);

            }
            
            if (mb == null)
                mb = new MonthlyBilling();
            if(mb.MbPerPaList==null)
            mb.MbPerPaList = new List<MonthlyBillingPerPa>();

            mb.MbPerPaList = monthlyBillingList;

            MonthlyBillingPerPaBL mbPpabl = new MonthlyBillingPerPaBL();
            List<MonthlyBillingPerPa> tmp = new List<MonthlyBillingPerPa>();

            //fill MonthlyBilling PerPa with data
            foreach (MonthlyBillingPerPa mbPa in mb.MbPerPaList)
            {
                tmp.Add(SelectMb(mbPa));
            }
            mb.MbPerPaList = tmp;
            mb.RehaDays = MbDao.selectReha(selectedPurchaserData.Purchaser.Id, selectedMonth, selectedYear);

            //wenn keine Leistungseinträge vorhanden wird nichts berechnet
            if (mb.MbPerPaList == null || mb.MbPerPaList.Count == 0)
            {

            }
            else
            {
                
                //berechnet alles was für MB wichtig ist.
                calculateValues();
            }
            return mb;
        }

        

        /// <summary>
        /// for GUI selecting all Leistungseinträge and calculate all important values for MB
        /// 
        /// </summary>
        /// <param name="mb"></param>
        public void selectMB(MonthlyBilling mb)
        {

            this.mb = mb;
            mb.MbPerPaList = selectAllMbpa(mb);


            //wenn keine Leistungseinträge vorhanden wird nichts berechnet
            if(mb.MbPerPaList == null || mb.MbPerPaList.Count == 0)
            {

            }
            else
            {
                //berechnet alles was für MB wichtig ist.
                calculateValues();
            }
        }

        private List<MonthlyBillingPerPa> selectAllMbpa(MonthlyBilling mb)
        {
            List<MonthlyBillingPerPa> mbppa = new List<MonthlyBillingPerPa>();
            mbppa = MbDao.SelectMBperPa(mb);
            return mbppa;
        }

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// alle Monatsabrechnungen aus der DB holen
        /// </summary>
        /// <returns></returns>
        public List<MonthlyBilling> SelectAllMB()
        {
            List<MonthlyBilling> mbList = new List<MonthlyBilling>();
            mbList = MbDao.selectAllMB();

            foreach (MonthlyBilling mb in mbList)
            {
                CreateMb(mb);
                calculateValues();
            }
            return mbList;
        }
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// alle Monatsabrechnungen für eine bestimmte Periode aus der DB holen
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<MonthlyBilling> SelectMbPeriod(DateTime from, DateTime to)
        {
            List<MonthlyBilling> mbList = new List<MonthlyBilling>();
            mbList = MbDao.SelectPeriod(from, to);
            //fehlerhandling??
            return mbList;
        }
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// holt alle Monatsabrechnungen eines bestimmten AG aus der DB
        /// </summary>
        /// <param name="pur"></param>
        /// <returns></returns>
        public List<MonthlyBilling> SelectAllMB(Purchaser pur)
        {
            return MbDao.selectSpecificMB(pur);
        }
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// holt eine bestimmte Monatsabrechnung aus der DB (bestimmtes Monat für bestimmten AG)
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        public MonthlyBilling SelectSpecificMb(MonthlyBilling mb)
        {
            return null; //MbDao.SelectAllMB(mb);
        }



        public MonthlyBilling calculateAkontierung()
        {

            mb.BetragFestgelegterBedarf = mb.Purchaser.AssistenceDemand * mb.Purchaser.HourlyRatePayoff;
            mb.DifferenceToPreviousMonth = getDifferenceToPrevMonth();
            //DIfferenz Vormonat??
            mb.PayOut = mb.BetragFestgelegterBedarf - mb.Purchaser.InputIncome - mb.ContributionCareAllowance - mb.DifferenceToPreviousMonth;

            return mb;
        }


        //-----------------------------------------------------------------------------------------------------
        public void calculateValues()
        {
            getSums();
            //calculate Beitrag aus Pflegegeld
            if (mb.Purchaser.AssistenceDemand * (decimal)4.6 > (decimal)mb.Purchaser.CareAllowanceMaximum)
            {
                mb.ContributionCareAllowance = (decimal)mb.Purchaser.CareAllowanceMaximum;
            }
            else
            {
                mb.ContributionCareAllowance = mb.Purchaser.AssistenceDemand * (decimal)4.6;
            }

            //Akontierung
            calculateAkontierung();

            //Abrechnung
            calculateAbrechnung();

            //Differenz
            calculateDifferenz();

            //Stundenübersicht
            calculateHourOverview();
        }




        private void getSums()
        {
            getDemandHours();
            getAllKM();
        }

        /// <summary>
        /// Berechnen der KM Summen einer Monatsabrechnung und der KM Summen jedes einzelnen PA für diese MA 
        /// </summary>
        private void getAllKM()
        {
            foreach (MonthlyBillingPerPa mppa in mb.MbPerPaList)
            {
                foreach (EffortEntry ee in mppa.EffortList)
                {
                    mppa.BillableKm += ee.Km;
                    mppa.PrivateKm += ee.Km;
                }
                mb.SumKm += mppa.BillableKm;
            }            
        }

        private void calculateHourOverview()
        {
            mb.HourContingent = mb.Purchaser.AssistenceDemand * 12;
            mb.SoFarTookHours = getTookHours();
            mb.RemainingHours = mb.HourContingent - mb.SoFarTookHours;
            mb.CurrentHourDeposit = mb.Purchaser.AssistenceDemand - mb.SumHours;
        }

        private void calculateDifferenz()
        {
            mb.AmountActuallyPaid = mb.BetragFestgelegterBedarf - mb.Purchaser.InputIncome - mb.ContributionCareAllowance;
           
            mb.Difference = mb.AmountActuallyPaid - mb.PayOff;
        }

        private void calculateAbrechnung()
        {
            getSumHoursForThisMonth();
            mb.ConsumedHoursAmount = mb.SumHours * mb.Purchaser.HourlyRatePayoff;       
            mb.TravelExpences = mb.SumKm * mb.Purchaser.TravellingAllowanceKM;
            mb.PayOff = mb.ConsumedHoursAmount - mb.Purchaser.InputIncome - mb.ContributionCareAllowance + mb.TravelExpences;           
        }

        /// <summary>
        /// Summe der Arbeitsstunden für dieses Monat
        /// </summary>
        private void getSumHoursForThisMonth()
        {
            foreach (MonthlyBillingPerPa mppa in mb.MbPerPaList)
            {
                if (mppa.EffortList != null)
                {
                    foreach (EffortEntry ee in mppa.EffortList)
                    {
                        //mppa.WorkingHours += (decimal)ee.Hours;
                    }
                }
                mb.SumHours += mppa.WorkingHours;
            }
        }

        //-----------------------------------------------------------------------------------------------------

        /// <summary>
        /// verbleibende Stunden im Stundenkontingent berechnen
        /// </summary>
        /// <returns></returns>
        private decimal getDemandHours()
        {
            List<MonthlyBilling> mList = new List<MonthlyBilling>();
            mList = MbDao.selectAllFrom(mb.Purchaser.Purchaser.ApprovalBegin);
            decimal s = 0;
           
            foreach (MonthlyBilling m in mList)
            {
                s += m.SumHours;
            }
            mb.SoFarTookHours = s;
            
            mb.demandHours =  mb.HourContingent-s;

            return mb.demandHours;
        }

        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// holt alle bereits benötigten Stunden des Stundenkontingents im Bewilligungszeitraum
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        private decimal getTookHours()
        {
            List<MonthlyBilling> mList = new List<MonthlyBilling>();
            mList = MbDao.selectAllFrom(mb.Purchaser.Purchaser.ApprovalBegin);
            decimal sum = 0;
            foreach (MonthlyBilling m in mList)
            {
               // calculateValues(m);

                sum += m.ConsumedHoursAmount;
            }
            return sum;
        }

        //-----------------------------------------------------------------------------------------------------
       /// <summary>
       /// holt die Differenz aus dem Vormonat
       /// </summary>
       /// <param name="mb"></param>
       /// <returns></returns>
        private decimal getDifferenceToPrevMonth()
        {
            List<MonthlyBilling> mList = new List<MonthlyBilling>();
            mList = MbDao.selectAllFrom(mb.Purchaser.Purchaser.ApprovalBegin);
            foreach (MonthlyBilling m in mList)
            {
                if (m.Year == mb.Year && m.Month + 1 == mb.Month)
                {
                    return m.Difference;
                }
            }
            return 0;
        }
        //-----------------------------------------------------------------------------------------------------
        public bool CreateMb(MonthlyBilling m)
        {
           // m = MbDao.InsertMonthlyBilling(m);
            foreach (MonthlyBillingPerPa mbp in m.MbPerPaList)
            {
                //ermittelt die KM und Stunden Summen
                if (mbp.EffortList != null)
                {
                    foreach (EffortEntry ee in mbp.EffortList)
                    {
                        DateTime currentDate = new DateTime(ee.Year, ee.Month, ee.Day);
                        if (IsNightShift(ee.A1))
                        {
                            m.SumHours += 4;
                        }
                        else if (IsHoliday(currentDate) || IsNight(ee.From, ee.To))
                        {
                            TimeSpan diff = ee.To.Subtract(ee.From);
                            m.SumHours += (decimal)((diff.TotalHours * 1.5));
                        }
                        else
                        {
                            TimeSpan diff = ee.To.Subtract(ee.From);
                            m.SumHours += (int)(diff.TotalHours);
                        }
                        m.SumKm += ee.Km;
                    }
                }
            }
            return true;
        }
        //-----------------------------------------------------------------------------------------------------
        public bool IsNightShift(Activity activity)
        {
            return (activity.Name == Activity.State.Nachtbereitschaft);
        }
        //-----------------------------------------------------------------------------------------------------
        public bool IsNight(DateTime dateTime1, DateTime dateTime2)
        {
            DateTime nightFrom = new DateTime(dateTime1.Year,dateTime1.Month,dateTime1.Day,22,00,00);
            DateTime nightTo = new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, 06, 00, 00);
            return (dateTime1.Hour >= nightFrom.Hour || dateTime2.Hour >= nightFrom.Hour);
        }
        //-----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Überprüft ob es sich bei dem Tag um einen Sonntag oder Feiertag handelt
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime currentDate)
        {
            HolidayLogic austrianHolidays = HolidayLogic.GetInstance(currentDate.Year);
            String d = currentDate.ToString("dddd");
            bool isH = austrianHolidays.IsHoliday(currentDate);
            return currentDate.ToString("dddd") == "Sonntag" || austrianHolidays.IsHoliday(currentDate);
        }
        //-----------------------------------------------------------------------------------------------------
        public bool UpdateMb(MonthlyBilling mb)
        {
            if (MbDao.UpdateMonthlyBilling(mb))
                return true;
            return false;
        }


        public MonthlyBilling InsertMonthlyBilling(MonthlyBilling mb)
        {
            foreach (MonthlyBillingPerPa mbpa in mb.MbPerPaList)
            {
                mb.MbPerPaList.Add(InsertMonthlyBilling(mbpa));
            }
            return mb;
        }

        public MonthlyBillingPerPa InsertMonthlyBilling(MonthlyBillingPerPa mb)
        {
            return MbDao.InsertMonthlyBilling(mb);
        }

        public bool UpdateMonthlyBillingEntry(MonthlyBillingPerPa mb)
        {
            return MbDao.UpdateMonthlyBillingEntry(mb);
        }

        public List<MonthlyBillingPerPa> SelectMBperPa(List<MonthlyBillingPerPa> mbppa)
        {
           return MbDao.SelectMBperPa(mb);

        }

        public MonthlyBilling SelectAllMB(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }

        public void deleteReha(int month, int year, long agid, int days)
        {
            MbDao.DeleteReha(month, year, agid, days);
        }

        public void insertReha(long agid, int month, int year, int days)
        {
            MbDao.InsertReha(agid, month, year, days);
        }
    }
}


