using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trove_Stats.model
{
    class Stat
    {
        public Stat()
        {
        }

        public Stat(string type,  string value) {
            StatType = type;
            StatValue = value;

        }

       override public String ToString()
        {
            return StatValue + " " + StatType;
        }

       public string StatType { get; set; }
       public string StatValue { get; set; }


        
    

    }
}
