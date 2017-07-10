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
using SelvesSoftware.DataContainer;
using SelvesSoftware.DB;
using SelvesSoftware.GUI.Monatsabrechnung.Windows;

namespace SelvesSoftware.GUI.Monatsabrechnung
{
    /// <summary>
    /// Interaktionslogik für MABearbeiten.xaml
    /// </summary>
    public partial class MABearbeiten : Page
    {
        public int selectedMonth;
        public int selectedYear;
        public PersonalAssistant selectedPersonalAssistent;
        public PurchaserData selectedPurchaserData;
        public MonthlyBilling selectedMonthlyBilling;

        IPurchaserDataBl pdBl = new PurchaserDataBl();
        IPersonalAssistentBl paBl = new PersonalAssistentBl();
        IMonthlyBillingBl mbBl = new MonthlyBillingBl();

        Dictionary<PersonalAssistant, String> errorList = new Dictionary<PersonalAssistant, string>();

        public MABearbeiten()
        {
            InitializeComponent();
            Bearbeiten.IsEnabled = false;
            getPurchaserList();
            
         
            ///<summary>
            ///wird in Klasse Lestungaufzeichnung verschoben 
            /// </summary>
            
            /*
            //activity1.Items.Add("Nachtbereitschaft");
            //activity1.Items.Add("Unterstützung bei der Grundversorgung");
            //activity1.Items.Add("Hauswirtschafltiche Tätigkeiten");
            //activity1.Items.Add("Begleitung und Mobilität");
            //activity1.Items.Add("Freizeitgestaltung");
            //activity1.Items.Add("Unterstützung bei jeder Form der Kommunikation");

            //activity2.Items.Add("Unterstützung bei der Grundversorgung");
            //activity2.Items.Add("Hauswirtschafltiche Tätigkeiten");
            //activity2.Items.Add("Begleitung und Mobilität");
            //activity2.Items.Add("Freizeitgestaltung");
            //activity2.Items.Add("Unterstützung bei jeder Form der Kommunikation");

            //activity3.Items.Add("Unterstützung bei der Grundversorgung");
            //activity3.Items.Add("Hauswirtschafltiche Tätigkeiten");
            //activity3.Items.Add("Begleitung und Mobilität");
            //activity3.Items.Add("Freizeitgestaltung");
            //activity3.Items.Add("Unterstützung bei jeder Form der Kommunikation");
            */
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "MONATSABRECHNUNG";
                }
            }
        }



        //Choose Purchaser + MBList -----------------------------------------------------------------------
        public List<PurchaserData> purchaserList;
        public List<MonthlyBilling> availableMBs;

        private void getPurchaserList()
        {
            purchaserList=pdBl.SelectAllPurchaser();
            purchaserBox.ItemsSource = purchaserList;
            purchaserBox.DisplayMemberPath = "Name";
            
        }


        private void purchaserSelected(object sender, SelectionChangedEventArgs e)
        {
            selectedPurchaserData = pdBl.Select(((PurchaserData)purchaserBox.SelectedItem));
            getYears();
            if (yearBox.SelectedIndex != Years.Count - 1)
            {
                yearBox.SelectedIndex = Years.Count - 1;
            }
            else
            {
                yearSelectionChanged(sender, e);
            }
            if (yearBox.SelectedIndex != Years.Count - 1)
            {
                monthBox.SelectedIndex = Months.Count - 1;
            }
            else
            {
                monthSelectionChanged(sender, e);
            }
            loadReha();
            
            

        }

        private void getWarnings()
        {
            if (selectedMonthlyBilling != null )
            {
                if (selectedMonthlyBilling.MbPerPaList != null)
                {
                    foreach (MonthlyBillingPerPa mppa in selectedMonthlyBilling.MbPerPaList)
                    {
                        if (mppa.EffortList.Count != 0)
                        {
                            errorList.Add(mppa.Pa, "Für diesen Persönlichen Assistenten existiert in diesem Monat noch keine Leistungsaufzeichnung");
                        }
                    }

                    showWarnings();
                }
            }
        }

        private void showWarnings()
        {
            foreach(PersonalAssistant pa in currentAssistents)
            {
                if (errorList.ContainsKey(pa))
                {
           
                  
                    //personalAssistentsBox.Items[currentAssistents.IndexOf(pa)].BackColor = Color.Red;

                }
            }
            personalAssistentsBox.ItemsSource = currentAssistents;
            personalAssistentsBox.DisplayMemberPath = "Name";

        }

        //Choose Month / Year --------------------------------------------------------------------
        List<int> Years;
        List<int> Months;
        //workaround
        MonthlyBillingDAO mbdao = new MonthlyBillingDAO();

        private void getYears()
        {

            //workaround :/
            Years=mbdao.getMBYears(selectedPurchaserData.Purchaser);

                if (selectedPurchaserData.Purchaser.Active && !Years.Contains(System.DateTime.Now.Year))
                {
                    Years.Add(System.DateTime.Now.Year);
                }
            Years.Sort();
            yearBox.ItemsSource = Years;
            getMonths();
        }

        private void getMonths()
        {
            int lastMonth = (System.DateTime.Now.Month != 1) ? System.DateTime.Now.Month - 1 : 12;

            //workaround
            Months = mbdao.getMBMonths(selectedPurchaserData.Purchaser, selectedYear);

            if (selectedPurchaserData.Purchaser.Active)
            {
                if (!Months.Contains(System.DateTime.Now.Month))
                {
                    Months.Add(System.DateTime.Now.Month);
                }
                if (!Months.Contains(lastMonth))
                {
                    Months.Add(lastMonth);
                }
            }
            Months.Sort();
            monthBox.ItemsSource = Months;

            //this is only vorübergehend
            /*
            int start = (selectedYear == ((DateTime)selectedPurchaserData.Purchaser.EntryDate).Year)? ((DateTime)selectedPurchaserData.Purchaser.EntryDate).Month : 1;
            int end = (selectedYear == System.DateTime.Now.Year)? System.DateTime.Now.Month : 12;

                for(int i = start; i <= end; i++)
                {
                    Months.Add(i);
                }
            */

         

        }


       private void monthSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (monthBox.SelectedItem == null) monthBox.SelectedIndex = monthBox.Items.Count - 1;
            selectedMonth = (int)monthBox.SelectedItem;
            showPersonalAssistants();
            
            showPurchaserData();
            selectedMonthlyBilling.RehaDays= mbdao.selectReha(selectedPurchaserData.Purchaser.Id, selectedMonth, selectedYear);
            loadReha();
            getWarnings();
        }

        private void yearSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedYear = (int)yearBox.SelectedItem;
            getMonths();
            
        }

        /// get MonthlyBilling
        /// </summary>
        private void showPurchaserData()
        {
            selectedMonthlyBilling = new MonthlyBilling();           
            selectedMonthlyBilling = mbBl.SelectMb(selectedYear, selectedMonth, selectedPurchaserData, currentAssistents);
                selectedMonthlyBilling.Purchaser = selectedPurchaserData;

            tbFahrtkostenAnzahl.Text = selectedMonthlyBilling.SumKm.ToString();

            if(selectedMonthlyBilling.Purchaser!= null) tbAnzahlfestgelegterBetrag.Text = selectedMonthlyBilling.Purchaser.AssistenceDemand.ToString();

            tbBetragfestgelegterBetrag.Text = selectedMonthlyBilling.BetragFestgelegterBedarf.ToString();
            tbBetragAusEK.Text = selectedMonthlyBilling.Purchaser.InputIncome.ToString();
            tbBetragAusPG.Text = selectedMonthlyBilling.ContributionCareAllowance.ToString();
            tbDiffVormonatBetrag.Text = selectedMonthlyBilling.DifferenceToPreviousMonth.ToString();
            tbAuszahlunsbetrag.Content = selectedMonthlyBilling.PayOut.ToString();
            tbAnzahltatsVerbrStunden.Text = selectedMonthlyBilling.SumHours.ToString();
            tbBetrtasVerbrStunden.Text = selectedMonthlyBilling.ConsumedHoursAmount.ToString();
            tbBetragAusEKAbr.Text = selectedMonthlyBilling.Purchaser.InputIncome.ToString();
            tbBetragAusPGAbr.Text = selectedMonthlyBilling.ContributionCareAllowance.ToString();
            tbFahrtkostenAnzahl.Text = selectedMonthlyBilling.SumKm.ToString();
            tbBetragFahrtkosten.Text= selectedMonthlyBilling.TravelExpences.ToString();
            tbAbrechnungsbetrag.Content = selectedMonthlyBilling.PayOff.ToString();
            tbausbezBetrag.Text = selectedMonthlyBilling.AmountActuallyPaid.ToString();
            tbabgerechnBetrag.Text = selectedMonthlyBilling.PayOff.ToString();
            tbDiff.Content = selectedMonthlyBilling.Difference.ToString();
            tbStundenkontingent.Text = selectedMonthlyBilling.HourContingent.ToString();
            tbbisherBeansprStunden.Text = selectedMonthlyBilling.SoFarTookHours.ToString();
            tbverblStunden.Text = selectedMonthlyBilling.RemainingHours.ToString();
            tbStundenguthaben.Text = selectedMonthlyBilling.CurrentHourDeposit.ToString();

            assistenceDemand.Text = selectedMonthlyBilling.Purchaser.AssistenceDemand.ToString();
            travellingAllowanceKM.Text = selectedMonthlyBilling.Purchaser.TravellingAllowanceKM.ToString();
            income.Text = selectedMonthlyBilling.Purchaser.Income.ToString();
            careAllowance.Text = selectedMonthlyBilling.Purchaser.CareAllowance.ToString();
            hourlyRate.Text = selectedMonthlyBilling.Purchaser.HourlyRate.ToString();
            hourlyRatePayoff.Text = selectedMonthlyBilling.Purchaser.HourlyRatePayoff.ToString();
            inputIncome.Text = selectedMonthlyBilling.Purchaser.InputIncome.ToString();
            careAllowanceMaximum.Text = selectedMonthlyBilling.Purchaser.CareAllowanceMaximum.ToString();
            rehaDays.Text = selectedMonthlyBilling.RehaDays.ToString();
            loadReha();
        }

        //Choose Assistent -----------------------------------------------------------------------
        public List<PersonalAssistant> currentAssistents;

        private void getcurrentPersonalAssistantList()
        {
            currentAssistents = new List<PersonalAssistant>();
            foreach (EmploymentStatus e in selectedPurchaserData.Purchaser.Employees)
            {
                currentAssistents.Add(e.Assistant);
            }
        }

        private void getselectedPersonalAssistantList()
        {
            currentAssistents= mbdao.getPAsFromMB(selectedPurchaserData.Purchaser, selectedYear, selectedMonth);
        }

        private void showPersonalAssistants()
        {
            //IF this year and this or last month 
            if((selectedMonth == System.DateTime.Now.Month || selectedMonth == System.DateTime.Now.Month-1) && selectedYear == System.DateTime.Now.Year) 
            {
                getcurrentPersonalAssistantList();
            }
            else
            {
                getselectedPersonalAssistantList();
            }

            personalAssistentsBox.ItemsSource = currentAssistents;
            personalAssistentsBox.DisplayMemberPath = "Name";


        }

        private void PaSelected(object sender, MouseButtonEventArgs e)
        {
            openLAA();
        }

        private void PABearbeitenClick(object sender, RoutedEventArgs e)
        {
            if (personalAssistentsBox.SelectedItem != null)
            {
                openLAA();
            }
        }

        private void openLAA()
        {
            selectedPersonalAssistent = ((PersonalAssistant)personalAssistentsBox.SelectedItem);

            LAAuswählen la = new LAAuswählen(selectedPersonalAssistent, selectedPurchaserData.Purchaser, selectedMonth, selectedYear);
            la.ShowDialog();
            showPurchaserData();
        }

        private void PABoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(personalAssistentsBox.SelectedItem == null)
            {
                Bearbeiten.IsEnabled = false;
            }
            else
            {
                Bearbeiten.IsEnabled = true;
            }
        }

       

        private void rehaCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (rehaDays.Visibility == Visibility.Hidden)
            {
                rehaDays.Visibility = Visibility.Visible;
                rehaDaysLabel.Visibility = Visibility.Visible;
                saveRehaButton.Visibility = Visibility.Visible;
            } else if(selectedMonthlyBilling!=null && selectedMonthlyBilling.RehaDays>0 && rehaDays.Visibility==Visibility.Visible)
            {
                switch (MessageBox.Show("Wollen Sie diese Reha wirklich löschen?",
                        "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.No:
                        rehaCheckBox.IsChecked=true;
                        break;
                    case MessageBoxResult.Yes:
                        rehaDaysLabel.Visibility = Visibility.Hidden;
                        rehaDays.Visibility = Visibility.Hidden;
                        saveRehaButton.Visibility = Visibility.Hidden;
                        selectedMonthlyBilling.RehaDays = 0;
                        rehaDays.Text = "";
                        mbBl.deleteReha(selectedMonth, selectedYear, selectedMonthlyBilling.Purchaser.Purchaser.Id,selectedMonthlyBilling.RehaDays);
                        break;
                }
            } else
            {
                rehaDaysLabel.Visibility = Visibility.Hidden;
                rehaDays.Visibility = Visibility.Hidden;
                saveRehaButton.Visibility = Visibility.Hidden;
            }
            
        }

        private void loadReha()
        {
            if (selectedMonthlyBilling.RehaDays > 0)
            {
                rehaCheckBox.IsChecked = true;
                rehaDays.Visibility = Visibility.Visible;
                rehaDaysLabel.Visibility = Visibility.Visible;
                saveRehaButton.Visibility = Visibility.Visible;
                rehaDays.Text = selectedMonthlyBilling.RehaDays.ToString();
            } else
            {
                rehaCheckBox.IsChecked = false;
                rehaDays.Visibility = Visibility.Hidden;
                rehaDaysLabel.Visibility = Visibility.Hidden;
                saveRehaButton.Visibility = Visibility.Hidden;
                rehaDays.Background = Brushes.White;
                rehaDays.Text = "";
            }
        }

        private void saveRehaButton_Click(object sender, RoutedEventArgs e)
        {
            if(rehaDays.Text!=null && !rehaDays.Text.Equals(""))
            {
                try {
                    int reha = Int32.Parse(rehaDays.Text);
                    rehaDays.Background = Brushes.White;
                    mbBl.insertReha(selectedMonthlyBilling.Purchaser.Purchaser.Id, selectedMonth, selectedYear, reha);
                } catch(FormatException ex)
                {
                    rehaDays.Background = Brushes.LightPink;
                }
                
            }
        }
    }

}
