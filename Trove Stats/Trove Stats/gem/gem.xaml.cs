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

namespace Trove_Stats.gem
{
    /// <summary>
    /// Interaktionslogik für gem.xaml
    /// </summary>
    public partial class Gem : Page
    {
        public Gem()
        {
            InitializeComponent();
            emp.Background = new SolidColorBrush(Colors.LightBlue);
        }

        private void Emp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("emp.xaml", UriKind.Relative);
            emp.Background = new SolidColorBrush(Colors.LightBlue);
           small.Background = new SolidColorBrush(Colors.Lavender);
        }

        private void Small_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            frame.Source = new Uri("lesser.xaml", UriKind.Relative);
            emp.Background = new SolidColorBrush(Colors.Lavender);
            small.Background = new SolidColorBrush(Colors.LightBlue);
        }
    }
}
