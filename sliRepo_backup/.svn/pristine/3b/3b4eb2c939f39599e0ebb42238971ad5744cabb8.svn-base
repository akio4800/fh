using SelvesSoftware.BusinessLogic;
using SelvesSoftware.DataContainer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SelvesSoftware.GUI.Personenverwaltung.PA.Windows
{
    /// <summary>
    /// Interaction logic for AGAuswählen.xaml
    /// </summary>
    public partial class AGAuswählen : Window
    {
        private ObservableCollection<Track> Data = new ObservableCollection<Track>();
        private List<PurchaserData> InputList;
        List<Purchaser> availablePur = new List<Purchaser>();
        public IPurchaserDataBl bl = new PurchaserDataBl();

        private PaHinzufügen newPA;
        private PaBearbeiten editPA;

        public AGAuswählen(PaBearbeiten w)
        {
            editPA = w;
            InitializeComponent();
            setPurchaserOnGrid(editPA.employedPurchasers);
        }

        public AGAuswählen(PaHinzufügen w)
        {
            newPA = w;
            InitializeComponent();
            setPurchaserOnGrid(newPA.employedPurchasers);
        }

        private void setPurchaserOnGrid(List<Purchaser> employedPurchasers)
        {
            InputList = bl.SelectAllPurchaser();

            for (int i = InputList.Count - 1; i >= 0; --i)
            {
                if (employedPurchasers == null)
                {
                    Data.Add(new Track(InputList[i].Purchaser.Id, InputList[i].Purchaser.FirstName, InputList[i].Purchaser.LastName));
                }
                else
                {
                    bool isAlreadyIn = false;
                    foreach (Purchaser p in employedPurchasers)
                    {
                        if (p.Id == InputList[i].Purchaser.Id)
                        {
                            isAlreadyIn = true;
                        }
                    }
                    if (!isAlreadyIn)
                    {
                        Data.Add(new Track(InputList[i].Purchaser.Id, InputList[i].Purchaser.FirstName, InputList[i].Purchaser.LastName));
                        InputList.RemoveAt(i);
                    }

                }
            }

            GridÜbersicht.DataContext = Data;
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            if (GridÜbersicht.SelectedIndex != -1)
            {
                Purchaser p = new Purchaser();
                p.Id = Data[GridÜbersicht.SelectedIndex].Id;
                p.FirstName = Data[GridÜbersicht.SelectedIndex].FirstName;
                p.LastName = Data[GridÜbersicht.SelectedIndex].LastName;

                if (newPA != null)
                {
                    newPA.employedPurchasers.Add(p);
                    newPA.addedAGs.Add(p);
                    newPA.guiListEmployed.Add(new Track(p.Id, p.FirstName, p.LastName));
                    newPA.AGListe.ItemsSource = newPA.guiListEmployed;

                }
                else if (editPA != null)
                {
                    editPA.employedPurchasers.Add(p);
                    editPA.addedAGs.Add(p);
                    editPA.guiListEmployed.Add(new Track(p.Id, p.FirstName, p.LastName));
                    editPA.AGListe.ItemsSource = editPA.guiListEmployed;
                }

                this.Close();
            }
            
        }
        
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
