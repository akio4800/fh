using SelvesSoftware.GUI.Elemente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using SelvesSoftware.GUI.Personenverwaltung.PA;

namespace SelvesSoftware.GUI
{
    class InputEvaluator
    {
        Brush InvalidInputBackground = Brushes.LightPink;
        Brush NormalInputBackground = Brushes.White;
        bool invalid = false;

        public bool Evaluate(AgHinzufügen h)
        {

           
            bool validInput = false;
            if(
                //Basic Data
                isValidName(h.FirstName,0,20) &
                isValidName(h.LastName,0,20) &
                isValidEmail(h.Email) &
                isValidPhoneNumber(h.PhoneNr) &
                isValidPhoneNumber(h.MobileNr) &
                isValidNumber(h.SVN,10,10) &
                isValidString(h.Street) &
                isValidString(h.HNr) &
                isValidString(h.StairNr) &
                isValidString(h.DoorNr) &
                isValidNumber(h.Etage) &
                isValidNumber(h.ZIP) &
                isValidString(h.City) &
                isValidString(h.Country) &
                isValidString(h.nationality) &

                //Specific Purchaser Data
                isValidDouble(h.PayperHour) &
                isValidDouble(h.KmPay) &
                isValidDouble(h.billablePayperHour) &
                isValidNumber(h.Needof) &
                isValidDouble(h.Income) &
                isValidDouble(h.IncomeBetrag) &
                

                //BankData
                isValidString(h.BIC) &
                isValidString(h.IBAN) &
                isValidString(h.kontoinhaber) &

                //mustHave
                isMustHave(h.LastName) &
                isMustHave(h.FirstName) &
                isMustHave(h.EntryDate) &


                //validate Dates
                isBefore(h.ApprovelFrom.SelectedDate, h.ApprovelTo.SelectedDate) &
                isBefore(h.EntryDate.SelectedDate, h.ApprovelFrom.SelectedDate)

                //
                )
            {
                validInput = true;
            }
            return validInput;
                
        }



        public bool Evaluate(AgBearbeiten h)
        {
            bool validInput = false;

            if (
                //Basic Data
                isValidName(h.FirstName,0,20) &
                isValidName(h.LastName,0,20) &
                isValidEmail(h.Email) &
                isValidPhoneNumber(h.PhoneNr) &
                isValidPhoneNumber(h.MobileNr) &
                isValidNumber(h.SVN,10,10) &
                isValidString(h.Street) &
                isValidString(h.HNr) &
                isValidString(h.StairNr) &
                isValidString(h.DoorNr) &
                isValidNumber(h.Etage) &
                isValidNumber(h.ZIP) &
                isValidString(h.City) &
                isValidString(h.Country) &
                isValidString(h.nationality) &

                //Specific Purchaser Data
                isValidDouble(h.PayperHour) &
                isValidDouble(h.KmPay) &
                isValidDouble(h.billablePayperHour) &
                isValidNumber(h.Needof) &
                isValidDouble(h.Income) &
                isValidDouble(h.IncomeBetrag) &
                

                //BankData
                isValidString(h.BIC) &
                isValidString(h.IBAN) &
                isValidString(h.kontoinhaber) &

                //mustHave
                isMustHave(h.LastName) &
                isMustHave(h.FirstName) &
                isMustHave(h.EntryDate) &


                //validate Dates
                isBefore(h.ApprovelFrom.SelectedDate, h.ApprovelTo.SelectedDate) &
                isBefore(h.EntryDate.SelectedDate, h.ApprovelFrom.SelectedDate)

                )
            {
                validInput = true;
            }
            return validInput;

        }

        private bool isMustHave(DatePicker entryDate)
        {
            if(entryDate.SelectedDate != null)
            {
                entryDate.Background = NormalInputBackground;
                return true;
            }
            entryDate.Background = InvalidInputBackground;
            return false;
        }

        public bool Evaluate(PaHinzufügen h)
        {
            bool validInput = false;

            if (
                //Basic Data
                isValidName(h.FirstName, 0, 20) &
                isValidName(h.LastName, 0, 20) &
                isValidEmail(h.Email) &
                isValidPhoneNumber(h.PhoneNr) &
                isValidPhoneNumber(h.MobileNr) &
                isValidNumber(h.SVN,10,10) &
                isValidString(h.Street) &
                isValidString(h.HNr) &
                isValidString(h.StairNr) &
                isValidString(h.DoorNr) &
                isValidNumber(h.Etage) &
                isValidNumber(h.ZIP) &
                isValidString(h.City) &
                isValidString(h.Country) &
                isValidString(h.nationality) &

                //BankData
                isValidString(h.BIC) &
                isValidString(h.IBAN) &
                isValidString(h.kontoinhaber) &

                //mustHave
                isMustHave(h.LastName) &
                isMustHave(h.FirstName) 


                )
            {
                validInput = true;
            }
            return validInput;
        }

        public bool Evaluate(PaBearbeiten h)
        {
            bool validInput = false;
            if (
                //Basic Data
                isValidName(h.FirstName, 0, 20) &
                isValidName(h.LastName, 0, 20) &
                isValidEmail(h.Email) &
                isValidPhoneNumber(h.PhoneNr) &
                isValidPhoneNumber(h.MobileNr) &
                isValidNumber(h.SVN,10,10) &
                isValidString(h.Street) &
                isValidString(h.HNr) &
                isValidString(h.StairNr) &
                isValidString(h.DoorNr) &
                isValidNumber(h.Etage) &
                isValidNumber(h.ZIP) &
                isValidString(h.City) &
                isValidString(h.Country) &
                isValidString(h.nationality) &

                //BankData
                isValidString(h.BIC) &
                isValidString(h.IBAN) &
                isValidString(h.kontoinhaber) &

                //mustHave
                isMustHave(h.LastName) &
                isMustHave(h.FirstName) 


                )
            {
                validInput = true;
            }
           
            return validInput;
        }



        public bool isBefore(DateTime? begin, DateTime? end)
        {
            if (begin > end)
            {
                MessageBox.Show("Bitte Datumsangaben überprüfen!");
                return false;
            }
            return true;
        }

        public bool isMustHave(InputBox ib)
        {
            if(ib.Text== null || ib.Text == "")
            {
                ib.Background = InvalidInputBackground;
                return false;
            }
            return true;
        }

        public bool isValidString(InputBox ib,int minLength=0, int maxLength=100)
        {
            ib.Background = NormalInputBackground;
            if (ib.Text == null || ib.Text == "")
            {
                return true;   
            }
            if(ib.getContent().Length >= minLength && ib.getContent().Length <= maxLength)
            {
                return true;
            }
            ib.Background = InvalidInputBackground;
            return false;
        }

        

        public bool isValidEmail(InputBox ib)
        {
            ib.Background = NormalInputBackground;

            String s = ib.Text;

            if (ib.Text == null || ib.Text == "")
            {
                return true;
            }
            if(s.Contains('@') && s.Contains('.'))
            {
                return true;
            }


            ib.Background = InvalidInputBackground;
            return false;

            /* does not work for some reason
            String strIn = ib.Text;
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return true;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                ib.Background = InvalidInputBackground;
                return false;
            }

            if (invalid)
                ib.Background = InvalidInputBackground;
            return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                ib.Background = InvalidInputBackground;
                return false;
            }
            return true;
            */
        }



        public bool isValidNumber(InputBox ib, int minLength=0, int maxLength=30)
        {
            ib.Background = NormalInputBackground;

            if (ib.Text == null || ib.Text == "")
            {
                return true;
            }

            if (ib.getContent().Length >= minLength && ib.getContent().Length <= maxLength)
            {
                long n;
                if (long.TryParse(ib.getContent(), out n))
                {
                    return true;
                }
            }


            ib.Background = InvalidInputBackground;
            return false;
        }

        private bool isValidName(InputBox ib, int minLength=0,int maxLength=100)
        {
            ib.Background = NormalInputBackground;

            if(ib.Text == null || ib.Text == "")
            {
                return true;
            }

            if (isValidString(ib,minLength,maxLength) && !(ib.Text.Any(char.IsDigit)))
            {
                return true;
            }
            ib.Background = InvalidInputBackground;
            return false;
        }


        private bool isValidPhoneNumber(InputBox phoneNr)
        {
            phoneNr.Background = NormalInputBackground;

            string s = phoneNr.Text;

            s.Trim(new char[] { ' ', '+', '/', '(', ')', '\\' });
            s = Regex.Replace(s, @"\s", "");

            if (phoneNr.Text == null || phoneNr.Text == "")
            {
                return true;
            }
            long x = 0;
            if(long.TryParse(s,out x))
            {
                return true;
            }

            phoneNr.Background = InvalidInputBackground;
            return false;

        }

        public bool isValidDouble(InputBox ib)
        {
            ib.Background = NormalInputBackground;

            if (ib.Text == null || ib.Text == "")
            {
                return true;
            }

            double price;
            bool isDouble = Double.TryParse(ib.Text, out price);
            if (isDouble)
            {
                return true;
            }

            ib.Background = InvalidInputBackground;
            return false;
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
