using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow
{
    public class MonthlyBilling
    {
        public List<MonthlyBillingPerPA> mb { get; set; }
        public int sumKM { get; set; }
        public int sumHours { get; set; }
    }
}
