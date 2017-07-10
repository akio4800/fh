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

namespace SelvesSoftware.GUI.Personenverwaltung.PA.Windows
{
    /// <summary>
    /// Interaktionslogik für DienstHinzufügen.xaml
    /// </summary>
    public partial class DienstHinzufügen : Window
    {
        public DienstHinzufügen()
        {
            InitializeComponent();
        }

        public DienstHinzufügen(PaBearbeiten w)
        {
            editPA = w;
            InitializeComponent();
            
        }

        public DienstHinzufügen(PaHinzufügen w)
        {
            newPA = w;
            InitializeComponent();
        }

        private PaBearbeiten editPA;
        private PaHinzufügen newPA;


        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Employment emp = new Employment();
            Start.Background = Brushes.White;
            End.Background = Brushes.White;
            notification.Content = "";
            String endString = "";
            if (Start != null && Start.Text!="")
            {
                try
                {
                    emp.EmplBegin = Convert.ToDateTime(Start.Text);
                }
                catch(Exception ex)
                {
                    notification.Content = "Datum inkorrekt";
                    Start.Background = Brushes.LightPink;
                }
                
               
                if (End != null && End.Text!="")
                {
                    try
                    {
                        emp.EmplEnd = Convert.ToDateTime(End.Text);
                        
                    }
                    catch(Exception ex)
                    {
                        notification.Content = "Datum inkorrekt";
                        End.Background = Brushes.LightPink;

                    }
                   
                    endString = " - " + End.Text;
                }
                

                //TO DO Daten nur in die GUI schreiben, wenn das Parsen oben funktioniert hat!

            } else
            {
                if (End != null && End.Text != "")
                {
                    notification.Content = "Dienstbeginn fehlt";
                    Start.Background = Brushes.LightPink;
                } else
                {
                    notification.Content = "Keine Daten zu speichern";
                    Start.Background = Brushes.LightPink;
                }
            }
            if (notification.Content.Equals(""))
            {
                if (newPA != null)
                {
                    emp.EmplId = newPA.pa.Id;
                    //newPA.employments.Items.Add(Start.Text + endString);
                    newPA.employmentList.Add(emp);
                    newPA.addedEmps.Add(emp);
                    //Workaround to update the Listbox
                    newPA.employments.DisplayMemberPath = "";
                    newPA.employments.DisplayMemberPath = "GuiDate";
                }
                else if (editPA != null)
                {
                    emp.EmplId = editPA.pa.Id;
                    //editPA.employments.Items.Add(Start.Text + endString);
                    editPA.addedEmps.Add(emp);
                    editPA.employmentList.Add(emp);
                    //Workaround to update the Listbox
                    editPA.employments.DisplayMemberPath = "";
                    editPA.employments.DisplayMemberPath = "GuiDate";
                }
                this.Close();
                this.Close();
            }
            
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
