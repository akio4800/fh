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
using SelvesSoftware.GUI.Personenverwaltung.PA.Windows;
using SelvesSoftware.BusinessLogic;
using System.Media;
using SelvesSoftware.DataContainer;
using static SelvesSoftware.GUI.GuiParser;
using System.Collections.ObjectModel;

namespace SelvesSoftware.GUI.Personenverwaltung.PA
{
    /// <summary>
    /// Interaktionslogik für PAHinzufügen.xaml
    /// </summary>
    public partial class PaHinzufügen : Page
    {

        private InfoWindow infoW;
        IPersonalAssistentBl _bl;
        public IPersonalAssistentBl bl = new PersonalAssistentBl();

        public PersonalAssistant pa {get; set; }

        public ObservableCollection<Track> guiListEmployed = new ObservableCollection<Track>();
        public List<Purchaser> employedPurchasers = new List<Purchaser>();
        public List<Purchaser> addedAGs = new List<Purchaser>();
        public List<Purchaser> deletedAGs = new List<Purchaser>();

        public List<Employment> employmentList = new List<Employment>();
        public List<Employment> addedEmps = new List<Employment>();
        public List<Employment> deletedEmps = new List<Employment>();


        private void DienstHinzugfügen_Click(object sender, RoutedEventArgs e)
        {
            DienstHinzufügen d = new DienstHinzufügen(this);
            d.Show();
        }

        private void DienstEntfernen_Click(object sender, RoutedEventArgs e)
        {
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
            SystemSounds.Asterisk.Play();
        }
        private void AGHinzufügen_Click(object sender, RoutedEventArgs e)
        {
            AGAuswählen agaus = new AGAuswählen(this);
            agaus.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputEvaluator ie = new InputEvaluator();
            _bl = new PersonalAssistentBl();
            bool save = true;

            if (doubles())
            {
                SystemSounds.Asterisk.Play();
                switch (MessageBox.Show("Es existiert bereits ein Persönlicher Assistent mit diesem Namen. Soll er wirklich hinzugefügt werden?",
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
                    //PersonalAssistant pa = new PersonalAssistant();
                    pa.HomeAdress = new Adress();
                    pa.HomeAdress.Street = Street.getContent();
                    pa.HomeAdress.HouseNumber = parseInt(HNr.getContent());
                    pa.HomeAdress.Etage = parseInt(Etage.getContent());
                    pa.HomeAdress.StairNumber = parseInt(StairNr.getContent());
                    pa.HomeAdress.DoorNumber = parseInt(DoorNr.getContent());
                    pa.HomeAdress.City = City.getContent();
                    pa.HomeAdress.ZipCode = parseInt(ZIP.getContent());
                    pa.HomeAdress.Country = Country.getContent();
                    pa.nationality = nationality.getContent();

                    pa.FirstName = FirstName.getContent();
                    pa.LastName = LastName.getContent();
                    pa.EMail = Email.getContent();
                    pa.PhoneNumber = PhoneNr.getContent();
                    pa.MobilePhone = MobileNr.getContent();
                    pa.SVN = parseLong(SVN.getContent());
                    // TODO add document date

                    pa.IBAN = IBAN.getContent();
                    pa.BIC = BIC.getContent();
                    pa.AccountHolder = kontoinhaber.getContent();

                    pa.SV = ((bool)SV.IsChecked) ? (true) : false;
                    pa.Dienstvertrag = ((bool)Dienstvertrag.IsChecked) ? (true) : false;
                    pa.BestBH = ((bool)BestätigungBH.IsChecked) ? (true) : false;
                    pa.Grundkurs = ((bool)Grundkurs.IsChecked) ? (true) : false;

                    pa.Purchasers = new List<Purchaser>();
                    pa.EmploymentTimes = this.employmentList;


                    pa.consumedHours = Convert.ToDecimal(tbHours.getContent());

                    //Deadline für die Weiterbildungen ermitteln
                    if (pa.EmploymentTimes != null && pa.EmploymentTimes.Count !=0 && pa.EmploymentTimes.Last().EmplBegin.Year == 1) { 
                        pa.deadLineHours = pa.EmploymentTimes.Last().EmplBegin.AddYears(2);
                    }
                    else
                    {
                        pa.deadLineHours = DateTime.Now.AddYears(2);
                    }

                    if ((bool)activeRadioButton.IsChecked)
                        {
                            pa.Active = true;
                        }
                        foreach (Purchaser pur in employedPurchasers)

                        {
                            //pa.Purchasers.Add(pur);
                        }
                        _bl.CreatePa(pa);


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

                    //insert PA Connection
                    foreach (Purchaser p in addedAGs)
                    {
                        PBL.add(pa, p);
                    }

                    //delete PA connection
                    foreach (Purchaser p in deletedAGs)
                    {
                        PBL.delete(pa, p);
                    }


                    //add Employments
                    foreach (Employment emp in addedEmps)
                    {
                        emp.EmplId = pa.Id;
                        _bl.insertEmployment(emp);
                    }
                    //delete Employments
                    foreach (Employment emp in deletedEmps)
                    {
                        emp.EmplId = pa.Id;
                        _bl.deleteEmployment(emp);
                    }



                    //switch to Übersicht
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(Main))
                        {
                            (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAÜbersicht.xaml", UriKind.Relative);


                        }
                    }
                }
            }
        }



        private bool doubles()
        {
            List<PersonalAssistant> assistents = bl.SelectAllPa();
            foreach (PersonalAssistant pa in assistents)
            {
                if (pa.FirstName.Equals(FirstName.getContent()) && pa.LastName.Equals(LastName.getContent()))
                {
                    return true;
                }
            }
            return false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearFields();
            //DIRTY
            InputEvaluator ie = new InputEvaluator();
            ie.Evaluate(this);
            FirstName.Background = Brushes.White;
            LastName.Background = Brushes.White;
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
            IBAN.Text = "";
            BIC.Text = "";
            kontoinhaber.Text = "";
            AGListe.Items.Clear();
            documentDate.Clear();
            activeRadioButton.IsChecked = true;
            inactiveRadioButton.IsChecked = false;
            employedPurchasers.Clear();
            employmentList.Clear();
            //Workaround to update the Listbox
            employments.DisplayMemberPath = "";
            employments.DisplayMemberPath = "GuiDate";
        }

        private void AGEntfernen_Click(object sender, RoutedEventArgs e)
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

        private void AGListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteAG.IsEnabled = true;
            
        }

        private void employments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DienstEntfernen.IsEnabled = true;
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
            if (pa == null)
            {
                pa = new PersonalAssistant();
            }

            infoW = new InfoWindow(pa.InfoField);
            infoW.ShowDialog();
            if(infoW.changed)
            pa.InfoField = infoW.tbInfo.Text;
        }
    }
}
