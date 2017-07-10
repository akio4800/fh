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
    /// Interaktionslogik für MainSideNavigation.xaml
    /// </summary>
    public partial class MainSideNavigation : Page
    {
        public MainSideNavigation()
        {
            InitializeComponent();
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
