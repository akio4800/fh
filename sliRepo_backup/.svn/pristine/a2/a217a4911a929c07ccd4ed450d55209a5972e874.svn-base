using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;
using Npgsql;
using SelvesSoftware.BusinessLogic;
using System.Windows;

namespace SelvesSoftware.DB
{
    public class PurchaserDataDAO : IPurchaserDataDAO
    {

        /// <summary>
        /// Auswahl der Auftraggeberdaten für einen bestimmten Auftraggeber in einem bestimmten Monat
        /// </summary>
        /// <param name="pur"></param>
        /// <returns></returns>
        /// 
        public PurchaserData Select(PurchaserData purData)
        {

            NpgsqlConnection con = DB.DBConnector.GetConnection();
            Purchaser pur = new Purchaser();
            pur.Id = purData.Purchaser.Id;
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select * from (((((Person p inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left outer join Adresse adresse on " +
                "(p.adressId = adresse.Adressid)) left Outer Join Person kontakt on (ag.kontaktperson = kontakt.personid) ) left outer join Adresse " +
                "kontaktad on (kontakt.adressid = kontaktad.adressid))) left outer Join Auftraggeberdaten agDaten on (ag.agid = agDaten.agid)" +
                " where ag.agid =@id and agDaten.agDatenid IN((select max(agDatenid) from Auftraggeberdaten group by agid))";

            DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);

            //using the Connection to get Datas

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
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
                        pur.HomeAdress.AdressId = reader.GetInt32(22);
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
                        pur.ContactPerson.Id = reader.GetInt32(31);
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

                        if (!(reader.IsDBNull(43)))
                        {
                            pur.ContactPerson.InfoField = reader.GetString(43);
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
                    purData.Purchaser = pur;
                    if (!(reader.IsDBNull(53)))
                    {
                        purData.TravellingAllowanceKM = reader.GetDecimal(53);
                    }
                    if (!(reader.IsDBNull(54)))
                    {
                        purData.HourlyRatePayoff = reader.GetDecimal(54);
                    }
                    if (!(reader.IsDBNull(55)))
                    {
                        purData.HourlyRate = reader.GetDecimal(55);
                    }
                    if (!(reader.IsDBNull(56)))
                    {
                        purData.InputIncome = reader.GetDecimal(56);
                    }
                    if (!(reader.IsDBNull(57)))
                    {
                        purData.Month = Convert.ToInt32(reader.GetDecimal(57));
                    }
                    if (!(reader.IsDBNull(58)))
                    {
                        purData.Year = Convert.ToInt32(reader.GetDecimal(58));
                    }
                    //59 = agid
                    if (!(reader.IsDBNull(60)))
                    {
                        purData.TravellingAllowance = reader.GetDecimal(60);
                    }
                    if (!(reader.IsDBNull(61)))
                    {
                        purData.AssistenceDemand = reader.GetInt32(61);
                    }
                    if (!(reader.IsDBNull(62)))
                    {
                        purData.CareAllowance = reader.GetInt32(62);
                    }
                    if (!(reader.IsDBNull(63)))
                    {
                        purData.Income = reader.GetDecimal(63);
                    }
                    //64 = agdatenid
                }

                purData.Purchaser.Employees = new List<EmploymentStatus>();
                reader.Close();
                command.CommandText = "select * from dienstverhaeltnis dienst inner join person p on (dienst.paid=p.personid) where dienst.agid=@id";
                DB.DBConnector.AddToCommand("@id", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader.IsDBNull(1)))
                    {
                        EmploymentStatus emp = new EmploymentStatus();
                        emp.Purchaser = purData.Purchaser;
                         emp.Assistant = new PersonalAssistant();
                        emp.Assistant.Id = reader.GetInt32(1);
                        if (!(reader.IsDBNull(5)))
                        {
                            emp.Assistant.FirstName = reader.GetString(5);
                        }
                        if (!(reader.IsDBNull(6)))
                        {
                            emp.Assistant.LastName = reader.GetString(6);
                        }
                        purData.Purchaser.Employees.Add(emp);
                    }
                    
                }
            }


            catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
            con.Close();

            return purData;
        }

        /// <summary>
        /// inserts PurchaserData AND the Purchaser
        /// </summary>
        /// <param name="purData"></param>
        /// <returns></returns>

        public PurchaserData Insert(PurchaserData purData)
        {
            PurchaserDAO pdao = new PurchaserDAO();
            pdao.Insert(purData.Purchaser);

            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO auftraggeberdaten (agid,monat,jahr, stundensatzauszahlung, " +
            "Stundensatz,BeitragEinkommen,FahrtkostenZusatz,BetreuungsbedarfH, EinkommenMonat, PflegegeldStufe, " +
            "FahrtkostenzusatzKM, agdatenid) VALUES (@agid,@month, @year, @stundensatzauszahlung, @Stundensatz, " +
            "@BeitragEinkommen,@FahrtkostenZusatz,@BetreuungsbedarfH, @EinkommenMonat, @PflegegeldStufe, @FahrtkostenzusatzKM,nextval('AGDatenIDGen') )";


            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Purchaser.Id);
            DB.DBConnector.AddToCommand("@month", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Month);
            DB.DBConnector.AddToCommand("@year", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Year);
            DB.DBConnector.AddToCommand("@stundensatzauszahlung", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRatePayoff);
            DB.DBConnector.AddToCommand("@Stundensatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRate);
            DB.DBConnector.AddToCommand("@BeitragEinkommen", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.InputIncome);
            DB.DBConnector.AddToCommand("@FahrtkostenZusatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowance);
            DB.DBConnector.AddToCommand("@BetreuungsbedarfH", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.AssistenceDemand);
            DB.DBConnector.AddToCommand("@EinkommenMonat", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Income);
            DB.DBConnector.AddToCommand("@PflegegeldStufe", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.CareAllowance);
            DB.DBConnector.AddToCommand("@FahrtkostenzusatzKM", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowanceKM);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();

            return purData;
        }


        public List<PurchaserData> SelectAll()
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select ag.agid from auftraggeber ag inner join dienstverhaeltnis d on(ag.agid = d.agid) group by ag.agid having count(ag.agid) > 0";

            List<PurchaserData> pds = new List<PurchaserData>();
            //using the Connection to get Datas

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                List<int> employedPaIds = new List<int>();
                while (reader.Read())
                {
                    if (!(reader.IsDBNull(0)))
                    {
                        employedPaIds.Add(reader.GetInt32(0));
                    }
                }

                command.CommandText = "select * from (((((Person p inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left Outer join Adresse adresse on " +
                    "(adresse.Adressid=p.adressId)) left Outer Join Person kontakt on (kontakt.personid=ag.kontaktperson) ) left outer join Adresse " +
                    "kontaktad on (kontakt.adressid=kontaktad.adressid))) Left outer Join Auftraggeberdaten agDaten on ( agDaten.agid=ag.agid) where agDaten.agDatenid " +
                    "IN ((select max(agDatenid) from Auftraggeberdaten group by agid))";
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Purchaser pur = new Purchaser();
                    PurchaserData purData = new PurchaserData();
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
                        pur.ContactPerson.Id = reader.GetInt32(31);
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
                        if (!(reader.IsDBNull(43)))
                        {
                            pur.ContactPerson.InfoField = reader.GetString(43);
                        }
                        int i = 44;
                        if (!(reader.IsDBNull(i++)))
                        {
                            pur.ContactPerson.HomeAdress = new Adress();
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.Street = reader.GetString(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.City = reader.GetString(i++);
                            }

                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.Country = reader.GetString(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.HouseNumber = reader.GetInt32(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.StairNumber = reader.GetInt32(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.Etage = reader.GetInt32(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.ZipCode = reader.GetInt32(i++);
                            }
                            if (!(reader.IsDBNull(i)))
                            {
                                pur.ContactPerson.HomeAdress.DoorNumber = reader.GetInt32(i++);
                            }
                        }
                    }

                    purData.Purchaser = pur;
                    if (!(reader.IsDBNull(53)))
                    {
                        purData.TravellingAllowanceKM = reader.GetDecimal(53);
                    }
                    if (!(reader.IsDBNull(54)))
                    {
                        purData.HourlyRatePayoff = reader.GetDecimal(54);
                    }
                    if (!(reader.IsDBNull(55)))
                    {
                        purData.HourlyRate = reader.GetDecimal(55);
                    }
                    if (!(reader.IsDBNull(56)))
                    {
                        purData.InputIncome = reader.GetDecimal(56);
                    }
                    if (!(reader.IsDBNull(57)))
                    {
                        purData.Month = Convert.ToInt32(reader.GetDecimal(57));
                    }
                    if (!(reader.IsDBNull(58)))
                    {
                        purData.Year = Convert.ToInt32(reader.GetDecimal(58));
                    }
                    //59 = agid
                    if (!(reader.IsDBNull(60)))
                    {
                        purData.TravellingAllowance = reader.GetDecimal(60);
                    }
                    if (!(reader.IsDBNull(61)))
                    {
                        purData.AssistenceDemand = reader.GetInt32(61);
                    }
                    if (!(reader.IsDBNull(62)))
                    {
                        purData.CareAllowance = reader.GetInt32(62);
                    }
                    if (!(reader.IsDBNull(63)))
                    {
                        purData.Income = reader.GetDecimal(63);
                    }
                    //64 = agdatenid

                    purData.Purchaser.Employees = new List<EmploymentStatus>();
                    if (employedPaIds.Contains(Convert.ToInt32(purData.Purchaser.Id)))
                    {
                        purData.Purchaser.Employees.Add(new EmploymentStatus());
                    }
                    pds.Add(purData);
                }
                reader.Close();
            }
            catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }


            con.Close();
            return pds;
        }
       
        public bool Update(PurchaserData purData)
        {
            PersonDAO pDao = new PersonDAO();
            if (purData.Purchaser.ContactPerson!=null&&purData.Purchaser.ContactPerson.Id == 0)
            {
                pDao.Insert(purData.Purchaser.ContactPerson);
            }
            else if(purData.Purchaser.ContactPerson != null)
            {
                purData.Purchaser.ContactPerson=pDao.Update(purData.Purchaser.ContactPerson);
            }
            PurchaserDAO purDAO = new PurchaserDAO();
            purDAO.Update(purData.Purchaser);

            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE auftraggeberdaten SET stundensatzauszahlung=@stundensatzauszahlung, " +
            "Stundensatz=@Stundensatz,BeitragEinkommen=@BeitragEinkommen,FahrtkostenZusatz=@FahrtkostenZusatz,BetreuungsbedarfH=@BetreuungsbedarfH, EinkommenMonat=@EinkommenMonat, PflegegeldStufe=@PflegegeldStufe, " +
            "FahrtkostenzusatzKM=@FahrtkostenzusatzKM WHERE agid=@agid AND monat=@month AND jahr=@year";


            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Purchaser.Id);
            DB.DBConnector.AddToCommand("@month", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Month);
            DB.DBConnector.AddToCommand("@year", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Year);
            DB.DBConnector.AddToCommand("@stundensatzauszahlung", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRatePayoff);
            DB.DBConnector.AddToCommand("@Stundensatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRate);
            DB.DBConnector.AddToCommand("@BeitragEinkommen", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.InputIncome);
            DB.DBConnector.AddToCommand("@FahrtkostenZusatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowance);
            DB.DBConnector.AddToCommand("@BetreuungsbedarfH", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.AssistenceDemand);
            DB.DBConnector.AddToCommand("@EinkommenMonat", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Income);
            DB.DBConnector.AddToCommand("@PflegegeldStufe", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.CareAllowance);
            DB.DBConnector.AddToCommand("@FahrtkostenzusatzKM", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowanceKM);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
            return true;
        }

        public void changeGlobals(decimal stundensatz, decimal stundensatzAusz, decimal fahrtkostenzusatzKM)
        {
            //TODO new entry for current month with new globals
            throw new NotImplementedException();
        }

        public void UpdateGlobals()
        {
            throw new NotImplementedException();
        }
        //for testing
        public void DeletePurchaserDataRecursive(long id)
        {
            Npgsql.NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM dienstverhaeltnis WHERE agid=@agid";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM auftraggeberdaten WHERE agid=@agid";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command.CommandText = "DELETE FROM auftraggeber WHERE agid=@agid";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            command.CommandText = "DELETE FROM person WHERE personid=@agid";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, id);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
        }
        /// <summary>
        /// inserts PurchaserData WITHOUT the according purchaser
        /// </summary>
        /// <param name="purData"></param>
        /// <returns></returns>
        public PurchaserData InsertPurData(PurchaserData purData)
        {
            PurchaserDAO pdao = new PurchaserDAO();
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO auftraggeberdaten (agid,monat,jahr, stundensatzauszahlung, " +
            "Stundensatz,BeitragEinkommen,FahrtkostenZusatz,BetreuungsbedarfH, EinkommenMonat, PflegegeldStufe, " +
            "FahrtkostenzusatzKM, agdatenid) VALUES (@agid,@month, @year, @stundensatzauszahlung, @Stundensatz, " +
            "@BeitragEinkommen,@FahrtkostenZusatz,@BetreuungsbedarfH, @EinkommenMonat, @PflegegeldStufe, @FahrtkostenzusatzKM,nextval('AGDatenIDGen') )";


            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Purchaser.Id);
            DB.DBConnector.AddToCommand("@month", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Month);
            DB.DBConnector.AddToCommand("@year", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Year);
            DB.DBConnector.AddToCommand("@stundensatzauszahlung", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRatePayoff);
            DB.DBConnector.AddToCommand("@Stundensatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRate);
            DB.DBConnector.AddToCommand("@BeitragEinkommen", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.InputIncome);
            DB.DBConnector.AddToCommand("@FahrtkostenZusatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowance);
            DB.DBConnector.AddToCommand("@BetreuungsbedarfH", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.AssistenceDemand);
            DB.DBConnector.AddToCommand("@EinkommenMonat", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Income);
            DB.DBConnector.AddToCommand("@PflegegeldStufe", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.CareAllowance);
            DB.DBConnector.AddToCommand("@FahrtkostenzusatzKM", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowanceKM);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();

            return purData;
        }

        public bool UpdatePurData(PurchaserData purData)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE auftraggeberdaten SET stundensatzauszahlung=@stundensatzauszahlung, " +
            "Stundensatz=@Stundensatz,BeitragEinkommen=@BeitragEinkommen,FahrtkostenZusatz=@FahrtkostenZusatz,BetreuungsbedarfH=@BetreuungsbedarfH, EinkommenMonat=@EinkommenMonat, PflegegeldStufe=@PflegegeldStufe, " +
            "FahrtkostenzusatzKM=@FahrtkostenzusatzKM, agdatenid=nextval('AGDatenIDGen') WHERE agid=@agid AND monat=@month AND jahr=@year";


            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Purchaser.Id);
            DB.DBConnector.AddToCommand("@month", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Month);
            DB.DBConnector.AddToCommand("@year", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Year);
            DB.DBConnector.AddToCommand("@stundensatzauszahlung", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRatePayoff);
            DB.DBConnector.AddToCommand("@Stundensatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.HourlyRate);
            DB.DBConnector.AddToCommand("@BeitragEinkommen", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.InputIncome);
            DB.DBConnector.AddToCommand("@FahrtkostenZusatz", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowance);
            DB.DBConnector.AddToCommand("@BetreuungsbedarfH", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.AssistenceDemand);
            DB.DBConnector.AddToCommand("@EinkommenMonat", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.Income);
            DB.DBConnector.AddToCommand("@PflegegeldStufe", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.CareAllowance);
            DB.DBConnector.AddToCommand("@FahrtkostenzusatzKM", NpgsqlTypes.NpgsqlDbType.Numeric, command, purData.TravellingAllowanceKM);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
            return true;
        }
    }
}
