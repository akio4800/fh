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
using SelvesSoftware.BusinessLogic;

namespace SelvesSoftware.GUI.Monatsabrechnung.Windows
{
    /// <summary>
    /// Interaktionslogik für LAAuswählen.xaml
    /// </summary>
    public partial class LAAuswählen : Window
    {
        List<EffortEntry> eeList = new List<EffortEntry>();
        EffortEntry eest = new EffortEntry();
       



        IEffortEntryBL _bl;


        public IEffortEntryBL bl
        {
            get
            {
                if (_bl == null)
                {
                    _bl = new EffortEntryBl();
                }
                return _bl;
            }
            set
            {
                _bl = value;
            }



        }
        public LAAuswählen()
        {
            
            InitializeComponent();
            /*
            A1.Items.Add(Activity.State.Nachtbereitschaft);
            A1.Items.Add(Activity.State.Grundversorgung);
            A1.Items.Add(Activity.State.Hauswirtschaft);
            A1.Items.Add(Activity.State.Begleitung);
            A1.Items.Add(Activity.State.Freizeitgestaltung);
            A1.Items.Add(Activity.State.Kommunikation);


            List<EffortEntry> eeList = new List<EffortEntry>();
            EffortEntry ee = new EffortEntry(2015, 12, 15, new Purchaser(), new PersonalAssistant(), new DateTime(2015, 12, 15, 0, 0, 0), DateTime.Now, 10, new Activity(Activity.State.Nachtbereitschaft), new Activity(Activity.State.Begleitung), new Activity(Activity.State.Grundversorgung));
            EffortEntry ee2 = new EffortEntry(2015, 2, 2, new Purchaser(), new PersonalAssistant(), new DateTime(2015, 2, 2, 0, 0, 0), DateTime.Now, 100, new Activity(Activity.State.Kommunikation), new Activity(Activity.State.Begleitung), new Activity(Activity.State.Freizeitgestaltung));
            eeList.Add(ee);
            eeList.Add(ee2);

            monthBox.Text = "Mai";
            //      Übersicht.ItemsSource = eeList
            Übersicht.DataContext = eeList;

            */

        }

        public LAAuswählen(PersonalAssistant pa, Purchaser pur, int month, int year)
        {
            InitializeComponent();
            CenterWindowOnScreen();
            A1.Items.Add(Activity.State.Nachtbereitschaft);
            A1.Items.Add(Activity.State.Grundversorgung);
            A1.Items.Add(Activity.State.Hauswirtschaft);
            A1.Items.Add(Activity.State.Begleitung);
            A1.Items.Add(Activity.State.Freizeitgestaltung);
            A1.Items.Add(Activity.State.Kommunikation);

           
            A2.Items.Add(Activity.State.Grundversorgung);
            A2.Items.Add(Activity.State.Hauswirtschaft);
            A2.Items.Add(Activity.State.Begleitung);
            A2.Items.Add(Activity.State.Freizeitgestaltung);
            A2.Items.Add(Activity.State.Kommunikation);

            A3.Items.Add(Activity.State.Grundversorgung);
            A3.Items.Add(Activity.State.Hauswirtschaft);
            A3.Items.Add(Activity.State.Begleitung);
            A3.Items.Add(Activity.State.Freizeitgestaltung);
            A3.Items.Add(Activity.State.Kommunikation);



            if (pa != null && pur != null)
            {
                paName.Text = pa.FirstName + " " + pa.LastName;
                monthBox.Text = new DateTime(year, month, 1).ToString("MMMM yyyy"); ;
                eeList = bl.GetEntries(pa, pur, month, year);
                Übersicht.ItemsSource = eeList;

            }
        }


        private void loadData()
        {


        }

        private void Bearbeiten_Click(object sender, RoutedEventArgs e)
        {

            EffortEntry ee = (EffortEntry)Übersicht.SelectedItem;
            if (ee != null)
            {
                eest = ee;
                einzel.Visibility = Visibility.Visible;
                eeToText(eest);



            }


        }

        private void eeToText(EffortEntry ee)
        {
            day.Text = ee.Day.ToString();
            von.Text = ee.From.ToString("HH.mm");
            bis.Text = ee.To.ToString("HH.mm");
            KM.Text = ee.Km.ToString();
            A1.Text = ee.A1.Name.ToString();
            A1.Text = ee.A1.Name.ToString();
            A1.Text = ee.A1.Name.ToString();




        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }


        private void Entfernen_Click(object sender, RoutedEventArgs e)
        {
            if (Übersicht.SelectedItem != null)
            {
                eeList.Remove((EffortEntry)Übersicht.SelectedItem);
                Übersicht.Items.Refresh();

                EinzelÜbersicht.Items.Refresh();

                bl.deleteEntry((EffortEntry)Übersicht.SelectedItem);
            }

        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {

        }

        //THOMAS--------------------------------------------------------------------------------------------------

       private void updateEEintoBL()
        {

        }

        private void Fertig_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
