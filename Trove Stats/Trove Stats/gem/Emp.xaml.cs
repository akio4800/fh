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
using Trove_Stats.model;
using System.Collections.ObjectModel;

namespace Trove_Stats.gem
{
    /// <summary>
    /// Interaktionslogik für emp.xaml
    /// </summary>
    public partial class Emp : Page
    {
        private ObservableCollection<EmpGem> Data = new ObservableCollection<EmpGem>();
        public Emp()
        {

            Data.Clear();


            InitializeComponent();

            first.Items.Add("PD");
            first.Items.Add("MD");
            first.Items.Add("CH");
            first.Items.Add("CD");
            first.Items.Add("MH");
            first.Items.Add("MH%");
            first.Items.Add("HR");

            second.Items.Add("PD");
            second.Items.Add("MD");
            second.Items.Add("CH");
            second.Items.Add("CD");
            second.Items.Add("MH");
            second.Items.Add("MH%");
            second.Items.Add("HR");


            third.Items.Add("PD");
            third.Items.Add("MD");
            third.Items.Add("CH");
            third.Items.Add("CD");
            third.Items.Add("MH");
            third.Items.Add("MH%");
            third.Items.Add("HR");

            type.Items.Add("Water");
            type.Items.Add("Air");
            type.Items.Add("Fire");

            ability.Items.Add("Pyrodisk");
            ability.Items.Add("Cubic Curtain");


            first.SelectedIndex = 0;
            second.SelectedIndex = 0;
            third.SelectedIndex = 0;
            ability.SelectedIndex = 0;
            type.SelectedIndex = 0;






        }




        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EmpGem gem = new EmpGem();
            if (type.SelectedItem != null)
            {
                gem.GemType = type.SelectedItem.ToString();
            }

            if (ability.SelectedItem != null)
            {
                gem.GemAbility = ability.SelectedItem.ToString();
            }
            if (lvl.Text != "")
            {
                gem.Lvl = int.Parse(lvl.Text);
            }

            if (pr.Text != "")
            {
                gem.PR = int.Parse(pr.Text);
            }
            if (first.SelectedItem != null && firstval.Text != string.Empty)
            {
                gem.First.StatType = first.SelectedItem.ToString();
                gem.First.StatValue = firstval.Text;
            }
            if (second.SelectedItem != null && secondval.Text != string.Empty)
            {
                gem.Second.StatType = second.SelectedItem.ToString();
                gem.Second.StatValue = secondval.Text;
            }

            if (third.SelectedItem != null && thirdval.Text != string.Empty)
            {
                gem.Third.StatType = third.SelectedItem.ToString();
                gem.Third.StatValue = thirdval.Text;
            }




            if (!check_gem_type(gem))
            {


                if (Data.Contains(gem))
                {

                    MessageBoxResult result = MessageBox.Show("Want to add it anyways?", "Gem alredy exists", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    switch (result)
                    {


                        case MessageBoxResult.Yes:

                            Data.Add(gem);
                            gemgrid.DataContext = Data;
                            break;

                        case MessageBoxResult.No:
                            /* ... */
                            break;

                    }
                }
                else
                {

                    Data.Add(gem);
                    gemgrid.DataContext = Data;






              

                }

                first.SelectedIndex = 0;
                second.SelectedIndex = 0;
                third.SelectedIndex = 0;
                ability.SelectedIndex = 0;
                type.SelectedIndex = 0;


                lvl.Text = "0";
                pr.Text = "0";
                firstval.Text = "0";
                secondval.Text = "0";
                thirdval.Text = "0";
            }
                else {

                MessageBox.Show("More than one stat are the same type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



                
            }


            bool check_gem_type(EmpGem gem)

            {

                return gem.First.StatType == gem.Second.StatType || gem.Second.StatType == gem.Third.StatType || gem.First.StatType == gem.Third.StatType ;
                    

            }
        }
    }
