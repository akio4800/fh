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
using System.Windows.Shapes;

namespace SelvesSoftware
{
    /// <summary>
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
           
            this.WindowState = WindowState.Maximized;
            
            InitializeComponent();
            this.windowHeader.Content = "START ÜBERSICHT";
            DB.DBConnector.getConfig();
        }

        private void SettingsLogin(object sender, RoutedEventArgs e)
        {

   
        }

        private void HomeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.windowHeader.Content = "START ÜBERSICHT";
            FrameÜbersicht.Source = new Uri("MainNavigation.xaml" ,UriKind.Relative);
            FrameNavigation.Source = new Uri("MainSideNavigation.xaml", UriKind.Relative);
        }

        private void HomeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb(180, 180, 180));
            HomeButton.Background = br;
        }

        private void HomeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Brush br = new SolidColorBrush(Color.FromRgb( 211, 211, 211));
            HomeButton.Background = br;
        }


       
    }
}
