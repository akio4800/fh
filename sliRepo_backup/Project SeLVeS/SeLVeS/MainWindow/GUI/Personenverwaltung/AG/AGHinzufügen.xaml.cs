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
using SelvesSoftware.GUI.Elemente;
//using SelvesSoftware.GUI.Personenverwaltung.AG;
using SelvesSoftware.BusinessLogic;
using SelvesSoftware.DataContainer;
using SelvesSoftware.GUI.Personenverwaltung.AG.Windows;
using System.Media;
using SelvesSoftware.GUI.Personenverwaltung.PA;
using System.Collections.ObjectModel;
using SelvesSoftware.GUI.Personenverwaltung;
using static SelvesSoftware.GUI.GuiParser;
using SelvesSoftware.DB;

namespace SelvesSoftware.GUI
{
    /// <summary>
    /// Interaktionslogik für AGHinzufügen.xaml
    /// </summary>
    public partial class AgHinzufügen : Page
    {
        public List<PersonalAssistant> employedPA = new List<PersonalAssistant>();
        public List<PersonalAssistant> addedPAs = new List<PersonalAssistant>();
        public List<PersonalAssistant> deletedPAs = new List<PersonalAssistant>();
        public ObservableCollection<Track> guiListEmployed = new ObservableCollection<Track>();
        public PurchaserData purData;
        private IPurchaserDataBl _bl;

        private ContactWindow contactW;
        private InfoWindow infoW;
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

        public AgHinzufügen()
        {


            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "AUFTRAGGEBER HINZUFÜGEN";
                }
            }

            InitializeComponent();
            
            deletePA.IsEnabled = false;
            

            FillGlobals();
            

            registerInputBoxes();
            _bl = new PurchaserDataBl();
            CareAllowence.Items.Add("0");
            CareAllowence.Items.Add("1");
            CareAllowence.Items.Add("2");
            CareAllowence.Items.Add("3");
            CareAllowence.Items.Add("4");
            CareAllowence.Items.Add("5");
            CareAllowence.Items.Add("6");
            CareAllowence.Items.Add("7");
            //CareAllowence.SelectedValue();
        }

        private void FillGlobals()
        {
            PurchaserDataDAO pdao = new PurchaserDataDAO();
            List<PurchaserData> purchasers = pdao.SelectAll();
            PayperHour.Text = purchasers[0].HourlyRate.ToString();
            billablePayperHour.Text = purchasers[0].HourlyRatePayoff.ToString();
            KmPay.Text = purchasers[0].TravellingAllowanceKM.ToString();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deletePA.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (purData == null)
                purData = new PurchaserData();


            if (purData.Purchaser == null)
                purData.Purchaser = new Purchaser();


            Purchaser pur = purData.Purchaser;
            //purData = new PurchaserData();
            pur.HomeAdress = new Adress();
            InputEvaluator ie = new InputEvaluator();
            bool save = true;

            if (doubles())
            {
                SystemSounds.Asterisk.Play();
                switch (MessageBox.Show("Es existiert bereits ein Auftraggeber mit diesem Namen. Soll er wirklich hinzugefügt werden?",
                    "Achtung!",
                    MessageBoxButton.YesNo, MessageBoxImage.Question))
                {

                    case MessageBoxResult.No:
                        save = false;
                        break;
                    case MessageBoxResult.Yes:
                        break;
                }
            }


            if (save)
            {
                if (ie.Evaluate(this))
                {

                    pur.FirstName = FirstName.getContent();
                    pur.LastName = LastName.getmustContent();
                    pur.EMail = Email.getContent();
                    if (PhoneNr.getContent() != null)
                    {
                        pur.PhoneNumber = PhoneNr.getContent();
                    }
                    if (MobileNr.getContent() != null)
                    {
                        pur.MobilePhone = MobileNr.getContent();
                    }
                    pur.HomeAdress.Street = Street.getContent();
                    pur.HomeAdress.StairNumber = parseInt(StairNr.getContent());
                    pur.HomeAdress.HouseNumber = parseInt(HNr.getContent());
                    pur.HomeAdress.Etage = parseInt(Etage.getContent());
                    pur.HomeAdress.DoorNumber = parseInt(DoorNr.getContent());
                    pur.HomeAdress.City = City.getContent();
                    pur.HomeAdress.Country = Country.getContent();
                    pur.HomeAdress.ZipCode = parseInt(ZIP.getContent());
                    pur.BIC = BIC.getContent();
                    pur.IBAN = IBAN.getContent();
                    pur.SVN = parseLong(SVN.getContent());
                    pur.AccountHolder = kontoinhaber.getContent();
                    pur.EntryDate = EntryDate.SelectedDate;
                    pur.ApprovalBegin = ApprovelFrom.SelectedDate;
                    pur.ApprovalEnd = ApprovelTo.SelectedDate;
                    pur.nationality = nationality.getContent();
                    purData.HourlyRate = parseDecimal(PayperHour.getContent());
                    purData.TravellingAllowanceKM = parseDecimal(KmPay.getContent());
                    purData.HourlyRatePayoff = parseDecimal(billablePayperHour.getContent());
                    purData.Income = parseDecimal(Income.getContent());
                    purData.AssistenceDemand = parseInt(Needof.getContent());
                    purData.InputIncome = parseDecimal(IncomeBetrag.getContent());
                    purData.CareAllowance = parseInt((string)CareAllowence.SelectedValue);
                    pur.DistrictCommision = districtcommission.getContent();
                    if ((bool)activeRadioBtn.IsChecked)
                    {
                        pur.Active = true;
                    }

                    if ((bool)einfuehrungskurs.IsChecked)
                    {
                        pur.hasContract = true;
                    }
                    if ((bool)contract.IsChecked)
                    {
                        pur.hasIntroCourse = true;
                    }

                    //switch to Übersicht
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Main))
                        {
                            (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGÜbersicht.xaml", UriKind.Relative);
                        }
                    }
                    purData.Purchaser = pur;
                    if (pur.FirstName != null && pur.LastName != null)
                    {
                        _bl.CreatePurchaser(purData);
                    }

                    //add PA connection
                    PAtoPurchaserBL PBL = new PAtoPurchaserBL();

                    foreach (PersonalAssistant pa in addedPAs)
                    {
                        foreach (PersonalAssistant pad in deletedPAs)
                        {
                            if (pa.Id == pad.Id)
                            {
                                addedPAs.Remove(pa);
                                deletedPAs.Remove(pad);
                            }
                        }
                    }


                    foreach (PersonalAssistant pa in addedPAs)
                    {

                        PBL.add(pa, pur);
                    }

                    //delete PA connection
                    foreach (PersonalAssistant pa in deletedPAs)
                    {
                        PBL.delete(pa, pur);
                    }

                }
            }
        }

        private bool doubles()
        {
            List<PurchaserData> purchaser = bl.SelectAllPurchaser();
            foreach (PurchaserData pur in purchaser)
            {
                if (pur.Purchaser.SVN != 0 && pur.Purchaser.SVN.Equals(SVN.getContent()) || pur.Purchaser.FirstName.Equals(FirstName.getContent()) && pur.Purchaser.LastName.Equals(LastName.getContent()))
                {
                    return true;
                }
            }
            return false;
        }

        private void registerInputBoxes()
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(this, i);

                InputBox ibx = new InputBox();

                if (Object.ReferenceEquals(childVisual.GetType(), ibx.GetType()))
                {
                    //childVisual.
                    InputBox ib = (InputBox)childVisual;
                    ib.GotFocus += new System.Windows.RoutedEventHandler(this.InputBoxFocus);
                    // Enumerate children of the child visual object.
                }
            }
        }

        private void InputBoxFocus(object sender, RoutedEventArgs e)
        {
            InputBox ib = (InputBox)sender;
            ib.Background = Brushes.White;
        }

        private void clearFields()
        {
            FirstName.Text = "";
            LastName.Text = "";
            Email.Text = "";
            PhoneNr.Text = "";
            MobileNr.Text = "";
            SVN.Text = "";
            Street.Text = "";
            HNr.Text = "";
            Etage.Text = "";
            StairNr.Text = "";
            DoorNr.Text = "";
            City.Text = "";
            Country.Text = "";
            ZIP.Text = "";
            nationality.Text = "";
            EntryDate.Text = "";
            ApprovelFrom.Text = "";
            ApprovelTo.Text = "";
            Needof.Text = "";
            Income.Text = "";
            IncomeBetrag.Text = "";
            CareAllowence.Text = "";

            IBAN.Text = "";
            BIC.Text = "";
            kontoinhaber.Text = "";


            //DIRTY
            InputEvaluator ie = new InputEvaluator();
            ie.Evaluate(this);
            FirstName.Background = Brushes.White;
            LastName.Background = Brushes.White;
           
            activeRadioBtn.IsChecked = true;
            inactiveRadioBtn.IsChecked = false;
          
            contract.IsChecked = false;
            einfuehrungskurs.IsChecked = false;
            employedPA.Clear();
            guiListEmployed.Clear();
            employedPersonalAssistants.ItemsSource = guiListEmployed;
        }

        private void Zurücksetzen_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void PAHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            PAAuswählen paaus = new PAAuswählen(this);
            paaus.ShowDialog();
            
        }

        private void deletePA_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
            switch (MessageBox.Show("Wollen Sie diesen Eintrag wirklich löschen?",
               "Achtung!",
               MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Yes:
                    String AG = employedPersonalAssistants.SelectedItem as String;
                    string[] names = AG.Split(new Char[] { ' ' });
                    int indexDelete = 0;
                    for (int i = 0; i < employedPA.Count; ++i)
                    {
                        if (employedPA[i].FirstName.Equals(names[0]) && employedPA[i].LastName.Equals(names[1]))
                        {
                            indexDelete = i;
                        }

                    }
                    employedPA.RemoveAt(indexDelete);
                    employedPersonalAssistants.Items.RemoveAt(indexDelete);
                    deletedPAs.Add(employedPA[indexDelete]);
                    break;

            }
        }

        private void employedPersonalAssistants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deletePA.IsEnabled = true;
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
            if (purData == null)
            {
                purData = new PurchaserData();
            }
            infoW = new InfoWindow(purData.Purchaser.InfoField);
            infoW.ShowDialog();
            purData.Purchaser.InfoField = infoW.tbInfo.Text;
            if (infoW.changed)
                purData.Purchaser.InfoField = infoW.tbInfo.Text;
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            if (purData == null)
                purData = new PurchaserData();
            if (purData.Purchaser == null)
            purData.Purchaser = new Purchaser();
            if(purData.Purchaser.ContactPerson==null)
            purData.Purchaser.ContactPerson = new Person();

            contactW = new ContactWindow(purData.Purchaser.ContactPerson);
            contactW.ShowDialog();
            purData.Purchaser.ContactPerson = contactW.person;
        }
    }
}
