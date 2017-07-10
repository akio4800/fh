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
using System.Collections.ObjectModel;
using SelvesSoftware.BusinessLogic;
using SelvesSoftware.DataContainer;
using System.Windows.Markup;
using System.Media;
using SelvesSoftware.GUI.Personenverwaltung.AG.Windows;

namespace SelvesSoftware.GUI
{
    /// <summary>
    /// Interaktionslogik für AGÜbersicht.xaml
    /// </summary>
    /// 

    public partial class AgÜbersicht : Page
    {
        private ObservableCollection<Track> Data = new ObservableCollection<Track>();
        private List<PurchaserData> InputList;
        private IPurchaserDataBl _bl;
        public int idx { get; set; }
        String selection = "Aktiv";

        public GlobalsWindows globals;


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

        public int MainIndex { get; set; }

        private class Track
        {
            private long _id;
            private String _fn;
            private String _lm;
            private String _adr;
            private String _activ;
            private String _tel;
            private String _mobile;
            private String _mail;
            private String _error;
            private String _info;
            private Brush _myBackground;
            private DateTime? _endDate;
            private AgInformation _ai;

            public long Id
            {
                get { return _id; }
                set { _id = value; }
            }

            public AgInformation Ai
            {
                get { return _ai; }
                set { _ai = value; }
            }
            public Brush MyBackground
            {
                get { return _myBackground; }
                set { _myBackground = value; }
            }

            public String MobilePhone
            {
                get { return _mobile; }
                set { _mobile = value; }
            }

            public String Mail
            {
                get { return _mail; }
                set { _mail = value; }
            }

            public String Error
            {
                get { return _error; }
                set { _error = value; }
            }

            public String Info
            {
                get { return _info; }
                set { _info = value; }
            }

            public DateTime? EndDate
            {
                get { return _endDate; }
                set { _endDate = value; }
            }


            public String FirstName
            {
                get { return _fn; }
                set { _fn = value; }
            }

            public String LastName
            {
                get { return _lm; }
                set { _lm = value; }
            }

            public String Address
            {
                get { return _adr; }
                set { _adr = value; }
            }
            public String PhoneNumber
            {
                get { return _tel; }
                set { _tel = value; }
            }

            public String Active
            {
                get { return _activ; }
                set { _activ = value; }
            }        
        }
        public AgÜbersicht()
        {
            //LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("de-DE")));
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "AUFTRAGGEBER ÜBERSICHT";
                }
            }

            InitializeComponent();
            filter.Items.Clear();
            filter.Items.Add("Aktiv");
            filter.Items.Add("Inaktiv");
            filter.Items.Add("Alle");
            InputList = bl.SelectAllPurchaser();
            loadData();
            filter.SelectedIndex = 0;
        }

        private void GridÜbersicht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = App.Current as App;
            MainIndex = GridÜbersicht.SelectedIndex;
            Track row = (Track)GridÜbersicht.SelectedItem;
            if (row != null)
            {
                a.AGIndex = row.Id;
            }
            else
            {
                a.AGIndex = 0;
            }
        }
        private void Row_MouseEnter(object sender, MouseEventArgs e)
        {
            var row = e.Source as DataGridRow;
            Track tr = (Track)row.DataContext;
            string help = "";
            if (tr.MyBackground == Brushes.LightPink && tr.Ai.Errors.Count() != 0)
            {
                for (int i = 0; i < tr.Ai.Errors.Count(); ++i)
                {
                    help += tr.Ai.Errors[i] + "\n";
                }

                if (tr.Ai.Infos != "")
                    help += tr.Ai.Infos;
                row.ToolTip = help;
            }else if(tr.MyBackground == Brushes.Yellow && tr.Ai.Infos != null)
            {
                row.ToolTip = tr.Ai.Infos;
            }
        }

        private void GridÜbersicht_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {   
            Track row = (Track)GridÜbersicht.SelectedItem;
            var a = App.Current as App;
            
            if (row != null)
            a.AGIndex = row.Id;
            {
                if (row != null && row.MyBackground != null && row.MyBackground == Brushes.LightPink)
                {
                    SystemSounds.Asterisk.Play();
                }
                {
                    if (GridÜbersicht.SelectedIndex != -1)
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(Main))
                            {
                                (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGEinzelAnsicht.xaml", UriKind.Relative);
                            }
                        }
                    }
                }
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadData();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = filter.SelectedItem.ToString();
            loadData();
        }

        private void loadData()
        {
            Data.Clear();
            String active = "";
            for (int i = 0; i < InputList.Count; ++i)
            {
                if (InputList[i].Purchaser.Active)
                {
                    active = "Ja";
                }
                else
                {
                    active = "Nein";
                }
                String nameF = InputList[i].Purchaser.FirstName.ToLower() + " " + InputList[i].Purchaser.LastName.ToLower();
                String nameR = InputList[i].Purchaser.LastName.ToLower() + " " + InputList[i].Purchaser.FirstName.ToLower();
                Track t = new Track();
                t.Id = InputList[i].Purchaser.Id;
                t.FirstName = InputList[i].Purchaser.FirstName;
                t.LastName = InputList[i].Purchaser.LastName;
                t.Address = InputList[i].Purchaser.HomeAdress.ConvertToString();
                t.PhoneNumber = InputList[i].Purchaser.PhoneNumber;
                t.MobilePhone = InputList[i].Purchaser.MobilePhone;
                t.Mail = InputList[i].Purchaser.EMail;
                t.EndDate = InputList[i].Purchaser.ApprovalEnd;
                t.Active = active;
                t.Ai = new AgInformation(InputList[i]);

                t.MyBackground = Brushes.White;

                if (t.Ai.Infos != null)
                {
                    t.MyBackground = Brushes.Yellow;
                    t.Error = "*";
                }          
                if (t.Ai.Errors.Count != 0)
                {
                    t.MyBackground = Brushes.LightPink;
                    t.Error = "!";
                }
                
                    

                
                if (nameF.Contains(search.Text.ToLower()) ||nameR.Contains(search.Text.ToLower()))
                {
                    if (selection == "Aktiv" && t.Active == "Ja")
                    {
                        Data.Add(t);
                    }
                    else if (selection == "Inaktiv" && t.Active == "Nein")
                    {
                        Data.Add(t);
                    }
                    else if (selection == "Alle")
                    {
                        Data.Add(t);
                    }
                }
            }
            GridÜbersicht.DataContext = Data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            globals = new GlobalsWindows(InputList[0].HourlyRate, InputList[0].HourlyRatePayoff, InputList[0].TravellingAllowanceKM);
            globals.ShowDialog();

            if(globals.changed) 
             bl.changeGlobals(globals.Stundensatz,globals.StundensatzAusz,globals.FahrtkostenzusatzKM);
           
        }
    }
}
