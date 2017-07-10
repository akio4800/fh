using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;
using SelvesSoftware.DataContainer;




namespace SelvesSoftware.DB
{
    public class PurchaserDAO : IPurchaserDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pur">Leerer Purchaser DataCollector, der nur die ID des zu suchenden enthält</param>
        /// <returns>Gesuchten Auftraggeber</returns>
        public Purchaser Select(Purchaser pur)
        {

            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select * from (((Person p Inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Inner join Adresse adresse on " +
                "(adresse.Adressid=p.adressId)) left Outer Join Person kontakt on (kontakt.personid=ag.kontaktperson) ) left outer join Adresse " +
                "kontaktad on (kontakt.adressid=kontaktad.adressid) where ag.agid=@id";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);

            //using the Connection to get Datas
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!(reader.IsDBNull(0)))
                {
                    pur.Id = reader.GetInt32(0);
                }
                if (!(reader.IsDBNull(1)))
                {
                    pur.EMail = reader.GetString(1);
                }
                if (!(reader.IsDBNull(2)))
                {
                    pur.FirstName = reader.GetString(2);
                }
                if (!(reader.IsDBNull(3)))
                {
                    pur.LastName = reader.GetString(3);
                }
                if (!(reader.IsDBNull(4)))
                {
                    pur.PhoneNumber = reader.GetString(4);
                }
                if (!(reader.IsDBNull(6)))
                {
                    pur.MobilePhone = reader.GetString(6);
                }
                if (!(reader.IsDBNull(7)))
                {
                    pur.IBAN = reader.GetString(7);
                }
                if (!(reader.IsDBNull(8)))
                {
                    pur.BIC = reader.GetString(8);
                }
                if (!(reader.IsDBNull(9)))
                {
                    pur.AccountHolder = reader.GetString(9);
                }
                if (!(reader.IsDBNull(10)))
                {
                    pur.SVN = Convert.ToInt64(reader.GetDecimal(10));
                }
                if (!(reader.IsDBNull(11)))
                {
                    pur.nationality = reader.GetString(11);
                }
                if (!(reader.IsDBNull(12)))
                {
                    pur.InfoField = reader.GetString(12);
                }
                if (!(reader.IsDBNull(13)))
                {
                    pur.Active = reader.GetBoolean(13);
                }
                if (!(reader.IsDBNull(14)))
                {
                    pur.ApprovalBegin = reader.GetDateTime(14);
                }
                if (!(reader.IsDBNull(15)))
                {
                    pur.ApprovalEnd = reader.GetDateTime(15);
                }
                if (!(reader.IsDBNull(16)))
                {
                    pur.EntryDate = reader.GetDateTime(16);
                } // 17 = AGid, 18 = kontaktperson id
                if (!(reader.IsDBNull(19)))
                {
                    pur.hasIntroCourse = reader.GetBoolean(19);
                }
                if (!(reader.IsDBNull(20)))
                {
                    pur.hasContract = reader.GetBoolean(20);
                }
                if (!(reader.IsDBNull(21)))
                {
                    pur.DistrictCommision = reader.GetString(21);
                }
                if (!(reader.IsDBNull(22)))
                {
                    pur.HomeAdress = new Adress();
                    if (!(reader.IsDBNull(23)))
                    {
                        pur.HomeAdress.Street = reader.GetString(23);
                    }
                    if (!(reader.IsDBNull(24)))
                    {
                        pur.HomeAdress.City = reader.GetString(24);
                    }
                    if (!(reader.IsDBNull(25)))
                    {
                        pur.HomeAdress.Country = reader.GetString(25);
                    }
                    if (!(reader.IsDBNull(26)))
                    {
                        pur.HomeAdress.HouseNumber = reader.GetInt32(26);
                    }
                    if (!(reader.IsDBNull(27)))
                    {
                        pur.HomeAdress.StairNumber = reader.GetInt32(27);
                    }
                    if (!(reader.IsDBNull(28)))
                    {
                        pur.HomeAdress.Etage = reader.GetInt32(28);
                    }
                    if (!(reader.IsDBNull(29)))
                    {
                        pur.HomeAdress.ZipCode = reader.GetInt32(29);
                    }
                    if (!(reader.IsDBNull(30)))
                    {
                        pur.HomeAdress.DoorNumber = reader.GetInt32(30);
                    }
                }
                //kontaktperson
                if (!(reader.IsDBNull(31)))
                {
                    pur.ContactPerson = new Person();
                    if (!(reader.IsDBNull(32)))
                    {
                        pur.ContactPerson.EMail = reader.GetString(32);
                    }
                    if (!(reader.IsDBNull(33)))
                    {
                        pur.ContactPerson.FirstName = reader.GetString(33);
                    }
                    if (!(reader.IsDBNull(34)))
                    {
                        pur.ContactPerson.LastName = reader.GetString(34);
                    }
                    if (!(reader.IsDBNull(35)))
                    {
                        pur.ContactPerson.PhoneNumber = reader.GetString(35);
                    }
                    if (!(reader.IsDBNull(37)))
                    {
                        pur.ContactPerson.MobilePhone = reader.GetString(37);
                    }


                    if (!(reader.IsDBNull(44)))
                    {
                        pur.ContactPerson.HomeAdress = new Adress();
                        if (!(reader.IsDBNull(45)))
                        {
                            pur.ContactPerson.HomeAdress.Street = reader.GetString(45);
                        }
                        if (!(reader.IsDBNull(46)))
                        {
                            pur.ContactPerson.HomeAdress.City = reader.GetString(46);
                        }
                        if (!(reader.IsDBNull(47)))
                        {
                            pur.ContactPerson.HomeAdress.Country = reader.GetString(47);
                        }
                        if (!(reader.IsDBNull(48)))
                        {
                            pur.ContactPerson.HomeAdress.HouseNumber = reader.GetInt32(48);
                        }
                        if (!(reader.IsDBNull(49)))
                        {
                            pur.ContactPerson.HomeAdress.StairNumber = reader.GetInt32(49);
                        }
                        if (!(reader.IsDBNull(50)))
                        {
                            pur.ContactPerson.HomeAdress.Etage = reader.GetInt32(50);
                        }
                        if (!(reader.IsDBNull(51)))
                        {
                            pur.ContactPerson.HomeAdress.ZipCode = reader.GetInt32(51);
                        }
                        if (!(reader.IsDBNull(52)))
                        {
                            pur.ContactPerson.HomeAdress.DoorNumber = reader.GetInt32(52);
                        }
                    }
                }
            }
            reader.Close();
            con.Close();

            pur.Employees = SelectEmploymentStatusList(pur);


            return pur;
        }
        /// <summary>
        /// Alle in der Datenbank befindlichen Auftraggeber werden gesucht
        /// </summary>
        /// <param name="paList"></param>
        /// <returns></returns>
        public List<Purchaser> SelectAll()
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            List<Purchaser> purs = new List<Purchaser>();
            try
            {

                NpgsqlCommand command = new NpgsqlCommand(null, con);
                command.CommandText = "select * from (((Person p Inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Inner join Adresse adresse on " +
                    "(adresse.Adressid=p.adressId)) left Outer Join Person kontakt on (kontakt.personid=ag.kontaktperson) ) left outer join Adresse " +
                    "kontaktad on (kontakt.adressid=kontaktad.adressid)";
                Purchaser pur = new Purchaser();

                //using the Connection to get Datas
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                    {
                        pur.Id = reader.GetInt32(0);
                    }
                    if (!(reader.IsDBNull(1)))
                    {
                        pur.EMail = reader.GetString(1);
                    }
                    if (!(reader.IsDBNull(2)))
                    {
                        pur.FirstName = reader.GetString(2);
                    }
                    if (!(reader.IsDBNull(3)))
                    {
                        pur.LastName = reader.GetString(3);
                    }
                    if (!(reader.IsDBNull(4)))
                    {
                        pur.PhoneNumber = reader.GetString(4);
                    }
                    if (!(reader.IsDBNull(6)))
                    {
                        pur.MobilePhone = reader.GetString(6);
                    }
                    if (!(reader.IsDBNull(7)))
                    {
                        pur.IBAN = reader.GetString(7);
                    }
                    if (!(reader.IsDBNull(8)))
                    {
                        pur.BIC = reader.GetString(8);
                    }
                    if (!(reader.IsDBNull(9)))
                    {
                        pur.AccountHolder = reader.GetString(9);
                    }
                    if (!(reader.IsDBNull(10)))
                    {
                        pur.SVN = Convert.ToInt64(reader.GetDecimal(10));
                    }
                    if (!(reader.IsDBNull(11)))
                    {
                        pur.nationality = reader.GetString(11);
                    }
                    if (!(reader.IsDBNull(12)))
                    {
                        pur.InfoField = reader.GetString(12);
                    }
                    if (!(reader.IsDBNull(13)))
                    {
                        pur.Active = reader.GetBoolean(13);
                    }
                    if (!(reader.IsDBNull(14)))
                    {
                        pur.ApprovalBegin = reader.GetDateTime(14);
                    }
                    if (!(reader.IsDBNull(15)))
                    {
                        pur.ApprovalEnd = reader.GetDateTime(15);
                    }
                    if (!(reader.IsDBNull(16)))
                    {
                        pur.EntryDate = reader.GetDateTime(16);
                    } // 17 = AGid, 18 = kontaktperson id
                    if (!(reader.IsDBNull(19)))
                    {
                        pur.hasIntroCourse = reader.GetBoolean(19);
                    }
                    if (!(reader.IsDBNull(20)))
                    {
                        pur.hasContract = reader.GetBoolean(20);
                    }
                    if (!(reader.IsDBNull(21)))
                    {
                        pur.DistrictCommision = reader.GetString(21);
                    }
                    if (!(reader.IsDBNull(22)))
                    {
                        pur.HomeAdress = new Adress();
                        if (!(reader.IsDBNull(23)))
                        {
                            pur.HomeAdress.Street = reader.GetString(23);
                        }
                        if (!(reader.IsDBNull(24)))
                        {
                            pur.HomeAdress.City = reader.GetString(24);
                        }
                        if (!(reader.IsDBNull(25)))
                        {
                            pur.HomeAdress.Country = reader.GetString(25);
                        }
                        if (!(reader.IsDBNull(26)))
                        {
                            pur.HomeAdress.HouseNumber = reader.GetInt32(26);
                        }
                        if (!(reader.IsDBNull(27)))
                        {
                            pur.HomeAdress.StairNumber = reader.GetInt32(27);
                        }
                        if (!(reader.IsDBNull(28)))
                        {
                            pur.HomeAdress.Etage = reader.GetInt32(28);
                        }
                        if (!(reader.IsDBNull(29)))
                        {
                            pur.HomeAdress.ZipCode = reader.GetInt32(29);
                        }
                        if (!(reader.IsDBNull(30)))
                        {
                            pur.HomeAdress.DoorNumber = reader.GetInt32(30);
                        }
                    }
                    //kontaktperson
                    if (!(reader.IsDBNull(31)))
                    {
                        if (!(reader.IsDBNull(32)))
                        {
                            pur.ContactPerson.EMail = reader.GetString(32);
                        }
                        if (!(reader.IsDBNull(33)))
                        {
                            pur.ContactPerson.FirstName = reader.GetString(33);
                        }
                        if (!(reader.IsDBNull(34)))
                        {
                            pur.ContactPerson.LastName = reader.GetString(34);
                        }
                        if (!(reader.IsDBNull(35)))
                        {
                            pur.ContactPerson.PhoneNumber = reader.GetString(35);
                        }
                        if (!(reader.IsDBNull(37)))
                        {
                            pur.ContactPerson.MobilePhone = reader.GetString(37);
                        }


                        if (!(reader.IsDBNull(44)))
                        {
                            pur.ContactPerson.HomeAdress = new Adress();
                            if (!(reader.IsDBNull(45)))
                            {
                                pur.ContactPerson.HomeAdress.Street = reader.GetString(45);
                            }
                            if (!(reader.IsDBNull(46)))
                            {
                                pur.ContactPerson.HomeAdress.City = reader.GetString(46);
                            }
                            if (!(reader.IsDBNull(47)))
                            {
                                pur.ContactPerson.HomeAdress.Country = reader.GetString(47);
                            }
                            if (!(reader.IsDBNull(48)))
                            {
                                pur.ContactPerson.HomeAdress.HouseNumber = reader.GetInt32(48);
                            }
                            if (!(reader.IsDBNull(49)))
                            {
                                pur.ContactPerson.HomeAdress.StairNumber = reader.GetInt32(49);
                            }
                            if (!(reader.IsDBNull(50)))
                            {
                                pur.ContactPerson.HomeAdress.Etage = reader.GetInt32(50);
                            }
                            if (!(reader.IsDBNull(51)))
                            {
                                pur.ContactPerson.HomeAdress.ZipCode = reader.GetInt32(51);
                            }
                            if (!(reader.IsDBNull(52)))
                            {
                                pur.ContactPerson.HomeAdress.DoorNumber = reader.GetInt32(52);
                            }
                        }
                    }
                    purs.Add(pur);
                }

                con.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
            return purs;
        }

        public Purchaser Insert(Purchaser person)
        {
            //Person selbst hinzufügen
            PersonDAO personDao = new PersonDAO();
            Person pa = personDao.Insert(person); //returniert neu eingefügte person mit neuer id

            String insertText = "";
            if (person.ContactPerson != null)
            {
                person.ContactPerson = (new PersonDAO()).Insert(person.ContactPerson);
                insertText = "INSERT INTO auftraggeber (agid,aktiv,bewilligungsanfang, bewilligungsende, " +
            "eintrittsdatum, kontaktperson,hateinfuehrungskurs,hatkontrakt,bezirkshauptmannschaft) VALUES (@agid,@aktiv,@bewilligungsanfang, @bewilligungsende, @eintrittsdatum, @kontaktperson,@hateinfuehrungskurs,@hatkontrakt,@bezirkshauptmannschaft)";
            }
            else
            {
                insertText = "INSERT INTO auftraggeber (agid,aktiv,bewilligungsanfang, bewilligungsende, " +
            "eintrittsdatum,hateinfuehrungskurs,hatkontrakt,bezirkshauptmannschaft) VALUES (@agid,@aktiv,@bewilligungsanfang, @bewilligungsende, @eintrittsdatum,@hateinfuehrungskurs,@hatkontrakt,@bezirkshauptmannschaft)";
            }

            NpgsqlConnection con = DB.DBConnector.GetConnection();

             
            NpgsqlCommand command = new NpgsqlCommand(null, con);

            command.CommandText = insertText;

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.Id);
            DB.DBConnector.AddToCommand("@aktiv", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Active);
            DB.DBConnector.AddToCommand("@bewilligungsanfang", NpgsqlTypes.NpgsqlDbType.Date, command, person.ApprovalBegin);
            DB.DBConnector.AddToCommand("@bewilligungsende", NpgsqlTypes.NpgsqlDbType.Date, command, person.ApprovalEnd);
            DB.DBConnector.AddToCommand("@eintrittsdatum", NpgsqlTypes.NpgsqlDbType.Date, command, person.EntryDate);
            DB.DBConnector.AddToCommand("@hateinfuehrungskurs", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.hasIntroCourse);
            DB.DBConnector.AddToCommand("@hatkontrakt", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.hasContract);
            if (person.ContactPerson != null)
            {
                DB.DBConnector.AddToCommand("@kontaktperson", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.ContactPerson.Id);
            }
            DB.DBConnector.AddToCommand("@bezirkshauptmannschaft", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.DistrictCommision);
            try {int num = command.ExecuteNonQuery();
                Console.WriteLine(num);
            } catch(Exception e){MessageBox.Show(e.Message.ToString(),"Error");}
            con.Close();
            
            
            return person;

        }

        public bool Update(Purchaser person)
        {
            PersonDAO pdao = new PersonDAO();
            pdao.Update(person);

            NpgsqlConnection con = DB.DBConnector.GetConnection();

             
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE auftraggeber SET aktiv=@aktiv,bewilligungsanfang=@bewilligungsanfang, bewilligungsende=@bewilligungsende, eintrittsdatum=@eintrittsdatum, kontaktperson=@kontaktperson ,hateinfuehrungskurs=@hateinfuehrungskurs,hatkontrakt=@hatkontrakt, bezirkshauptmannschaft=@bezirkshauptmannschaft WHERE agid=@agid";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.Id);
            DB.DBConnector.AddToCommand("@aktiv", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.Active);
            DB.DBConnector.AddToCommand("@bewilligungsanfang", NpgsqlTypes.NpgsqlDbType.Date, command, person.ApprovalBegin);
            DB.DBConnector.AddToCommand("@bewilligungsende", NpgsqlTypes.NpgsqlDbType.Date, command, person.ApprovalEnd);
            DB.DBConnector.AddToCommand("@eintrittsdatum", NpgsqlTypes.NpgsqlDbType.Date, command, person.EntryDate);
            DB.DBConnector.AddToCommand("@hateinfuehrungskurs", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.hasIntroCourse);
            DB.DBConnector.AddToCommand("@hatkontrakt", NpgsqlTypes.NpgsqlDbType.Boolean, command, person.hasContract);
            if (person.ContactPerson != null)
            {
                DB.DBConnector.AddToCommand("@kontaktperson", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.ContactPerson.Id);
            }
            else
            {
                DB.DBConnector.AddToCommand("@kontaktperson", NpgsqlTypes.NpgsqlDbType.Numeric, command, null);
            }
            DB.DBConnector.AddToCommand("@bezirkshauptmannschaft", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.DistrictCommision);
            try {command.ExecuteNonQuery();}catch(Exception e){MessageBox.Show(e.Message.ToString(),"Error");}

            con.Close();
            return true;
        }

        public List<EmploymentStatus> SelectEmploymentStatusList(Purchaser pur)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();

             

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Dienstverhaeltnis p WHERE p.agid=@id";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Integer, command, pur.Id);

            NpgsqlDataReader reader = command.ExecuteReader();
            List<int> paIDs = new List<int>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(1))
                {
                    paIDs.Add(reader.GetInt32(1));
                }
            }
            reader.Close();
            con.Close();

            PersonalAssistantDAO paDAO = new PersonalAssistantDAO();
            pur.Employees = new List<EmploymentStatus>();
            foreach (int i in paIDs)
            {
                PersonalAssistant pa = new PersonalAssistant();
                pa.Id = i;
                pa = paDAO.selectReduced(pa);
                EmploymentStatus empStatus = new EmploymentStatus();
                empStatus.Assistant = pa;
                empStatus.Purchaser = pur;
                pur.Employees.Add(empStatus);
            }

            return pur.Employees;
        }

        public void InsertContactPerson(Purchaser pur, Person p)
        {
            PersonDAO personDAO = new PersonDAO();
            p=personDAO.Insert(p);
            pur.ContactPerson = p;
            Update(pur);
        }

        public void UpdateContactPerson(Person p)
        {
            PersonDAO personDAO = new PersonDAO();
            p=personDAO.Update(p);
        }
        

 
    }
}
