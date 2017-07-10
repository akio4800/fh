using SelvesSoftware.BusinessLogic;
using SelvesSoftware.GUI.Personenverwaltung.PA.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
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
using System.Globalization;
using static SelvesSoftware.GUI.GuiParser;

namespace SelvesSoftware.GUI.Personenverwaltung.PA
{
    /// <summary>
    /// Interaktionslogik für PABearbeiten.xaml
    /// </summary>
    public partial class PaBearbeiten : Page
    {
        private InfoWindow infoW;
        IPersonalAssistentBl _bl;
        public IPersonalAssistentBl bl
        {
            get
            {
                if (_bl == null)
                {
                    _bl = new PersonalAssistentBl();
                }
                return _bl;
            }
            set
            {
                _bl = value;
            }
        }
        public PersonalAssistant pa;

        public List<Purchaser> employedPurchasers = new List<Purchaser>();
        public List<Employment> employmentList = new List<Employment>();

        public List<Employment> addedEmps = new List<Employment>();
        public List<Employment> deletedEmps = new List<Employment>();

        public ObservableCollection<Track> guiListEmployed = new ObservableCollection<Track>();
        public List<Purchaser> addedAGs = new List<Purchaser>();
        public List<Purchaser> deletedAGs = new List<Purchaser>();

        private bool onLoad = true;


        public PaBearbeiten()
        {
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "PERSÖNLICHEN ASSISTENTEN BEARBEITEN";


                }
            }
            InitializeComponent();
            getAssistant();
            onLoad = false;
        }

        private void getAssistant()
        {

            var a = App.Current as App;
            pa = bl.SelectPa((int)a.PAIndex);

            FirstName.Text = pa.FirstName;
            LastName.Text = pa.LastName;
            MobileNr.Text = pa.MobilePhone;
            PhoneNr.Text = pa.PhoneNumber;
            Email.Text = pa.EMail;
            SVN.Text = pa.SVN.ToString();
            Street.Text = pa.HomeAdress.Street;
            Country.Text = pa.HomeAdress.Country;
            HNr.Text = pa.HomeAdress.HouseNumber.ToString();
            DoorNr.Text = pa.HomeAdress.DoorNumber.ToString();
            Etage.Text = pa.HomeAdress.Etage.ToString();
            StairNr.Text = pa.HomeAdress.StairNumber.ToString();
            City.Text = pa.HomeAdress.City;
            ZIP.Text = pa.HomeAdress.ZipCode.ToString();
            nationality.Text = pa.nationality;
            SVN.Text = (pa.SVN == 0) ? "": pa.SVN.ToString();

            if (pa.ClosingDateDocuments != null) { documentDate.Text = pa.ClosingDateDocuments.Value.ToShortDateString(); }

            tbHours.Text = pa.consumedHours.ToString();
            if (pa.deadLineHours!=null)
            tbDeadline.Text = pa.deadLineHours.Value.ToShortDateString();

            IBAN.Text = pa.IBAN;
            BIC.Text = pa.BIC;
            kontoinhaber.Text = pa.AccountHolder;


            activeRadioBtn.IsChecked = pa.Active;
            inactiveRadioBtn.IsChecked = !pa.Active;

            SV.IsChecked = pa.SV;
            Dienstvertrag.IsChecked = pa.Dienstvertrag;
            BestätigungBH.IsChecked = pa.BestBH;
            Grundkurs.IsChecked = pa.Grundkurs;

            if (pa.EmploymentTimes != null)
            {
                employmentList = pa.EmploymentTimes;
            }
            else
            {
                employmentList = new List<Employment>();
            }
            employments.ItemsSource = employmentList;
            /*
            if (pa.EmploymentTimes != null)
            {
                foreach (Employment e in pa.EmploymentTimes)
                {
                    String pd;
                    if (e.EmplEnd.Year != 1)
                    {
                        pd = e.EmplBegin.ToShortDateString() + " - " + e.EmplEnd.ToShortDateString();
                    }
                    else
                    {
                        pd = e.EmplBegin.ToShortDateString();
                    }
                    employments.Items.Add(pd);
                }
            }
            else
            {
                employments.Items.Add("Keine Dienste");

            }
            */


            if (pa.Purchasers != null)
            {
                foreach (Purchaser p in pa.Purchasers)
                {
                    String pd = p.LastName + " " + p.FirstName;
                    employedPurchasers.Add(p);
                    guiListEmployed.Add(new Track(p.Id, p.FirstName, p.LastName));
                }
                AGListe.ItemsSource = guiListEmployed;
            }
            
        }

        private void DienstHinzugfügen_Click(object sender, RoutedEventArgs e)
        {
            DienstHinzufügen d = new DienstHinzufügen(this);
            d.ShowDialog();
        }

        private void DienstEntfernen_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
            switch (MessageBox.Show("Wollen Sie den Dienst wirklich löschen?",
                "Achtung!",
                      MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    if (employments.SelectedItem != null)
                    {
                        employmentList.Remove((Employment)employments.SelectedItem);
                        deletedEmps.Add((Employment)employments.SelectedItem);
                        
                        //Workaround to update the Listbox
                        employments.DisplayMemberPath = "";
                        employments.DisplayMemberPath = "GuiDate";
                    }
                    break;

            }
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!onLoad)
            {


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

        private void AGentfernen_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
            switch (MessageBox.Show("Wollen Sie diesen Eintrag wirklich löschen?",
                "Achtung!",
                MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                case MessageBoxResult.No:

                    break;
                case MessageBoxResult.Yes:
                    Track t = (Track)AGListe.SelectedItem;

                    if (t != null)
                    {
                        int indexDelete = 0;
                        for (int i = 0; i < employedPurchasers.Count; ++i)
                        {
                            if (employedPurchasers[i].Id == t.Id)
                            {
                                indexDelete = i;
                            }

                        }
                        guiListEmployed.Remove(t);
                        AGListe.ItemsSource = guiListEmployed;
                        deletedAGs.Add(employedPurchasers[indexDelete]);
                    }
                    break;

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            guiListEmployed.Clear();
            employmentList.Clear();
            employments.Items.Clear();
            
            getAssistant();
        }


        private void Speichern_Click(object sender, RoutedEventArgs e)
        {
            InputEvaluator ie = new InputEvaluator();


            if (ie.Evaluate(this))
            {
                PersonalAssistant paU = new PersonalAssistant();
                var a = App.Current as App;
                paU.Id = a.PAIndex;
                paU.HomeAdress = new Adress();
                paU.HomeAdress.AdressId = pa.HomeAdress.AdressId;
                paU.HomeAdress.Street = Street.getContent();
                paU.HomeAdress.HouseNumber = parseInt(HNr.getContent());
                paU.HomeAdress.Etage = parseInt(Etage.getContent());
                paU.HomeAdress.StairNumber = parseInt(StairNr.getContent());
                paU.HomeAdress.DoorNumber = parseInt(DoorNr.getContent());
                paU.HomeAdress.City = City.getContent();
                paU.HomeAdress.ZipCode = parseInt(ZIP.getContent());
                paU.HomeAdress.Country = Country.getContent();
                paU.nationality = nationality.getContent();

                paU.FirstName = FirstName.getContent();
                paU.LastName = LastName.getContent();
                paU.EMail = Email.getContent();
                paU.PhoneNumber = PhoneNr.getContent();
                paU.MobilePhone = MobileNr.getContent();
                paU.SVN = parseLong(SVN.getContent());
                paU.ClosingDateDocuments = DateTime.ParseExact(documentDate.getContent(), "dd.mm.yyyy", CultureInfo.InvariantCulture);

                paU.IBAN = IBAN.getContent();
                paU.BIC = BIC.getContent();
                paU.AccountHolder = kontoinhaber.getContent();
                //paU.EmploymentTimes.Clear();

                paU.SV = ((bool)SV.IsChecked) ? (true) : false;
                paU.Dienstvertrag = ((bool)Dienstvertrag.IsChecked) ? (true) : false;
                paU.BestBH = ((bool)BestätigungBH.IsChecked) ? (true) : false;
                paU.Grundkurs = ((bool)Grundkurs.IsChecked) ? (true) : false;
                paU.Active = ((bool)activeRadioBtn.IsChecked) ? (true) : false;

                //Weiterbildungen
                paU.consumedHours = Convert.ToDecimal(tbHours.getContent());          
                paU.deadLineHours = Convert.ToDateTime(tbDeadline.getContent());
                paU.InfoField = pa.InfoField;
                bl.UpdatePa(paU);

               //check if add and delete are in conflict
                PAtoPurchaserBL PBL = new PAtoPurchaserBL();

                foreach (Purchaser p in addedAGs)
                {
                    foreach (Purchaser pd in deletedAGs)
                    {
                        if (p.Id == pd.Id)
                        {
                            addedAGs.Remove(p);
                            deletedAGs.Remove(pd);
                        }
                    }
                }

                //add PA connection
                foreach (Purchaser p in addedAGs)
                {
                    PBL.add(paU, p);
                }

                //delete PA connection
                foreach (Purchaser p in deletedAGs)
                {
                    PBL.delete(paU, p);
                }

                //add Employments
                foreach (Employment emp in addedEmps)
                {
                    _bl.insertEmployment(emp);
                }
                //delete Employments
                foreach (Employment emp in deletedEmps)
                {
                    _bl.deleteEmployment(emp);
                }


                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(Main))
                    {
                        (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAÜbersicht.xaml", UriKind.Relative);

                    }
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAÜbersicht.xaml", UriKind.Relative);


                }
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            infoW = new InfoWindow(pa.InfoField);
            infoW.ShowDialog();

            if(infoW.changed)
            pa.InfoField = infoW.tbInfo.Text;
        }

        private void AGHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            AGAuswählen agaus = new AGAuswählen(this);
            agaus.ShowDialog();
        }
    }
}
