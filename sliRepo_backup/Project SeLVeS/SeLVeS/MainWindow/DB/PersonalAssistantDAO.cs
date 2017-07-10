using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Npgsql;
using System.Collections.Generic;
using SelvesSoftware.DataContainer;
using System.Windows;

namespace SelvesSoftware.DB
{
    //-----------------------------------------------------------------------------------------------------
    /// <summary>
    /// author: TS
    /// </summary>

    //-----------------------------------------------------------------------------------------------------
    public class PersonalAssistantDAO : IPersonalAssistantDAO
    {
        //-----------------------------------------------------------------------------------------------------
        public PersonalAssistantDAO()
        {

        }

        //-----------------------------------------------------------------------------------------------------
        public PersonalAssistant select(PersonalAssistant p)
        {

            //TODO EXCEPTION PERSON NOT FOUND
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM (Person p left outer join Adresse ad on (p.adressid=ad.adressid))inner join Persoenlicherassistent pa on (p.personid=pa.paid) where pa.paid=@id";
            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, p.Id);

            NpgsqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                //the Reader needs to 

                if (!reader.IsDBNull(0))
                {
                    p.Id = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(2))
                {
                    p.FirstName = reader.GetString(2);
                }
                if (!reader.IsDBNull(1))
                {
                    p.EMail = reader.GetString(1);
                }
                if (!reader.IsDBNull(3))
                {
                    p.LastName = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    p.PhoneNumber = reader.GetString(4);
                }
                if (!(reader.IsDBNull(6)))
                {
                    p.MobilePhone = reader.GetString(6);
                }
                if (!(reader.IsDBNull(5)))
                {
                    p.HomeAdress.AdressId = Convert.ToInt32(reader.GetInt32(5));
                }
                if (!(reader.IsDBNull(7)))
                {
                    p.IBAN = reader.GetString(7);
                }
                if (!(reader.IsDBNull(8)))
                {
                    p.BIC = reader.GetString(8);
                }
                if (!(reader.IsDBNull(9)))
                {
                    p.AccountHolder = reader.GetString(9);
                }
                if (!(reader.IsDBNull(10)))
                {
                    p.SVN = Convert.ToInt64(reader.GetDecimal(10));
                }
                if (!(reader.IsDBNull(11)))
                {
                    p.nationality = reader.GetString(11);
                }
                if (!(reader.IsDBNull(12)))
                {
                    p.InfoField = reader.GetString(12);
                }
                if (!(reader.IsDBNull(13)))
                {
                    p.HomeAdress = new Adress();
                    p.HomeAdress.AdressId = reader.GetInt32(13);
                    if (!(reader.IsDBNull(14)))
                    {
                        p.HomeAdress.Street = reader.GetString(14);
                    }
                    if (!(reader.IsDBNull(15)))
                    {
                        p.HomeAdress.City = reader.GetString(15);
                    }
                    if (!(reader.IsDBNull(16)))
                    {
                        p.HomeAdress.Country = reader.GetString(16);
                    }
                    if (!(reader.IsDBNull(17)))
                    {
                        p.HomeAdress.HouseNumber = reader.GetInt32(17);
                    }
                    if (!(reader.IsDBNull(18)))
                    {
                        p.HomeAdress.StairNumber = reader.GetInt32(18);
                    }
                    if (!(reader.IsDBNull(19)))
                    {
                        p.HomeAdress.Etage = reader.GetInt32(19);
                    }
                    if (!(reader.IsDBNull(20)))
                    {
                        p.HomeAdress.ZipCode = reader.GetInt32(20);
                    }
                    if (!(reader.IsDBNull(21)))
                    {
                        p.HomeAdress.DoorNumber = reader.GetInt32(21);
                    }
                }
                if (!reader.IsDBNull(22))
                {
                    p.Active = reader.GetBoolean(22);
                }
                if (!reader.IsDBNull(24))
                {
                    p.ClosingDateDocuments = reader.GetDateTime(24);
                }
                if (!reader.IsDBNull(25))
                {
                    p.SV = reader.GetBoolean(25);
                }
                if (!reader.IsDBNull(26))
                {
                    p.Dienstvertrag = reader.GetBoolean(26);
                }
                if (!reader.IsDBNull(27))
                {
                    p.BestBH = reader.GetBoolean(27);
                }
                if (!reader.IsDBNull(28))
                {
                    p.Grundkurs = reader.GetBoolean(28);
                }
                if (!reader.IsDBNull(29))
                {
                    p.deadLineHours = reader.GetDateTime(29);
                }
                if (!reader.IsDBNull(30))
                {
                    p.consumedHours = reader.GetDecimal(30);
                }
            }
            reader.Close();

            command = new NpgsqlCommand(null, con);
            command.CommandText = "Select * from dienst where paid=@id";
            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, p.Id);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employment e = new Employment();
                if (!reader.IsDBNull(0))
                {
                    e.EmplBegin = reader.GetDateTime(0);
                }
                if (!reader.IsDBNull(1))
                {
                    e.EmplId = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    e.EmplEnd = reader.GetDateTime(2);
                }
                if (p.EmploymentTimes == null)
                {
                    p.EmploymentTimes = new List<Employment>();
                }

                p.EmploymentTimes.Add(e);
            }

            p.Purchasers = SelectPurchasers(p);

            con = DB.DBConnector.GetConnection();

            command = new NpgsqlCommand(null, con);

            command.CommandText = "Select * from dokumente where paid=@id";
            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, p.Id);
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Document d = new Document();
                    if (!reader.IsDBNull(0))
                    {
                        d.DocumentName = reader.GetString(0);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        d.Date = reader.GetDateTime(1);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        d.FilePath = reader.GetString(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        d.Required = reader.GetBoolean(4);
                    }
                    if (p.Documents == null)
                    {
                        p.Documents = new List<Document>();
                    }
                    p.Documents.Add(d);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            con.Close();
            return p;

        }

        public List<Purchaser> SelectPurchasers(PersonalAssistant p)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Dienstverhaeltnis p WHERE p.paid=@id";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, p.Id);

            NpgsqlDataReader reader = command.ExecuteReader();
            List<int> purIds = new List<int>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    purIds.Add(reader.GetInt32(0));
                }
            }
            reader.Close();
            con.Close();

            PurchaserDAO purDAO = new PurchaserDAO();
            p.Purchasers = new List<Purchaser>();
            foreach (int i in purIds)
            {
                Purchaser pur = new Purchaser();
                pur.Id = i;
                pur = purDAO.Select(pur);
                EmploymentStatus empStatus = new EmploymentStatus();
                empStatus.Assistant = p;
                empStatus.Purchaser = pur;
                p.Purchasers.Add(pur);
            }

            return p.Purchasers;
        }

        //-----------------------------------------------------------------------------------------------------
        public PersonalAssistant insert(PersonalAssistant person)
        {

            PersonDAO personDao = new PersonDAO();
            Person pa = personDao.Insert(person); //returns inserted person with the new ID
            person.Id = pa.Id;
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO persoenlicherassistent (paid,aktiv,abgabedatumunterlagen,SV,Dienstvertrag,BestaetigungBH,Grundkurs,DeadLineWeiterbildung,WeiterbildungsStunden) VALUES (@paid,@aktiv,@datumunterlagen,@SV,@Dienstvertrag,@BH,@Grundkurs,@DeadLineWeiterbildung,@WeiterbildungsStunden)";
            //command.CommandText = "INSERT INTO persoenlicherassistent (paid,aktiv) VALUES (@paid,@aktiv)";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.Id);
            DB.DBConnector.AddToCommand("@aktiv", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Active);
            DB.DBConnector.AddToCommand("@datumunterlagen", NpgsqlTypes.NpgsqlDbType.Date, command, person.ClosingDateDocuments);
            DB.DBConnector.AddToCommand("@SV", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.SV);
            DB.DBConnector.AddToCommand("@Dienstvertrag", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Dienstvertrag);
            DB.DBConnector.AddToCommand("@BH", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.BestBH);
            DB.DBConnector.AddToCommand("@Grundkurs", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Grundkurs);
            DB.DBConnector.AddToCommand("@DeadLineWeiterbildung", NpgsqlTypes.NpgsqlDbType.Date, command, person.deadLineHours);
            DB.DBConnector.AddToCommand("@WeiterbildungsStunden", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.consumedHours);


            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
            con.Close();
            PersonalAssistant personOld = new PersonalAssistant();
            personOld.Id = person.Id;
            this.select(personOld);
            if (person.EmploymentTimes != null)
            {
                foreach (Employment e in person.EmploymentTimes)
                {
                    bool needs_insert = true;

                    e.EmplId = pa.Id;
                    if (e.EmplEnd.Year == 1)
                    {

                    }

                    if (personOld.EmploymentTimes != null)
                    {
                        foreach (Employment e2 in personOld.EmploymentTimes)
                        {
                            if (e.EmplBegin == e2.EmplBegin)
                            {
                                needs_insert = false;
                            }


                        }
                    }

                    if (needs_insert) { insertEmployment(e); };
                }
            }

            return person;
        }
        public void insertEmployment(Employment e)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO dienst VALUES (@begin,@paid,@ende)";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Integer, command, e.EmplId);
            DB.DBConnector.AddToCommand("@begin", NpgsqlTypes.NpgsqlDbType.Date, command, e.EmplBegin);

            DB.DBConnector.AddToCommand("@ende", NpgsqlTypes.NpgsqlDbType.Date, command, e.EmplEnd);

            try { command.ExecuteNonQuery(); } catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Error"); }

            con.Close();

        }
        public void updateEmployment(PersonalAssistant person)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE dienst SET (Ende=@ende)WHERE paid=@paid AND beginn=@begin";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Integer, command, person.Id);
            DB.DBConnector.AddToCommand("@begin", NpgsqlTypes.NpgsqlDbType.Date, command, person.EmploymentTimes[person.EmploymentTimes.Count - 1].EmplBegin);
            DB.DBConnector.AddToCommand("@ende", NpgsqlTypes.NpgsqlDbType.Date, command, person.EmploymentTimes[person.EmploymentTimes.Count - 1].EmplEnd);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }
        public void deleteEmployment(Employment e)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM dienst WHERE paid=@paid AND begin=@begin";


            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Integer, command,e.EmplId);
            DB.DBConnector.AddToCommand("@begin", NpgsqlTypes.NpgsqlDbType.Date, command, e.EmplBegin);

            try { command.ExecuteNonQuery(); } catch (Exception ex) { MessageBox.Show(ex.Message.ToString(), "Error"); }

            con.Close();

        }

        //-----------------------------------------------------------------------------------------------------
        public PersonalAssistant update(PersonalAssistant person)
        {
            PersonDAO pdao = new PersonDAO();
            pdao.Update(person);

            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE persoenlicherassistent SET aktiv=@aktiv,abgabedatumunterlagen=@datumunterlagen,SV=@SV,Dienstvertrag=@Dienstvertrag,BestaetigungBH=@BH,Grundkurs=@Grundkurs,DeadLineWeiterbildung=@DeadLineWeiterbildung,WeiterbildungsStunden=@WeiterbildungsStunden WHERE paid=@paid";
            //command.CommandText = "UPDATE persoenlicherassistent SET aktiv=@aktiv WHERE paid=@paid";


            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.Id);
            DB.DBConnector.AddToCommand("@aktiv", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Active);
            DB.DBConnector.AddToCommand("@datumunterlagen", NpgsqlTypes.NpgsqlDbType.Date, command, person.ClosingDateDocuments);
            DB.DBConnector.AddToCommand("@SV", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.SV);
            DB.DBConnector.AddToCommand("@Dienstvertrag", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Dienstvertrag);
            DB.DBConnector.AddToCommand("@BH", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.BestBH);
            DB.DBConnector.AddToCommand("@Grundkurs", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Grundkurs);
            DB.DBConnector.AddToCommand("@DeadLineWeiterbildung", NpgsqlTypes.NpgsqlDbType.Date, command, person.deadLineHours);
            DB.DBConnector.AddToCommand("@WeiterbildungsStunden", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.consumedHours);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
            return person;
        }

        public PersonalAssistant selectReduced(PersonalAssistant pa)
        {

            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT personid, vorname, nachname FROM Person p WHERE p.personid=@id";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, pa.Id);

            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                if (!(reader.IsDBNull(0)))
                {
                    if (!(reader.IsDBNull(1)))
                    {
                        pa.FirstName = reader.GetString(1);
                    }
                    if (!(reader.IsDBNull(2)))
                    {
                        pa.LastName = reader.GetString(2);
                    }
                }
            }
            reader.Close();
            /*
            command = new NpgsqlCommand(null, con);
            command.CommandText = "Select * from dienst where paid=@id";
            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, pa.Id);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employment e = new Employment();
                if (!reader.IsDBNull(0))
                {
                    e.EmplBegin = reader.GetDateTime(0);
                }
                if (!reader.IsDBNull(1))
                {
                    e.EmplId = reader.GetInt32(1);
                }
                if (!reader.IsDBNull(2))
                {
                    e.EmplEnd = reader.GetDateTime(2);
                }
                if (pa.EmploymentTimes == null)
                {
                    pa.EmploymentTimes = new List<Employment>();
                }

                pa.EmploymentTimes.Add(e);
            }

            command = new NpgsqlCommand(null, con);

            command.CommandText = "Select * from dokumente where paid=@id";
            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, pa.Id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Document d = new Document();
                if (!reader.IsDBNull(0))
                {
                    d.DocumentName = reader.GetString(0);
                }
                if (!reader.IsDBNull(1))
                {
                    d.Date = reader.GetDateTime(1);
                }
                if (!reader.IsDBNull(3))
                {
                    d.FilePath = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    d.Required = reader.GetBoolean(4);
                }
                if (pa.Documents == null)
                {
                    pa.Documents = new List<Document>();
                }
                pa.Documents.Add(d);
            }
            */
            con.Close();
            return pa;
        }

        private void fillPA(PersonalAssistant p, NpgsqlDataReader reader)
        {
            if (reader.Read())
            {
                //the Reader needs to 

                if (!reader.IsDBNull(0))
                {
                    p.Id = reader.GetInt32(0);
                }
                if (!reader.IsDBNull(2))
                {
                    p.FirstName = reader.GetString(2);
                }
                if (!reader.IsDBNull(1))
                {
                    p.EMail = reader.GetString(1);
                }
                if (!reader.IsDBNull(3))
                {
                    p.LastName = reader.GetString(3);
                }
                if (!reader.IsDBNull(4))
                {
                    p.PhoneNumber = reader.GetString(4);
                }
                if (!(reader.IsDBNull(6)))
                {
                    p.MobilePhone = reader.GetString(6);
                }
                if (!(reader.IsDBNull(5)))
                {
                    p.HomeAdress.AdressId = Convert.ToInt32(reader.GetInt32(5));
                }
                if (!(reader.IsDBNull(7)))
                {
                    p.IBAN = reader.GetString(7);
                }
                if (!(reader.IsDBNull(8)))
                {
                    p.BIC = reader.GetString(8);
                }
                if (!(reader.IsDBNull(9)))
                {
                    p.AccountHolder = reader.GetString(9);
                }
                if (!(reader.IsDBNull(10)))
                {
                    p.SVN = Convert.ToInt64(reader.GetDecimal(10));
                }
                if (!(reader.IsDBNull(11)))
                {
                    p.nationality = reader.GetString(11);
                }
                if (!(reader.IsDBNull(12)))
                {
                    p.InfoField = reader.GetString(12);
                }
                if (!(reader.IsDBNull(13)))
                {
                    p.HomeAdress = new Adress();
                    if (!(reader.IsDBNull(14)))
                    {
                        p.HomeAdress.Street = reader.GetString(14);
                    }
                    if (!(reader.IsDBNull(15)))
                    {
                        p.HomeAdress.City = reader.GetString(15);
                    }
                    if (!(reader.IsDBNull(16)))
                    {
                        p.HomeAdress.Country = reader.GetString(16);
                    }
                    if (!(reader.IsDBNull(17)))
                    {
                        p.HomeAdress.HouseNumber = reader.GetInt32(17);
                    }
                    if (!(reader.IsDBNull(18)))
                    {
                        p.HomeAdress.StairNumber = reader.GetInt32(18);
                    }
                    if (!(reader.IsDBNull(19)))
                    {
                        p.HomeAdress.Etage = reader.GetInt32(19);
                    }
                    if (!(reader.IsDBNull(20)))
                    {
                        p.HomeAdress.ZipCode = reader.GetInt32(20);
                    }
                    if (!(reader.IsDBNull(21)))
                    {
                        p.HomeAdress.DoorNumber = reader.GetInt32(21);
                    }
                }
                if (!reader.IsDBNull(22))
                {
                    p.Active = reader.GetBoolean(22);
                }
                if (!reader.IsDBNull(24))
                {
                    p.ClosingDateDocuments = reader.GetDateTime(24);
                }
                if (!reader.IsDBNull(25))
                {
                    p.SV = reader.GetBoolean(25);
                }
                if (!reader.IsDBNull(26))
                {
                    p.Dienstvertrag = reader.GetBoolean(26);
                }
                if (!reader.IsDBNull(27))
                {
                    p.BestBH = reader.GetBoolean(27);
                }
                if (!reader.IsDBNull(28))
                {
                    p.Grundkurs = reader.GetBoolean(28);
                }
            }

            if (!reader.IsDBNull(7))
            {
                p.deadLineHours = reader.GetDateTime(7);
            }
            if (!reader.IsDBNull(8))
            {
                p.consumedHours = reader.GetDecimal(8);
            }
        }

        //-----------------------------------------------------------------------------------------------------
        public List<PersonalAssistant> SelectAll()
        {
            //TODO EXCEPTION PERSON NOT FOUND
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            List<PersonalAssistant> pas = new List<PersonalAssistant>();
            try
            {
                
                NpgsqlCommand command = new NpgsqlCommand(null, con);

                command.CommandText = "select paid from dienst group by paid";
                NpgsqlDataReader reader = command.ExecuteReader();
                List<int> hasEmploymentIds = new List<int>();
                while (reader.Read())
                {
                    Employment e = new Employment();
                    if (!reader.IsDBNull(0))
                    {
                        hasEmploymentIds.Add(reader.GetInt32(0));
                    }
                }

                command.CommandText = "SELECT paid FROM Dienstverhaeltnis p group by paid";
                    

                reader = command.ExecuteReader();
                List<int> employersIds = new List<int>();
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        employersIds.Add(reader.GetInt32(0));
                    }
                }

                command.CommandText = "SELECT * FROM (Person p left outer join Adresse ad on (p.adressid=ad.adressid))inner join Persoenlicherassistent pa on (p.personid=pa.paid)";
                reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    //the Reader needs to 
                    PersonalAssistant p = new PersonalAssistant();
                    if (!reader.IsDBNull(0))
                    {
                        p.Id = reader.GetInt32(0);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        p.FirstName = reader.GetString(2);
                    }
                    if (!reader.IsDBNull(1))
                    {
                        p.EMail = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(3))
                    {
                        p.LastName = reader.GetString(3);
                    }
                    if (!reader.IsDBNull(4))
                    {
                        p.PhoneNumber = reader.GetString(4);
                    }
                    if (!(reader.IsDBNull(6)))
                    {
                        p.MobilePhone = reader.GetString(6);
                    }
                    if (!(reader.IsDBNull(5)))
                    {
                        p.HomeAdress.AdressId = Convert.ToInt32(reader.GetInt32(5));
                    }
                    if (!(reader.IsDBNull(7)))
                    {
                        p.IBAN = reader.GetString(7);
                    }
                    if (!(reader.IsDBNull(8)))
                    {
                        p.BIC = reader.GetString(8);
                    }
                    if (!(reader.IsDBNull(9)))
                    {
                        p.AccountHolder = reader.GetString(9);
                    }
                    if (!(reader.IsDBNull(10)))
                    {
                        p.SVN = Convert.ToInt64(reader.GetDecimal(10));
                    }
                    if (!(reader.IsDBNull(11)))
                    {
                        p.nationality = reader.GetString(11);
                    }
                    if (!(reader.IsDBNull(12)))
                    {
                        p.InfoField = reader.GetString(12);
                    }
                    if (!(reader.IsDBNull(13)))
                    {
                        p.HomeAdress = new Adress();
                        if (!(reader.IsDBNull(14)))
                        {
                            p.HomeAdress.Street = reader.GetString(14);
                        }
                        if (!(reader.IsDBNull(15)))
                        {
                            p.HomeAdress.City = reader.GetString(15);
                        }
                        if (!(reader.IsDBNull(16)))
                        {
                            p.HomeAdress.Country = reader.GetString(16);
                        }
                        if (!(reader.IsDBNull(17)))
                        {
                            p.HomeAdress.HouseNumber = reader.GetInt32(17);
                        }
                        if (!(reader.IsDBNull(18)))
                        {
                            p.HomeAdress.StairNumber = reader.GetInt32(18);
                        }
                        if (!(reader.IsDBNull(19)))
                        {
                            p.HomeAdress.Etage = reader.GetInt32(19);
                        }
                        if (!(reader.IsDBNull(20)))
                        {
                            p.HomeAdress.ZipCode = reader.GetInt32(20);
                        }
                        if (!(reader.IsDBNull(21)))
                        {
                            p.HomeAdress.DoorNumber = reader.GetInt32(21);
                        }
                    }
                    if (!reader.IsDBNull(22))
                    {
                        p.Active = reader.GetBoolean(22);
                    }
                    if (!reader.IsDBNull(24))
                    {
                        p.ClosingDateDocuments = reader.GetDateTime(24);
                    }
                    if (!reader.IsDBNull(25))
                    {
                        p.SV = reader.GetBoolean(25);
                    }
                    if (!reader.IsDBNull(26))
                    {
                        p.Dienstvertrag = reader.GetBoolean(26);
                    }
                    if (!reader.IsDBNull(27))
                    {
                        p.BestBH = reader.GetBoolean(27);
                    }
                    if (!reader.IsDBNull(28))
                    {
                        p.Grundkurs = reader.GetBoolean(28);
                    }
                    if (!reader.IsDBNull(29))
                    {
                        p.deadLineHours = reader.GetDateTime(29);
                    }
                    if (!reader.IsDBNull(30))
                    {
                        p.consumedHours = reader.GetDecimal(30);
                    }
                    if (hasEmploymentIds.Contains(Convert.ToInt32(p.Id)))
                    {
                        p.EmploymentTimes = new List<Employment>();
                        p.EmploymentTimes.Add(new Employment());
                    }
                    if (employersIds.Contains(Convert.ToInt32(p.Id)))
                    {
                        p.Purchasers = new List<Purchaser>();
                        p.Purchasers.Add(new Purchaser());
                    }
                    pas.Add(p);
                }
                reader.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }

            con.Close();
            return pas;

        }
        /// <summary>
        /// ein neues Dokument kann eingefügt werden.
        /// </summary>
        /// <param name="pa"></param>
        public void insertDocument(PersonalAssistant pa)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO Dokumente VALUES (@name,@datum,@paid,@file,@erforderlich)";

            DB.DBConnector.AddToCommand("@name", NpgsqlTypes.NpgsqlDbType.Varchar, command, pa.Documents[pa.Documents.Count - 1].DocumentName);
            DB.DBConnector.AddToCommand("@datum", NpgsqlTypes.NpgsqlDbType.Date, command, pa.Documents[pa.Documents.Count - 1].Date);
            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pa.Id);
            DB.DBConnector.AddToCommand("@file", NpgsqlTypes.NpgsqlDbType.Varchar, command, pa.Documents[pa.Documents.Count - 1].FilePath);
            DB.DBConnector.AddToCommand("@erforderlich", NpgsqlTypes.NpgsqlDbType.Boolean, command, pa.Documents[pa.Documents.Count - 1].Required);
           
            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }
        /// <summary>
        /// Die Attribute Filepath und erforderlich können mit dieser Methode upgedated werden, nicht aber 
        /// andere Attribute
        /// </summary>
        /// <param name="pa"></param>
        public void updateDocument(PersonalAssistant pa)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE Dokumente SET File=@file,erforderlich=@erforderlich WHERE name=@name AND datum=@datum AND paid=@paid";

            DB.DBConnector.AddToCommand("@name", NpgsqlTypes.NpgsqlDbType.Varchar, command, pa.Documents[pa.Documents.Count - 1].DocumentName);
            DB.DBConnector.AddToCommand("@datum", NpgsqlTypes.NpgsqlDbType.Date, command, pa.Documents[pa.Documents.Count - 1].Date);
            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pa.Id);
            DB.DBConnector.AddToCommand("@file", NpgsqlTypes.NpgsqlDbType.Varchar, command, pa.Documents[pa.Documents.Count - 1].FilePath);
            DB.DBConnector.AddToCommand("@erforderlich", NpgsqlTypes.NpgsqlDbType.Boolean, command, pa.Documents[pa.Documents.Count - 1].Required);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }
        /// <summary>
        /// Einfügen eines Dienstverhältnisses, kann nur über PADAO eingefügt werden, nicht über PurchaserDataDAO!
        /// </summary>
        /// <param name="pur"></param>
        /// <param name="pa"></param>
        public void insertEmploymentStatus(Purchaser pur, PersonalAssistant pa)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO Dienstverhaeltnis VALUES (@agid,@paid)";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pa.Id);
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }

        public void deleteEmploymentStatus(Purchaser pur, PersonalAssistant pa)
        {

            //eventuell alte Dienstverträge in einen dafür vorgesehenen Ordner verschieben
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM Dienstverhaeltnis WHERE agid=@agid AND paid=@paid";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pa.Id);
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }

        /// <summary>
        /// lädt alle purchaser in die Liste des Persönlichen Assistenen, die mit ihm ein Dienstverhältnis haben.
        /// </summary>
        /// <param name="pa"></param>
        public void selectPurchaserList(PersonalAssistant pa)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();



            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Dienstverhaeltnis p WHERE p.paid=@id";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, pa.Id);

            NpgsqlDataReader reader = command.ExecuteReader();
            List<int> agIDs = new List<int>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    agIDs.Add(reader.GetInt32(0));
                }
            }
            reader.Close();
            con.Close();

            PurchaserDAO purDAO = new PurchaserDAO();
            if (pa.Purchasers == null)
            {
                pa.Purchasers = new List<Purchaser>();
            }
            foreach (int i in agIDs)
            {
                Purchaser pur = new Purchaser();
                pur.Id = i;
                pur = purDAO.Select(pur);
                pa.Purchasers.Add(pur);
            }


        }

        public System.Collections.Generic.List<PersonalAssistant> SelectSpecific(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }
        //for testing
        public void DeletePersonalAssistantRecursive(long id)
        {
            Npgsql.NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM dienstverhaeltnis WHERE paid=@paid";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command.CommandText = "DELETE FROM dokumente WHERE paid=@paid";

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
            command.CommandText = "DELETE FROM dienst WHERE paid=@paid";
            
            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command.CommandText = "DELETE FROM persoenlicherassistent WHERE paid=@paid";
            
            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command.CommandText = "DELETE FROM person WHERE personid=@paid";

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

        }

    }
}