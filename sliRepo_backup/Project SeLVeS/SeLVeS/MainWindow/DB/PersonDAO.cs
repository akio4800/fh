using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;
using System.Windows;

namespace SelvesSoftware.DB
{
    /// <summary>
    /// author: TS
    /// </summary>
    public class PersonDAO : IPersonDAO
    {
        public AdressDAO AdressDao = new AdressDAO();
        /// <summary>
        /// select person by id
        /// (Make new Person, set id of Person, use in Parameter, get full Person as return)
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person Select(Person person)
        {

            //TODO EXCEPTION PERSON NOT FOUND

            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Person p left outer join Adresse ad on (p.adressid=ad.adressid) WHERE p.personid=@id";

            //adding Id as Parameter to the command
            NpgsqlParameter id = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Numeric);
            id.Value = person.Id;
            command.Parameters.Add(id);

            //using the Connection to get Datas
            NpgsqlDataReader reader = command.ExecuteReader();

            //creating a new Data Container and filling it
            FillPerson(person, reader);

            reader.Close();
            con.Close();



            return person;

        }


        public Person Insert(Person person)
        {
            //Because of Foreignt key the Adress is first written into the Database
            AdressDao.Insert(person.HomeAdress);

            NpgsqlConnection con = DB.DBConnector.GetConnection();

            //Template Insert Statement as Command
            NpgsqlCommand command = new NpgsqlCommand(null, con);

            command.CommandText = "INSERT INTO person (personid,email,vorname,nachname,telnummer,mobiltelefonnummer,adressid,iban,bic,kontoinhaber,svn,staatszugehoerigkeit, info) VALUES (nextval('PersonIDGen'),@email,@vorname,@nachname,@telnummer,@mobiltelefonnummer,@adressid,@iban,@bic,@kontoinhaber,@svn,@staatszugehoerigkeit,@info)";

            //filling the Parameters in the command form the Datacontainer
            DB.DBConnector.AddToCommand("@email", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.EMail);
            DB.DBConnector.AddToCommand("@vorname", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.FirstName);
            DB.DBConnector.AddToCommand("@nachname", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.LastName);
            DB.DBConnector.AddToCommand("@telnummer", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.PhoneNumber);
            DB.DBConnector.AddToCommand("@mobiltelefonnummer", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.MobilePhone);
            DB.DBConnector.AddToCommand("@adressid", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.HomeAdress.AdressId);
            DB.DBConnector.AddToCommand("@iban", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.IBAN);
            DB.DBConnector.AddToCommand("@bic", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.BIC);
            DB.DBConnector.AddToCommand("@kontoinhaber", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.AccountHolder);
            DB.DBConnector.AddToCommand("@svn", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.SVN);
            DB.DBConnector.AddToCommand("@staatszugehoerigkeit", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.nationality);
            DB.DBConnector.AddToCommand("@Info", NpgsqlTypes.NpgsqlDbType.Text, command, person.InfoField);

            //Insert into Database
            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            //The Database creates the Id from a Sequence, the used id is needed for further use and written into the DataContainer
            command.CommandText = "Select currval('PersonIDGen')";
            NpgsqlDataReader reader = command.ExecuteReader();
            reader.Read();
            if (!(reader.IsDBNull(0)))
            {
                person.Id = reader.GetInt64(0);
            }


            reader.Close();
            con.Close();


            return person;
        }


        public Person Update(Person person)
        {
            AdressDao.Update(person.HomeAdress);
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE person SET email=@email, vorname=@vorname, nachname=@nachname, telnummer=@telnummer, mobiltelefonnummer=@mobiltelefonnummer, iban=@iban,bic=@bic,kontoinhaber=@kontoinhaber,svn=@svn,staatszugehoerigkeit=@staatszugehoerigkeit, info=@Info WHERE personid=@personid";
            DB.DBConnector.AddToCommand("@personid", NpgsqlTypes.NpgsqlDbType.Integer, command, person.Id);
            DB.DBConnector.AddToCommand("@email", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.EMail);
            DB.DBConnector.AddToCommand("@vorname", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.FirstName);
            DB.DBConnector.AddToCommand("@nachname", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.LastName);
            DB.DBConnector.AddToCommand("@telnummer", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.PhoneNumber);
            DB.DBConnector.AddToCommand("@mobiltelefonnummer", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.MobilePhone);
            DB.DBConnector.AddToCommand("@iban", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.IBAN);
            DB.DBConnector.AddToCommand("@bic", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.BIC);
            DB.DBConnector.AddToCommand("@kontoinhaber", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.AccountHolder);
            DB.DBConnector.AddToCommand("@svn", NpgsqlTypes.NpgsqlDbType.Numeric, command, person.SVN);
            DB.DBConnector.AddToCommand("@staatszugehoerigkeit", NpgsqlTypes.NpgsqlDbType.Varchar, command, person.nationality);
            DB.DBConnector.AddToCommand("@Info", NpgsqlTypes.NpgsqlDbType.Text, command, person.InfoField);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); Console.WriteLine(e.StackTrace.ToString()); }

            con.Close();

            return person;
        }

        private void FillPerson(Person p, NpgsqlDataReader reader)
        {
            if (reader.HasRows)
            {
                //the Reader needs to 
                reader.Read();
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
            }

        }

    }
}
