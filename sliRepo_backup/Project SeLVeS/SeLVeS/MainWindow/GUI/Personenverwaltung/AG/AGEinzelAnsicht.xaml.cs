using SelvesSoftware.BusinessLogic;
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
using SelvesSoftware.DataContainer;
using System.Collections.ObjectModel;
using SelvesSoftware.GUI.Personenverwaltung.PA;
using SelvesSoftware.GUI.Personenverwaltung.AG.Windows;

namespace SelvesSoftware.GUI.Personenverwaltung.AG
{
    /// <summary>
    /// Interaktionslogik für AGEinzelAnsicht.xaml
    /// </summary>
    public partial class AGEinzelAnsicht : Page
    {
        private ContactWindow contactW;
        private ObservableCollection<String> Data = new ObservableCollection<String>();
        private IPurchaserDataBl _bl_p;

        public AGEinzelAnsicht()
        {
            

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "AUFTRAGGEBER STAMMDATEN";


                }
            }
            InitializeComponent();
            CareAllowence.Items.Add("0");
            CareAllowence.Items.Add("1");
            CareAllowence.Items.Add("2");
            CareAllowence.Items.Add("3");
            CareAllowence.Items.Add("4");
            CareAllowence.Items.Add("5");
            CareAllowence.Items.Add("6");
            CareAllowence.Items.Add("7");
            
            getPurchaser();
        }

        private InfoWindow infoW;

        public IPurchaserDataBl bl
        {
            get
            {
                if (_bl_p == null)
                {
                    _bl_p = new PurchaserDataBl();
                }
                return _bl_p;
            }
            set
            {
                _bl_p = value;
            }
        }

        private PurchaserData pd { get; set; }





        private void getPurchaser()
        {
           

            var a = App.Current as App;
            int AGidx = ((int)(a.AGIndex));

            pd = bl.SelectPurchaser(AGidx);
            FirstName.Text = pd.Purchaser.FirstName;
            LastName.Text = pd.Purchaser.LastName;
            Email.Text = pd.Purchaser.EMail;
            PhoneNr.Text = pd.Purchaser.PhoneNumber;
            MobileNr.Text = pd.Purchaser.MobilePhone;
            SVN.Text = (pd.Purchaser.SVN == 0) ? " " : pd.Purchaser.SVN.ToString();
            Street.Text = pd.Purchaser.HomeAdress.Street;
            HNr.Text = pd.Purchaser.HomeAdress.HouseNumber.ToString();
            Etage.Text = pd.Purchaser.HomeAdress.Etage.ToString();
            StairNr.Text = pd.Purchaser.HomeAdress.StairNumber.ToString();
            DoorNr.Text = pd.Purchaser.HomeAdress.DoorNumber.ToString();
            City.Text = pd.Purchaser.HomeAdress.City;
            Country.Text = pd.Purchaser.HomeAdress.Country;
            ZIP.Text = pd.Purchaser.HomeAdress.ZipCode.ToString();
            nationality.Text = pd.Purchaser.nationality;
            districtcommission.Text = pd.Purchaser.DistrictCommision;

            if (pd.Purchaser.EntryDate != null) { EntryDate.Text = pd.Purchaser.EntryDate.Value.ToShortDateString(); }
            if (pd.Purchaser.ApprovalBegin != null) { ApprovelFrom.Text = pd.Purchaser.ApprovalBegin.Value.ToShortDateString(); }
            if (pd.Purchaser.ApprovalEnd != null) { ApprovelTo.Text = pd.Purchaser.ApprovalEnd.Value.ToShortDateString(); }

            PayperHour.Text = pd.HourlyRate.ToString();
            KmPay.Text = pd.TravellingAllowanceKM.ToString();
            billablePayperHour.Text = pd.HourlyRatePayoff.ToString();
            Needof.Text = pd.AssistenceDemand.ToString();
            Income.Text = pd.Income.ToString();
            LimitKB.Text = pd.CareAllowanceMaximum.ToString();
            IncomeBetrag.Text = pd.InputIncome.ToString();
            CareAllowence.Text = pd.CareAllowance.ToString();


            IBAN.Text = pd.Purchaser.IBAN;
            BIC.Text = pd.Purchaser.BIC;
            kontoinhaber.Text = pd.Purchaser.AccountHolder;

            if (pd.Purchaser.Active)
            {
                activeRadioBtn.IsChecked = true;
                inactiveRadioBtn.IsChecked = false;
            }
            else
            {
                activeRadioBtn.IsChecked = false;
                inactiveRadioBtn.IsChecked = true;
            }

            if (pd.Purchaser.hasContract)
            {

                contract.IsChecked = true;

            }
            if (pd.Purchaser.hasIntroCourse)
            {
                einfuehrungskurs.IsChecked = true;
            }

            if (pd.Purchaser.Employees.Count != 0 || pd.Purchaser.Employees != null)
            {

                foreach (EmploymentStatus e in pd.Purchaser.Employees)
                {
                    String pa = e.Assistant.FirstName + " " + e.Assistant.LastName;
                    PAList.Items.Add(pa);
                }

            }
            else
            {
                PAList.Items.Add("Keine  Assistenten");
            }

            if (pd.Purchaser.ContactPerson == null || contactPersonEmpty())
            {
                btnContact.IsEnabled = false;
            }

            if (pd.Purchaser.InfoField == null || InfoEmpty())
            {
                btnInfo.IsEnabled = false;
            }
        }

        private bool InfoEmpty()
        {
            return pd.Purchaser.InfoField == null || pd.Purchaser.InfoField == "";
        }

        private bool contactPersonEmpty()
        {
            return pd.Purchaser.ContactPerson.FirstName == "" && pd.Purchaser.ContactPerson.LastName == "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGBearbeiten.xaml", UriKind.Relative);
                }
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGÜbersicht.xaml", UriKind.Relative);
                }
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
           
            infoW = new InfoWindow(pd.Purchaser.InfoField);
            infoW.btnSave.IsEnabled = false;
            infoW.tbInfo.Focusable = false;
            infoW.ShowDialog();
            //pd.Purchaser.InfoField = infoW.tbInfo.Text;
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            contactW = new ContactWindow(pd.Purchaser.ContactPerson);
            disableWindowContent();
            contactW.ShowDialog();
            //pd.Purchaser.ContactPerson = contactW.person;
        }

        private void disableWindowContent()
        {
            contactW.Email.Focusable = false;
            contactW.FirstName.Focusable = false;
            contactW.LastName.Focusable = false;
            contactW.PhoneNr.Focusable = false;
            contactW.MobileNr.Focusable = false;
            contactW.Email.Focusable = false;
            contactW.Street.Focusable = false;
            contactW.ZIP.Focusable = false;
            contactW.Country.Focusable = false;
            contactW.HNr.Focusable = false;
            contactW.DoorNr.Focusable = false;
            contactW.StairNr.Focusable = false;
            contactW.Etage.Focusable = false;
            contactW.tbInfo.Focusable = false;
            contactW.button.IsEnabled = false;
            contactW.City.Focusable = false;
        }
    }




}
