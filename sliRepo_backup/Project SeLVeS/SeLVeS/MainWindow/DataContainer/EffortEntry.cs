using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    /// <summary>
    /// Ein Leistungseintrag für einen PA und einen Ag für ein bestimmtes Datum + Zeitraum
    /// </summary>
    public class EffortEntry
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public Purchaser Purchaser { get; set; }
        public PersonalAssistant Pa { get; set; }

        public double Factor { get; set; }

        public DateTime Date
        {
            get { return new DateTime(Year, Month, Day); }

            set
            {
                Year = value.Year;
                Month = value.Month;
                Day = value.Day;


            }
        }

        public String Weekday
        {get{return new DateTime(Year, Month, Day).ToString("dddd"); }}

        public int Day { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public double Hours { get { return Math.Round((To - From).TotalHours,1); } }
        public int Km { get; set; }
        public Activity A1 { get; set; }
        public Activity A2 { get; set; }
        public Activity A3 { get; set; }
        
     

        public EffortEntry(int year, int month, int day, Purchaser p, PersonalAssistant pa, DateTime from, DateTime to,
            int km)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Purchaser = p;
            this.Pa = pa;
            this.From = from;
            this.To = to;
            this.Km = km;
        }
        public EffortEntry(int year, int month, int day, Purchaser p, PersonalAssistant pa, DateTime from, DateTime to,
           int km, Activity a1, Activity a2, Activity a3)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Purchaser = p;
            this.Pa = pa;
            this.From = from;
            this.To = to;
            this.Km = km;
            this.A1 = a1;
            this.A2 = a2;
            this.A3 = a3;
                     
        }

        public EffortEntry() { }

    }

    /// <summary>
    /// Tätigkeit für die Leistungserfassung
    /// </summary>
    public class Activity
    {
       
        public enum State
        {
            Nachtbereitschaft,
            Grundversorgung,
            Hauswirtschaft,
            Begleitung,
            Freizeitgestaltung,
            Kommunikation
        }

        public State Name { get; set; }

        public Activity(State s)
        {
            Name = s;
        }

        public Activity() { }

    }
}
