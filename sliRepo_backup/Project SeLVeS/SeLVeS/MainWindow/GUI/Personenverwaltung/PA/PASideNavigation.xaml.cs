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
    /// Interaktionslogik für PAÜbersichtSideNavigation.xaml
    /// </summary>
    public partial class PaÜbersichtSideNavigation : Page
    {
        public PaÜbersichtSideNavigation()
        {
            InitializeComponent();
            PaÜbersicht.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
        }


        private void PA_Hinzufügen_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            PaHinzufügen.Background = br;
        }

        /*private void PA_Bearbeiten_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            PaBearbeiten.Background = br;
        }
        */
        private void PA_Übersicht_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            PaÜbersicht.Background = br;
        }

        private void PA_Hinzufügen_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/PA/PAHinzufügen.xaml";
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
                PaHinzufügen.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                PaHinzufügen.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }
        /* TODO remove if not used any more
        private void PA_Bearbeiten_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/PA/PABearbeiten.xaml";
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
                PaBearbeiten.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                PaBearbeiten.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }
        */
        private void PA_Übersicht_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Personenverwaltung/PA/PAÜbersicht.xaml";
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
                PaÜbersicht.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                PaÜbersicht.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }


        }

        private void PA_Übersicht_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAÜbersicht.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            PaÜbersicht.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            PaHinzufügen.Background = br2;
        }

        private void PA_Hinzufügen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PAHinzufügen.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            PaHinzufügen.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            PaÜbersicht.Background = br2;
           
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
        /* TODO delete if not used anymore
private void PA_Bearbeiten_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
{
   foreach (Window window in Application.Current.Windows)
   {
       if (window.GetType() == typeof(Main))
       {
           (window as Main).FrameÜbersicht.Source = new Uri("../Personenverwaltung/PA/PABearbeiten.xaml", UriKind.Relative);

       }
   }

   Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
   PaBearbeiten.Background = br;
   Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
   PaÜbersicht.Background = br2;
   PaHinzufügen.Background = br2;
}

*/


    }
}
