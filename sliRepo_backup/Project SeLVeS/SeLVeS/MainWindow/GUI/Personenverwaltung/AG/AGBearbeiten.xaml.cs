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
using SelvesSoftware.GUI.Elemente;
using SelvesSoftware.GUI.Personenverwaltung.AG.Windows;
using System.Collections.ObjectModel;
using System.Media;
using SelvesSoftware.GUI.Personenverwaltung.PA;
using SelvesSoftware.GUI.Personenverwaltung;
using static SelvesSoftware.GUI.GuiParser;

namespace SelvesSoftware.GUI
{
    /// <summary>
    /// Interaktionslogik für AGBearbeiten.xaml
    /// </summary>
    public partial class AgBearbeiten : Page
    {
        private InfoWindow infoW;
        private ContactWindow contactW;
		private int  AGidx;
        private IPurchaserDataBl _bl;
        public IPurchaserDataBl bl
        {
            get
            {
                if (_bl == null)
                {
                    _bl = new PurchaserDataBl();
                }
                return _bl;
            }
            set
            {
                _bl = value;
            }
        }
        private PurchaserData purData;
        //List with always actual PA
        public List<PersonalAssistant> employedPA = new List<PersonalAssistant>();
        public ObservableCollection<Track> guiListEmployed = new ObservableCollection<Track>();
        public List<PersonalAssistant> addedPAs = new List<PersonalAssistant>();
        public List<PersonalAssistant> deletedPAs = new List<PersonalAssistant>();
        private bool load = true;
      


        public AgBearbeiten()
        {
              
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "AUFTRAGGEBER BEARBEITEN";


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
            var a = App.Current as App;
            Console.WriteLine(a.AGIndex);
          
            if (a.AGIndex != 0)
            {

                getPurchaser();

            }
            load = false;
        }
       

        private void inactiveRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (!load)
            {
                //popup machen
                switch (MessageBox.Show("Wollen Sie diese Person wirklich inaktiv setzen?",
                "Achtung!",
                MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.No:
                        inactiveRadioBtn.IsChecked = false;
                        activeRadioBtn.IsChecked = true;

                        break;
                    case MessageBoxResult.Yes:

                        break;

                }

            }
        }

        private void getPurchaser()
        {
            var a = App.Current as App;
           AGidx = ((int)(a.AGIndex));


            purData = bl.SelectPurchaser(AGidx);
            fillFields(purData);
        }

        private void fillFields(PurchaserData pd)
        {
            FirstName.Text = pd.Purchaser.FirstName;
            LastName.Text = pd.Purchaser.LastName;
            Email.Text = pd.Purchaser.EMail;
            PhoneNr.Text = pd.Purchaser.PhoneNumber;
            MobileNr.Text = pd.Purchaser.MobilePhone;
            SVN.Text = (pd.Purchaser.SVN == 0) ? "" : pd.Purchaser.SVN.ToString();
            Street.Text = pd.Purchaser.HomeAdress.Street;
            HNr.Text = pd.Purchaser.HomeAdress.HouseNumber.ToString();
            Etage.Text = pd.Purchaser.HomeAdress.Etage.ToString();
            StairNr.Text = pd.Purchaser.HomeAdress.StairNumber.ToString();
            DoorNr.Text = pd.Purchaser.HomeAdress.DoorNumber.ToString();
            City.Text = pd.Purchaser.HomeAdress.City;
            Country.Text = pd.Purchaser.HomeAdress.Country;
            ZIP.Text = pd.Purchaser.HomeAdress.ZipCode.ToString();
            nationality.Text = pd.Purchaser.nationality;
            EntryDate.Text = pd.Purchaser.EntryDate.ToString();
            ApprovelFrom.Text = pd.Purchaser.ApprovalBegin.ToString();
            ApprovelTo.Text = pd.Purchaser.ApprovalEnd.ToString();
            PayperHour.Text = pd.HourlyRate.ToString();
            KmPay.Text = pd.TravellingAllowanceKM.ToString();
            billablePayperHour.Text = pd.HourlyRatePayoff.ToString();
            Needof.Text = pd.AssistenceDemand.ToString();
            Income.Text = pd.Income.ToString();


            IncomeBetrag.Text = pd.InputIncome.ToString();
            CareAllowence.Text = pd.CareAllowance.ToString();
            districtcommission.Text = pd.Purchaser.DistrictCommision;

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
            //employedPA.Clear();
            //guiListEmployed.Clear();
            employedPersonalAssistants.ItemsSource = guiListEmployed;
  
            if (pd.Purchaser.Employees != null)
            {
                foreach (EmploymentStatus emp in pd.Purchaser.Employees)
                {
                    employedPA.Add(emp.Assistant);
                    
                    guiListEmployed.Add(new Track(emp.Assistant.Id, emp.Assistant.FirstName, emp.Assistant.LastName));

                }
                employedPersonalAssistants.ItemsSource = guiListEmployed;

            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fillFields(purData);
            //DIRTY
            InputEvaluator ie = new InputEvaluator();
            ie.Evaluate(this);
            FirstName.Background = Brushes.White;
            LastName.Background = Brushes.White;
        }

        private void PAHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            PAAuswählen paaus = new PAAuswählen(this);
            paaus.ShowDialog();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
         
            Purchaser purU = new Purchaser();
           

            if (purData.Purchaser.ContactPerson != null)
            {
                purU.ContactPerson = new Person();
                purU.ContactPerson = purData.Purchaser.ContactPerson;
            }

            PurchaserData purDataU = new PurchaserData();
            purU.HomeAdress = new Adress();
            InputEvaluator ie = new InputEvaluator();
            

            if (ie.Evaluate(this))
            {
                purU.Id = AGidx;
                purU.FirstName = FirstName.getContent();
                purU.LastName = LastName.getmustContent();
                purU.EMail = Email.getContent();
                if (PhoneNr.getContent() != null)
                {
                    purU.PhoneNumber = PhoneNr.getContent();
                }
                if (MobileNr.getContent() != null)
                {
                    purU.MobilePhone = MobileNr.getContent();
                }
                purU.HomeAdress.AdressId = purData.Purchaser.HomeAdress.AdressId;
                purU.HomeAdress.Street = Street.getContent();
                purU.HomeAdress.StairNumber = parseInt(StairNr.getContent());
                purU.HomeAdress.HouseNumber = parseInt(HNr.getContent());
                purU.HomeAdress.Etage = parseInt(Etage.getContent());
                purU.HomeAdress.DoorNumber = parseInt(DoorNr.getContent());
                purU.HomeAdress.City = City.getContent();
                purU.HomeAdress.Country = Country.getContent();
                purU.HomeAdress.ZipCode = parseInt(ZIP.getContent());    
                purU.EntryDate = EntryDate.DisplayDate;
                purU.ApprovalBegin = ApprovelFrom.DisplayDate;
                purU.ApprovalEnd = ApprovelTo.DisplayDate;
                purU.DistrictCommision = districtcommission.getContent();
                purU.nationality = nationality.getContent();
                purU.SVN = parseLong(SVN.getContent());
                

                purDataU.HourlyRate = parseDecimal(PayperHour.getContent());
                purDataU.TravellingAllowanceKM = parseDecimal(KmPay.getContent());
                purDataU.HourlyRatePayoff = parseDecimal(billablePayperHour.getContent());
                purDataU.Income = parseDecimal(Income.getContent());
                purDataU.AssistenceDemand = parseInt(Needof.getContent());
                purDataU.InputIncome = parseDecimal(IncomeBetrag.getContent());
                purDataU.CareAllowance = parseInt((string)CareAllowence.SelectedValue);
                purDataU.Purchaser.InfoField = purData.Purchaser.InfoField;
                purDataU.Year = purData.Year;
                purDataU.Month = purData.Month;
                purU.IBAN = IBAN.getContent();
                purU.BIC = BIC.getContent();
                purU.AccountHolder = kontoinhaber.getContent();

                if ((bool)activeRadioBtn.IsChecked)
                {
                    purU.Active = true;
                }else if ((bool)inactiveRadioBtn.IsChecked)
                {
                    purU.Active = false;
                }


                if ((bool)einfuehrungskurs.IsChecked)
                {
                    purU.hasContract = true;
                }
                if ((bool)contract.IsChecked)
                {
                    purU.hasIntroCourse = true;
                }

                if (purDataU.Purchaser.Employees != null)
                {
                    foreach (EmploymentStatus emp in purData.Purchaser.Employees)
                    {
                        bool found = false;
                        foreach (PersonalAssistant pa in employedPA)
                        {
                            if (pa.Id.Equals(emp.Assistant.Id))
                            {
                                found = true;
                            }
                        }
                        if (found)
                        {
                            purU.Employees.Add(emp);
                        }
                    }
                }

                //add PA connection
                PAtoPurchaserBL PBL = new PAtoPurchaserBL();

                foreach(PersonalAssistant pa in addedPAs)
                {
                    foreach(PersonalAssistant pad in deletedPAs)
                    {
                        if(pa.Id == pad.Id)
                        {
                            addedPAs.Remove(pa);
                            deletedPAs.Remove(pad);
                        }
                    }
                }


                foreach (PersonalAssistant pa in addedPAs)
                {
                    
                    PBL.add(pa, purU);
                }

                //delete PA connection
                foreach(PersonalAssistant pa in deletedPAs)
                {
                    PBL.delete(pa, purU);
                }


                //delete PA connection

                //AKTIV RADIO BOXEN NAMEN

                //switch to Übersicht

      

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Main))
                    {
                        (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGÜbersicht.xaml", UriKind.Relative);
              

                    }
                }

            }
            purU.InfoField = purData.Purchaser.InfoField;
            purDataU.Purchaser = purU;
            if (purU.FirstName != null && purU.LastName != null)
            {
                bl.UpdatePurchaser(purDataU);
            }



        }

        private void deletePA_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
             switch (MessageBox.Show("Wollen Sie diesen Eintrag wirklich löschen?",
                         "Achtung!", MessageBoxButton.YesNo, MessageBoxImage.Question))
             {
                 case MessageBoxResult.No:

                     break;
                 case MessageBoxResult.Yes:

                     Track t = (Track)employedPersonalAssistants.SelectedItem;

                    if (t != null)
                    {
                        int indexDelete = 0;
                        for (int i = 0; i < employedPA.Count; ++i)
                        {
                            if (employedPA[i].Id == t.Id)
                            {
                                indexDelete = i;
                            }

                        }
                        guiListEmployed.Remove(t);
                        employedPersonalAssistants.ItemsSource = guiListEmployed;
                        deletedPAs.Add(employedPA[indexDelete]);
                    }
                     break;

             }
        }

        private void CareAllowence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CareAllowence.SelectedIndex != purData.CareAllowance)
            {
                SystemSounds.Asterisk.Play();
                switch (MessageBox.Show("Wollen Sie wirklich die Pflegestufe ändern?",
                    "Achtung!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.No:
                        CareAllowence.SelectedIndex = (int)purData.CareAllowance;
                        break;
                    case MessageBoxResult.Yes:

                        break;
                }
            }           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
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
            infoW = new InfoWindow(purData.Purchaser.InfoField);
            infoW.ShowDialog();
            if (infoW.changed)
                purData.Purchaser.InfoField = infoW.tbInfo.Text;
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            contactW = new ContactWindow(purData.Purchaser.ContactPerson);
            contactW.ShowDialog();
            purData.Purchaser.ContactPerson = contactW.person;
        }

    
    }
}




