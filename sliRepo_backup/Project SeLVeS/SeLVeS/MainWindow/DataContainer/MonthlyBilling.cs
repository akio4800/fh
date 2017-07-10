using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware
{
    /// <summary>
    /// Zeigt alle Monatsabrechnungen eines AG von allen PAs eines Monats
    /// </summary>
    public class MonthlyBilling
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public PurchaserData Purchaser { get; set; }
        public List<MonthlyBillingPerPa> MbPerPaList { get; set; }


        #region PA

        public decimal SumKm { get; set; }


        /// <summary>
        /// Summe Stunden
        /// </summary>
        public decimal SumHours { get; set; }

        #endregion

        #region  Akontierung 


        /// <summary>
        /// Betrag festgelegter Bedarf = festgelegter Betreuungsbedarf in Stunden * Stundensatz zur Auszahlung
        /// Aktontierung
        /// </summary>
        public decimal BetragFestgelegterBedarf { get; set; }


        /// <summary>
        /// Beitrag aus Pflegegeld
        ///  Aktontierung + Abrechnung
        /// </summary>
        public decimal ContributionCareAllowance { get; set; }

        /// <summary>
        /// Differenz zum Vormonat
        ///  Aktontierung
        /// </summary>
        public decimal DifferenceToPreviousMonth { get; set; }

        /// <summary>
        /// Auszahlungsbetrag
        /// Akontierung
        /// </summary>
        public decimal PayOut { get; set; }

        #endregion

        #region  Abrechnung 
        /// <summary>
        /// tatsächlich verbrauchte Stunden
        /// Abrechnung
        /// </summary>
        public decimal ConsumedHoursAmount { get; set; }


        /// <summary>
        /// noch verbleibende Stunden bis zum Ende des Bewilligungszeitraums
        /// </summary>
        public decimal demandHours { get; set; }

        /// <summary>
        /// Betrag tasächlich verbrauchte Stunden = tatsächlich verbrauchte Stunden * Stundensatz zur Auszahlung
        /// Abrechnung
        /// </summary>
        public decimal BetragConsumedHours { get; set; }

        /// <summary>
        /// Fahrtkosten = Summe KM * Fahrtkostenzusatz/KM
        /// Abrechnung
        /// </summary>
        public decimal TravelExpences { get; set; }


        /// <summary>
        /// Abrechnungsbetrag
        /// Abrechnung + Differenz
        /// </summary>
        public decimal PayOff { get; set; }

        #endregion

        #region Differenz

        /// <summary>
        /// tatsächlich ausbezahlter Betrag 
        /// Differenz
        /// </summary>
        public decimal AmountActuallyPaid { get; set; }

        /// <summary>
        /// Differenz aus ausbezahltem und abgerechnetem Betrag
        /// </summary>
        public decimal Difference { get; set; }

        #endregion

        #region Stundenuebersicht

        /// <summary>
        /// Stundenkontingent f. Bewilligungszeitraum = Betreuungsbedarf/h * 12
        /// Stundenübersicht
        /// </summary>
        public decimal HourContingent { get; set; }


        /// <summary>
        /// bisher beanspruchte Stunden
        /// Stundenübersicht
        /// </summary>
        public decimal SoFarTookHours { get; set; }


        /// <summary>
        /// verbleibende Stunden bis Ende Bewilligungszeitraum
        /// Stundenübersicht
        /// </summary>
        public decimal RemainingHours { get; set; }


        /// <summary>
        /// aktuelles Stundenguthaben
        /// Stundenübersicht
        /// </summary>
        public decimal CurrentHourDeposit { get; set; }

        public int RehaDays { get; set; }

        #endregion

    }
}
