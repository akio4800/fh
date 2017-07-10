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
using System.Windows.Shapes;

namespace SelvesSoftware.GUI.Personenverwaltung.AG.Windows
{
    /// <summary>
    /// Interaction logic for GlobalsWindows.xaml
    /// </summary>
    public partial class GlobalsWindows : Window
    {

        public decimal Stundensatz { get; set; }
        public decimal StundensatzAusz { get; set; }
        public decimal FahrtkostenzusatzKM { get; set; }



        public decimal StundensatzOld { get; set; }
        public decimal StundensatzAuszOld { get; set; }
     
        public decimal FahrtkostenzusatzKMOld { get; set; }

        public bool changed;

        public GlobalsWindows()
        {
            InitializeComponent();
        }




        public GlobalsWindows(decimal h, decimal hAus,  decimal fahrtKM)
        {
            InitializeComponent();
            Stundensatz = h;
            StundensatzAusz = hAus;
       
            FahrtkostenzusatzKM = fahrtKM;


            StundensatzOld = h;
            StundensatzAuszOld = hAus;
           
            FahrtkostenzusatzKMOld = fahrtKM;
            fillFields();
            changed = false;

        }

        private void fillFields()
        {
            iptHSatz.Text = StundensatzOld.ToString();
            iptHSatzAusz.Text = StundensatzAuszOld.ToString();           
            iptFahrtKM.Text = FahrtkostenzusatzKMOld.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Die Änderungen werden sofort mit diesem Monat aktiv! Wollen Sie wirklich speichern?",
                       "Achtung!",
                       MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.No:
                    fillFields();
                    break;
                case MessageBoxResult.Yes:
                    Stundensatz = Convert.ToDecimal(iptHSatz.getContent());
                    StundensatzAusz = Convert.ToDecimal(iptHSatzAusz.getContent());
                    FahrtkostenzusatzKM = Convert.ToDecimal(iptFahrtKM.getContent());
                    changed = true;
                    this.Close();
                    break;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
