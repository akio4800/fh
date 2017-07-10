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
        public AgÜbersichtSideNavigation()
        {
            InitializeComponent();
            AgÜbersicht.Background =  new SolidColorBrush(Color.FromRgb(211, 211, 211));
        }

        private void AG_Hinzufügen_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            AgHinzufügen.Background = br;
        }

        private void AG_Bearbeiten_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            AgBearbeiten.Background = br;
        }

        private void AG_Übersicht_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            AgÜbersicht.Background = br;
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
                AgHinzufügen.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                AgHinzufügen.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }

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

        }

        private void AG_Übersicht_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/AG/AGÜbersicht.xaml";
             String uri = "";
            
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                   uri =  (window as Main).FrameÜbersicht.Source.ToString();

                
                }
            }
       

       
            if (str == uri)
            {
                 AgÜbersicht.Background= new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                 AgÜbersicht.Background= new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                 
            }

           
        }

        private void AG_Übersicht_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGÜbersicht.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            AgÜbersicht.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgBearbeiten.Background = br2;
            AgHinzufügen.Background = br2;
        }

        private void AG_Hinzufügen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/AG/AGHinzufügen.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            AgHinzufügen.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgÜbersicht.Background = br2;
            AgBearbeiten.Background = br2;
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
            AgBearbeiten.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            AgÜbersicht.Background = br2;
            AgHinzufügen.Background = br2;
        }
    }
}
