using SelvesSoftware.DataContainer;
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

namespace SelvesSoftware.GUI.Personenverwaltung.AG.Windows
{
    /// <summary>
    /// Interaction logic for Contact_Window.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public Person person;
        public ContactWindow()
        {
            InitializeComponent();
            person = new Person();
        }

        public ContactWindow(Person p)
        {
            InitializeComponent();

            if (p != null)
            {
                FirstName.Text = p.FirstName;
                LastName.Text = p.LastName;
                PhoneNr.Text = p.PhoneNumber;
                MobileNr.Text = p.MobilePhone;
                Email.Text = p.EMail;
                Street.Text = p.HomeAdress.Street;
                ZIP.Text = p.HomeAdress.ZipCode.ToString();
                City.Text = p.HomeAdress.City;
                Country.Text = p.HomeAdress.Country;
                HNr.Text = p.HomeAdress.HouseNumber.ToString();
                DoorNr.Text = p.HomeAdress.DoorNumber.ToString();
                StairNr.Text = p.HomeAdress.StairNumber.ToString();
                Etage.Text = p.HomeAdress.Etage.ToString();
                tbInfo.Text = p.InfoField;
                person = p;
            } else
            {
                person = new Person();
            }

           
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            long id = person.Id;
            person = new Person();
            person.Id = id;
            person.FirstName = FirstName.Text;
            person.LastName= LastName.Text;
            person.PhoneNumber=PhoneNr.Text;
            person.MobilePhone = MobileNr.Text;
            person.EMail= Email.Text;
            person.HomeAdress.Street=Street.Text;
            if(ZIP.Text!="")
            person.HomeAdress.ZipCode= Convert.ToUInt16(ZIP.Text) ;
            person.HomeAdress.City = City.Text;
            person.HomeAdress.Country= Country.Text;
            if (HNr.Text != "")
                person.HomeAdress.HouseNumber=Convert.ToUInt16(HNr.Text);
            if (DoorNr.Text != "")
                person.HomeAdress.DoorNumber=Convert.ToUInt16(DoorNr.Text);
            if (StairNr.Text != "")
                person.HomeAdress.StairNumber=Convert.ToUInt16(StairNr.Text);
            if (Etage.Text != "")
                person.HomeAdress.Etage=Convert.ToUInt16(Etage.Text);
            person.InfoField= tbInfo.Text;

            this.Close();
        }
    }
}
