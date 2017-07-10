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

namespace SelvesSoftware.GUI
{
    /// <summary>
    /// Interaktionslogik für AGÜbersichtSideNavigation.xaml
    /// </summary>
    public partial class AgÜbersichtSideNavigation : Page
    {
        public int idxtra { get; set; }

        public AgÜbersichtSideNavigation()
        {
            InitializeComponent();
            AgÜbersichtLabel.Background =  new SolidColorBrush(Color.FromRgb(211, 211, 211));
        }

        private void AG_Hinzufügen_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            AgHinzufügenLabel.Background = br;
        }


        private void AG_Übersicht_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            AgÜbersichtLabel.Background = br;
        }

        private void AG_Hinzufügen_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGHinzufügen.xaml";
            String uri = "";

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    uri = (window as Main).FrameÜbersicht.Source.ToString();


                }
            }
           


            if (str == uri)
            {
                AgHinzufügenLabel.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                AgHinzufügenLabel.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }
        /*
        private void AG_Bearbeiten_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGBearbeiten.xaml";
            String uri = "";

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    uri = (window as Main).FrameÜbersicht.Source.ToString();


                }
            }


            if (str == uri)
            {
                AgBearbeiten.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                AgBearbeiten.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        } */

        private void AG_Übersicht_MouseLeave(object sender, MouseEventArgs e)
        {
            String str1 = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGÜbersicht.xaml";
            String str2 = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGEinzelAnsicht.xaml";
            String str3 = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGBearbeiten.xaml";

            String uri = "";
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                   uri =  (window as Main).FrameÜbersicht.Source.ToString();

                
                }
            }
       

       
            if (str1 == uri||str2 == uri || str3 == uri)
            {
                 AgÜbersichtLabel.Background= new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                 AgÜbersichtLabel.Background= new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                 
            }

           
        }

        private void AG_Übersicht_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var a = App.Current as App;
            a.AGIndex = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGÜbersicht.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            AgÜbersichtLabel.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgHinzufügenLabel.Background = br2;
        }

        private void AG_Hinzufügen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var a = App.Current as App;
            a.AGIndex = 0;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGHinzufügen.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            AgHinzufügenLabel.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgÜbersichtLabel.Background = br2;
        }

        private void AG_Bearbeiten_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGBearbeiten.xaml", UriKind.Relative);

                }
            }

           




            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgÜbersichtLabel.Background = br2;
            AgHinzufügenLabel.Background = br2;

        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }
            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }
}
