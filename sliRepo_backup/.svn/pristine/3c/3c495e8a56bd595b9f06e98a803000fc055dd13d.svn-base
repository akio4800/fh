using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    /// <summary>
    /// author:zk
    /// This class represents the database table "dienstverhältnis" and shows which PA works for which AG currently.
    /// There is no extra DAO class for EmploymentStatus, the functions are placed in the PaDAO and purchaserDAO
    /// </summary>
    public class EmploymentStatus
    {
        public PersonalAssistant Assistant { get; set; }
        public Purchaser Purchaser { get; set; }
        // file dienstvertrag

        public EmploymentStatus(PersonalAssistant pa, Purchaser p)
        {
            Assistant = pa;
            Purchaser = p;
        }
        public EmploymentStatus() { }
    }
}
