﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SelvesSoftware.GUI;
using System.Collections;
using System.Media;

namespace SelvesSoftware
{
    /// <summary>
    /// Interaktionslogik für PAÜbersicht.xaml
    /// </summary>
    public partial class PaÜbersicht : Page
    {
        private ObservableCollection<Track> Data = new ObservableCollection<Track>();
        private List<PersonalAssistant> InputList;
        private IPersonalAssistentBl _bl;
        String selection = "Aktiv";

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
            private String _info;
            private PaInformation _pi;
            private Brush _myBackground;
            private object item;


            public long Id
            {
                get { return _id; }
                set { _id = value; }
            }
            public PaInformation Pi
            {
                get { return _pi; }
                set { _pi = value; }
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

            public String Info
            {
                get { return _info; }
                set { _info = value; }
            }

            public String Mail
            {
                get { return _mail; }
                set { _mail = value; }
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

        public PaÜbersicht()
        {
            String active = "";

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).windowHeader.Content = "ÜBERSICHT PERSÖNLICHE ASSISTENTEN ";
                }
            }
            InitializeComponent();
            filter.Items.Clear();
            filter.Items.Add("Aktiv");
            filter.Items.Add("Inaktiv");
            filter.Items.Add("Alle");
            InputList = bl.SelectAllPa();
            loadData();
            filter.SelectedIndex = 0;
            InputList = bl.SelectAllPa(); ;
        }

        private void GridÜbersicht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = App.Current as App;
            MainIndex = GridÜbersicht.SelectedIndex;
            Track row = (Track)GridÜbersicht.SelectedItem;
            if (row != null)
            {
                a.PAIndex = row.Id;
            }
            else
            {
                a.PAIndex = 0;
            }
        }

        private void GridÜbersicht_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Track row = (Track)GridÜbersicht.SelectedItem;

            if (row != null)
            {
                if (row.MyBackground == Brushes.LightPink)
                {
                    SystemSounds.Asterisk.Play();
                }
                if (e.LeftButton == MouseButtonState.Pressed && e.RightButton == MouseButtonState.Released)
                {
                    if (GridÜbersicht.SelectedIndex != -1)
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(Main))
                            {
                                (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAEinzelAnsicht.xaml", UriKind.Relative);
                            }
                        }
                    }
                }
            }
        }

        private void Row_MouseEnter(object sender, MouseEventArgs e)
        {
            var row = e.Source as DataGridRow;
            Track tr = (Track)row.DataContext;
            string help = "";

            if (tr.MyBackground == Brushes.LightPink && tr.Pi.Errors.Count() != 0)
            {
                for (int i = 0; i < tr.Pi.Errors.Count(); ++i)
                {
                    help += tr.Pi.Errors[i] + "\n";
                }
                if (tr.Pi.Infos!="")
                help += tr.Pi.Infos;
                row.ToolTip = help;
            }

            if (tr.MyBackground == Brushes.Yellow && tr.Pi.Infos != null)
            {             
                row.ToolTip = tr.Pi.Infos;
            }
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selection = filter.SelectedItem.ToString();
            loadData();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            String active = "";
            Data.Clear();

            for (int i = 0; i < InputList.Count; ++i)
            {
                if (InputList[i].Active)
                {
                    active = "Ja";
                }
                else
                {
                    active = "Nein";
                }
                Track t = new Track();

                t.Pi = new PaInformation(InputList[i]);

                t.MyBackground = Brushes.White;

                if (t.Pi.Infos != null)
                {
                    t.MyBackground = Brushes.Yellow;
                    t.Info = "*";
                }

                if (t.Pi.Errors.Count != 0)
                {
                    t.MyBackground = Brushes.LightPink;
                    t.Info = "!";

                }

                String nameF = InputList[i].FirstName.ToLower() + " " + InputList[i].LastName.ToLower();
                String nameR = InputList[i].LastName.ToLower() + " " + InputList[i].FirstName.ToLower();
                t.Id = InputList[i].Id;
                t.FirstName = InputList[i].FirstName;
                t.LastName = InputList[i].LastName;
                t.Address = InputList[i].HomeAdress.ConvertToString();
                t.PhoneNumber = InputList[i].PhoneNumber;
                t.MobilePhone = InputList[i].MobilePhone;
                t.Mail = InputList[i].EMail;
                t.Active = active;

                if (nameF.Contains(search.Text.ToLower())|| nameR.Contains(search.Text.ToLower()))
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
                GridÜbersicht.ItemsSource = Data;
            }
        }
    }
}


