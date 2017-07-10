using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    /// <summary>
    /// author:zk
    /// This class represents the database table "dienst" and shows when a PA started travelling at SLI.
    /// It is outsourced from PersonalAssistent because one PA can have several Employments due to working breaks etc.
    /// There is no extra DAO class for Employmen, the functions are placed in the PaDAO
    /// </summary>
    public class Employment
    {
        public long EmplId { get; set; }
        /// <summary>
        /// Beginndatum des des aktiven Zeitraums
        /// </summary>
        public DateTime EmplBegin { get; set; }

        /// <summary>
        /// Enddatum des aktiven Zeitraums
        /// </summary>
        public DateTime EmplEnd { get; set; }

        public Employment(DateTime begin, DateTime end){
            EmplBegin = begin;
            EmplEnd = end;
        }

        public String GuiDate
        {
            get {
                String date = EmplBegin.ToShortDateString();
                    if (EmplEnd.Year != 1) {
                        date = date + " - " + EmplEnd.ToShortDateString();
                    }
                return date;
                }
        }

        public Employment() { }
    }
}
