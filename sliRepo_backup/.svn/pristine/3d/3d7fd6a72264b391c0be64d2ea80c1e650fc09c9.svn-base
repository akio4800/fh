using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SelvesSoftware.BusinessLogic;

namespace SelvesSoftware.GUI.Monatsabrechnung
{
    /// <summary>
    /// Interaktionslogik für MAÜbersicht.xaml
    /// </summary>
    public partial class MAÜbersicht : Page
    {
        private IMonthlyBillingBl _bl;
        public IMonthlyBillingBl bl
        {
            get
            {
                if (_bl == null)
                {
                    _bl = new MonthlyBillingBl();
                }
                return _bl;
            }
            set
            {
                _bl = value;
            }
        }


        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Month { get; set; }
        public String Year { get; set; }
        public List<MonthlyBilling> MbList { get; set; }




        public MAÜbersicht()
        {
            InitializeComponent();
           
          
                MbList = bl.SelectAllMB();
              for (int i = 0; i < MbList.Count; ++i)
              {



                

           }

        }
    }
}
