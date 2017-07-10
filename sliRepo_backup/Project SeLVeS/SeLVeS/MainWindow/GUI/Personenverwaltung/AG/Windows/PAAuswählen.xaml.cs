using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SelvesSoftware.BusinessLogic;

namespace SelvesSoftware.GUI.Personenverwaltung.AG.Windows
{
    /// <summary>
    /// Interaction logic for PAAuswählen.xaml
    /// </summary>
    public partial class PAAuswählen : Window
    {
        private IPersonalAssistentBl bl = new PersonalAssistentBl();

        private ObservableCollection<Track> Data = new ObservableCollection<Track>();
        private List<PersonalAssistant> InputList;
        
        //Pages 
        private AgHinzufügen newAG;
        private AgBearbeiten editAG;

        public PAAuswählen(AgBearbeiten agBearbeiten)
        {
            editAG = agBearbeiten;
            InitializeComponent();
            setPersonalAssistantDataOnGrid(editAG.employedPA);
        }
        public PAAuswählen(AgHinzufügen agHinzufügen)
        {
            newAG = agHinzufügen;
            InitializeComponent();
            setPersonalAssistantDataOnGrid(newAG.employedPA);
        }

        private void setPersonalAssistantDataOnGrid(List<PersonalAssistant> employedPA)
        {
            InputList = bl.SelectAllPa();

            for (int i = InputList.Count - 1; i >= 0; --i)
            {
                if (employedPA == null)
                {
                    Data.Add(new Track(InputList[i].Id, InputList[i].FirstName, InputList[i].LastName));
                }
                else
                {
                    bool isAlreadyIn = false;
                    foreach (PersonalAssistant pa in employedPA)
                    {
                        if (pa.Id == InputList[i].Id)
                        {
                            isAlreadyIn = true;
                        }
                    }
                    if (!isAlreadyIn)
                    {
                        Data.Add(new Track(InputList[i].Id, InputList[i].FirstName, InputList[i].LastName));
                        InputList.RemoveAt(i);
                    }

                }
            }
            
            GridÜbersicht.DataContext = Data;
        }
       

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Auswählen Button
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (GridÜbersicht.SelectedIndex != -1)
            {
                PersonalAssistant pa = new PersonalAssistant();
                pa.Id = Data[GridÜbersicht.SelectedIndex].Id;
                pa.FirstName = Data[GridÜbersicht.SelectedIndex].FirstName;
                pa.LastName = Data[GridÜbersicht.SelectedIndex].LastName;

                if (newAG != null)
                {
                    newAG.employedPA.Add(pa);
                    newAG.addedPAs.Add(pa);
                    newAG.guiListEmployed.Add(new Track(pa.Id, pa.FirstName, pa.LastName));
                    newAG.employedPersonalAssistants.ItemsSource = newAG.guiListEmployed;

                }
                else if (editAG != null)
                {
                    editAG.employedPA.Add(pa);
                    editAG.addedPAs.Add(pa);
                    editAG.guiListEmployed.Add(new Track(pa.Id, pa.FirstName, pa.LastName));
                    editAG.employedPersonalAssistants.ItemsSource = editAG.guiListEmployed;
                }

                this.Close();
            }
        }
    }
}
