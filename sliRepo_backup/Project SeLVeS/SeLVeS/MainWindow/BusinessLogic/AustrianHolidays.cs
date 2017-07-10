using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
    public class AustrianHoliday : IComparable<AustrianHoliday>
    {
        private bool _isFix;
        private DateTime _date;
        private string _name;

        public AustrianHoliday(bool isFix, DateTime datum, string name)
        {
            this.IsFix = isFix;
            this.Date = _date;
            this.Name = name;

        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }   
        } 
        public bool IsFix
        {
            get
            {
                return _isFix;
            }
            set
            {
                _isFix = value;
            }
        }


        #region IComparable<AustrianHolidays> Member

        public int CompareTo(AustrianHoliday other)
        {
            return this._date.Date.CompareTo(other._date.Date);
        }

        #endregion
    }

    public class HolidayLogic
    {
        private static HolidayLogic _instance;
        private List<AustrianHoliday> _holidays;
        private int _year;

        /// <summary>
        /// Beschreibung: 
        /// </summary>
        public int CurrentYear
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
            }
        }
	
        public static HolidayLogic GetInstance(int year)
        {
            if (_instance == null || year != _instance.CurrentYear)
            {                
                _instance = new HolidayLogic(year);
                return _instance;
            }
            return _instance;
        }

        /// <summary>
        /// Beschreibung: Gibt variable Feiertage zurueck
        /// </summary>
        public List<AustrianHoliday> VariableHolidays
        {
            get
            {
                return _holidays.FindAll(delegate(AustrianHoliday f) { return !f.IsFix; });
            }
            
        }

        public bool IsHoliday(DateTime value)
        {
            return (_holidays.Find(delegate(AustrianHoliday f) { return f.Date.Date == value.Date; }) != null);
        }

        public AustrianHoliday GetHolidayName(DateTime value)
        {
            return (_holidays.Find(delegate(AustrianHoliday f) { return f.Date.Date == value.Date; }));
        }
        /// <summary>
        /// Beschreibung: gibt feste Feiertage zurueck
        /// </summary>
        public List<AustrianHoliday> FixedHolidays
        {
            get
            {
                return _holidays.FindAll(delegate(AustrianHoliday f) { return f.IsFix; });
            }
        }
	
        private HolidayLogic (int year)
        {
            this.CurrentYear = year;
            #region fillList
            this._holidays = new List<AustrianHoliday>();
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 1, 1), "Neujahr"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 1, 6), "Heilige Drei Könige"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 5, 1), "Staatsfeiertag"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 8, 15), "Mariä Himmelfahrt"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 10, 26), "Nationalfeiertag"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 12, 8), "Maria Empfängnis"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 11, 1), "Allerheiligen "));
            //this.holidays.Add(new AustrianHoliday(true, new DateTime(year, 12, 24), "Heiliger Abend"));//??
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 12, 25), "Weihnachten"));
            this._holidays.Add(new AustrianHoliday(true, new DateTime(year, 12, 26), "Stefanitag"));
            //this.holidays.Add(new AustrianHoliday(true, new DateTime(year, 12, 31), "Silvester"));//??
            DateTime osterSonntag = GetOsterSonntag();
            this._holidays.Add(new AustrianHoliday(false, osterSonntag, "Ostersonntag"));
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(-3), "Gründonnerstag"));
            //this.holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(-2), "Karfreitag")); //??
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(1), "Ostermontag"));
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(39), "Christi Himmelfahrt"));
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(49), "Pfingstsonntag"));
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(50), "Pfingstmontag"));
            this._holidays.Add(new AustrianHoliday(false, osterSonntag.AddDays(60), "Fronleichnam"));


            #endregion
        }

        private DateTime GetOsterSonntag()
        {
           
            int  g,h,c,j,l,i;

            g = _year % 19;
            c = this._year / 100;
            h = ((c-(c/4)) - (((8*c)+13)/25) + (19*g) + 15) % 30;
            i = h - (h/28) *(1- (29/(h+1)) * ((21-g)/11));
            j = (_year + (_year / 4) + i + 2 - c + (c / 4)) % 7;

            l = i - j;
            int month = (int)(3+ ((l+40)/44));
            int day = (int)(l + 28 - 31 * (month / 4));
            
            return new DateTime(_year, month, day);

        }
    }
}


