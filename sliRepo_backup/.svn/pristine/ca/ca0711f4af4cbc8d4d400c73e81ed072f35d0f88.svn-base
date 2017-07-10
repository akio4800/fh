using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    /// <summary>
    /// author: ZK
    /// </summary>
    public class Adress
    {
        public long AdressId { get; set; }
        public String Street{ get; set; }
        public int HouseNumber { get; set; }
        public int DoorNumber { get; set; }
        public int StairNumber { get; set; }
        public int Etage { get; set; }
        public int ZipCode { get; set; }
        public String City { get; set; }
        public String Country {  get; set; }

        public Adress(String street, int houseNum, int zip, String city)
        {
            this.Street = street;
            this.HouseNumber = HouseNumber;
            this.ZipCode = zip;
            this.City = city;
        }
        
        public String ConvertToString()
        {
            String s = "";
            
            if (this.Street != null)
            {
                if(!this.Street.Equals(""))
                    s += this.Street + " ";
            }

            if (this.HouseNumber != 0)
            {
                s += this.HouseNumber + ", ";
            }

            if (this.StairNumber != 0)
            {
                s += this.StairNumber + ", ";
            }

            if (this.Etage != 0)
            {
                s += this.Etage + ", ";
            }

            if (this.DoorNumber != 0)
            {
                s += this.DoorNumber + ", ";
            }

            if (this.ZipCode != 0)
            {
                s += this.ZipCode + ", ";
            }

            if (this.City != null )
            {
                if(!this.City.Equals(""))
                    s += this.City + ", ";
            }

            if (this.Country != null)
            {
                if(!this.Country.Equals(""))
                s += this.Country;
            }

            
            return s;
        }

        public Adress()
        {
            
        }
    }
}
