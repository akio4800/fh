using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.DataContainer
{
    public class PurchaserData
    {
        private PurchaserData _pur;

        public PurchaserData()
        {
            Purchaser = new Purchaser();
        }

        public PurchaserData(PurchaserData pur)
        {
            this._pur = pur;
        }

        public PurchaserData(int id)
        {
            this.Purchaser = new Purchaser();
            this.Purchaser.Id = id;
        }

        public int Month { get; set; }
        public int Year { get; set; }
        public Purchaser Purchaser { get; set;}
        //-----------Variable Daten-------------
        /// <summary>
        /// Stundensatz Auszahlung
        /// </summary>
        public decimal HourlyRatePayoff { get; set; }

        /// <summary>
        /// Stundensatz
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Beitrag Einkommen
        /// </summary>
        public decimal InputIncome { get; set; }

        

        /// <summary>
        /// Betreuungsbedarf in Stunden
        /// </summary>
        public decimal AssistenceDemand { get; set; }

        /// <summary>
        /// Einkommen pro Monat
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// Pflegegeldstufe
        /// </summary>
        public decimal CareAllowance { get; set; }

        /// <summary>
        /// Fahrtkostenzusatz
        /// </summary>
        public decimal TravellingAllowance { get; set; }


        /// <summary>
        /// Fahrtkostenzusatz pro Kilometer
        /// </summary>
        public decimal TravellingAllowanceKM { get; set; }

        /// <summary>
        /// Höchstgrenze für Kostenbeitrag aus Pflegegeld
        /// </summary>
        public decimal CareAllowanceMaximum { get; set; }
    
        internal bool Empty()
        {
            return true;
        }

        //DataBinding
        public String Name { get { return this.Purchaser.FirstName + " " + this.Purchaser.LastName; } }
  
    }
}
