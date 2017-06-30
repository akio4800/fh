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

namespace Trove_Stats
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            overview.Background = new SolidColorBrush(Colors.Lavender);
        }

        private void View_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("main/overview.xaml", UriKind.Relative);
            overview.Background = new SolidColorBrush(Colors.Lavender);
            classes.Background = new SolidColorBrush(Colors.AliceBlue);
            gem.Background = new SolidColorBrush(Colors.AliceBlue);
            item.Background = new SolidColorBrush(Colors.AliceBlue);

        }

        private void Class_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("main/overview.xaml", UriKind.Relative);
            
            overview.Background = new SolidColorBrush(Colors.AliceBlue);
            classes.Background = new SolidColorBrush(Colors.Lavender);
            gem.Background = new SolidColorBrush(Colors.AliceBlue);
            item.Background = new SolidColorBrush(Colors.AliceBlue);

        }

        private void Gem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("gem/gem.xaml", UriKind.Relative);
            overview.Background = new SolidColorBrush(Colors.AliceBlue);
            classes.Background = new SolidColorBrush(Colors.AliceBlue);
            gem.Background = new SolidColorBrush(Colors.Lavender);
            item.Background = new SolidColorBrush(Colors.AliceBlue);

        }

        private void Item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Source = new Uri("main/overview.xaml", UriKind.Relative);
             overview.Background = new SolidColorBrush(Colors.AliceBlue);
            classes.Background = new SolidColorBrush(Colors.AliceBlue);
            gem.Background = new SolidColorBrush(Colors.AliceBlue);
            item.Background = new SolidColorBrush(Colors.Lavender);
        }


     
    }
}
