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
using SelvesSoftware.GUI.Monatsabrechnung.Windows;

namespace SelvesSoftware.GUI.Monatsabrechnung
{
    /// <summary>
    /// Interaktionslogik für MASideNavigation.xaml
    /// </summary>
    public partial class MASideNavigation : Page
    {
        public MASideNavigation()
        {
            InitializeComponent();
            //MA_Übersicht.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
        }



         private void MA_Hinzufügen_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            //MA_Hinzufügen.Background = br;
        }

        private void MA_Bearbeiten_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            //MA_Bearbeiten.Background = br;
        }

        private void MA_Übersicht_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(215, 223, 230));
            //MA_Übersicht.Background = br;
        }

        private void MA_Hinzufügen_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Monatsabrechnung/MAHinzufügen.xaml";
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
                //MA_Hinzufügen.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                //MA_Hinzufügen.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }

        private void MA_Bearbeiten_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Monatsabrechnung/MABearbeiten.xaml";
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
                //MA_Bearbeiten.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                //MA_Bearbeiten.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }

        }

        private void MA_Übersicht_MouseLeave(object sender, MouseEventArgs e)
        {
            String str = "SelvesSoftware;component/gui/Monatsabrechnung/MAÜbersicht.xaml";
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
                //MA_Übersicht.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));

            }
            else
            {
                //MA_Übersicht.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }


        }

        private void MA_Übersicht_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /**
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Monatsabrechnung/MAÜbersicht.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            //MA_Übersicht.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            //MA_Bearbeiten.Background = br2;
            //MA_Hinzufügen.Background = br2;


    **/

            LAAuswählen la = new LAAuswählen();
            la.ShowDialog();

        }

        private void MA_Hinzufügen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Monatsabrechnung/MAHinzufügen.xaml", UriKind.Relative);

                }
            }
            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            //MA_Hinzufügen.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            //MA_Übersicht.Background = br2;
            //MA_Bearbeiten.Background = br2;
        }

        private void MA_Bearbeiten_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Main))
                {
                    (window as Main).FrameÜbersicht.Source = new Uri("../Monatsabrechnung/MABearbeiten.xaml", UriKind.Relative);

                }
            }

            Brush br = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            //MA_Bearbeiten.Background = br;
            Brush br2 = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            //MA_Übersicht.Background = br2;
            //MA_Hinzufügen.Background = br2;
        }

       

      
    
    }
}
