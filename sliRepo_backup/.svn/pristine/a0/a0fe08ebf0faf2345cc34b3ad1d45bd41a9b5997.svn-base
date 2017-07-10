using Npgsql;
using SelvesSoftware.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SelvesSoftware.DB
{
    public class MonthlyBillingDAO : IMonthlyBillingDAO
    {
        public List<MonthlyBilling> selectAllMB()
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Monatsabrechnung ORDER BY agid,monat,jahr";

            NpgsqlDataReader reader = command.ExecuteReader();

            List<MonthlyBilling> mbs = new List<MonthlyBilling>();
            MonthlyBilling mb = new MonthlyBilling();
            mb.MbPerPaList = new List<MonthlyBillingPerPa>();
            MonthlyBillingPerPa currmba = null;

            while (reader.Read())
            {
                MonthlyBillingPerPa mbPerPA = new MonthlyBillingPerPa();

                if (!(reader.IsDBNull(0)))
                {
                    mbPerPA.PrivateKm = reader.GetInt32(0);
                }
                if (!(reader.IsDBNull(1)))
                {
                    mbPerPA.Month = Convert.ToInt32(reader.GetDecimal(1));
                    mb.Month = mbPerPA.Month;
                }
                if (!(reader.IsDBNull(2)))
                {
                    mbPerPA.Year = Convert.ToInt32(reader.GetDecimal(2));
                    mb.Year = mbPerPA.Year;
                }
                if (!(reader.IsDBNull(3)))
                {
                    mbPerPA.Pa = new PersonalAssistant(reader.GetInt32(3));
                }
                if (!(reader.IsDBNull(4)))
                {
                    mbPerPA.Pur = new PurchaserData();
                    mbPerPA.Pur.Purchaser = new Purchaser();
                    mbPerPA.Pur.Purchaser.Id = reader.GetInt32(4);
                    if (mb.Purchaser == null)
                    {
                        mb.Purchaser = new PurchaserData();
                        mb.Purchaser.Purchaser = new Purchaser();
                        mb.Purchaser.Purchaser.Id = mbPerPA.Pur.Purchaser.Id;
                    }
                }
                if (!(reader.IsDBNull(5)))
                {
                    mbPerPA.WorkingHours = reader.GetInt32(5);
                }
                if (!(reader.IsDBNull(6)))
                {
                    mbPerPA.BillableKm = reader.GetInt32(6);
                }

                if (currmba != null && currmba.Month == mbPerPA.Month && currmba.Year == mbPerPA.Year && currmba.Pur.Purchaser.Id == mbPerPA.Pur.Purchaser.Id)
                {
                }
                else if (currmba != null)
                {
                    mbs.Add(mb);
                    mb = new MonthlyBilling();
                    mb.MbPerPaList = new List<MonthlyBillingPerPa>();
                }
                else
                {
                    currmba = mbPerPA;
                }
                mb.MbPerPaList.Add(mbPerPA);
                currmba = mbPerPA;

            }
            reader.Close();
            con.Close();
            return mbs;

        }
        

       

        public MonthlyBillingPerPa selectMBEntry(MonthlyBillingPerPa mb)
        {           
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT * FROM Monatsabrechnung p WHERE agid=@agid AND paid=@paid "+
                                  "AND monat=@monat AND jahr=@jahr";

            //adding Parameter to the command
            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, mb.Pa.Id);
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, mb.Pur.Purchaser.Id);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, mb.Month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, mb.Year);

            //using the Connection to get Datas
            NpgsqlDataReader reader = command.ExecuteReader();

            //creating a new Data Container and filling it
            if (reader.Read())
            {
                if (!(reader.IsDBNull(0)))
                {
                    mb.PrivateKm = reader.GetInt32(0);
                }
                if (!(reader.IsDBNull(5)))
                {
                    mb.WorkingHours = reader.GetInt32(5);
                }
                if (!(reader.IsDBNull(6)))
                {
                    mb.BillableKm = reader.GetInt32(6);
                }
           
            }

            reader.Close();
            con.Close();

            EffortEntryDAO eeDao = new EffortEntryDAO();
            List<EffortEntry> entries = null;
            entries = eeDao.GetEntries((int)mb.Pa.Id, (int)mb.Pur.Purchaser.Id, mb.Month, mb.Year);
            mb.EffortList = entries;

            return mb;

        }

        public List<MonthlyBilling> SelectPeriod(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectSpecificMB(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectSpecificMB(Purchaser pur)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectAllMB(MonthlyBilling pur)
        {
            throw new NotImplementedException();
        }

        public MonthlyBilling InsertMonthlyBilling(MonthlyBilling mb)
        {
            //falsches Insert
            return null;
        }

        public MonthlyBillingPerPa InsertMonthlyBilling(MonthlyBillingPerPa mb)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "INSERT INTO monatsabrechnung (monat,jahr,agid,paid,anzahprivatlkm,stundenanzahl," +
                "anzahlkmabgerechnet) VALUES (@monat,@jahr,@agid,@paid,@anzahlprivatKm, " +
                "@stundenanzahl,@anzahlKmabgerechnet)";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Pa.Id);
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Pur.Purchaser.Id);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Year);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Month);
            DB.DBConnector.AddToCommand("@anzahlprivatKm", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.PrivateKm);
            DB.DBConnector.AddToCommand("@stundenanzahl", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.WorkingHours);
            DB.DBConnector.AddToCommand("@anzahlKmabgerechnet", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.BillableKm);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
            return null;
        }

        public bool UpdateMonthlyBillingEntry(MonthlyBillingPerPa mb)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();


            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "UPDATE Monatsabrechnung SET anzahprivatlkm=@privat,stundenanzahl=@stunde, " +
                "anzahlkmabgerechnet=@kmabger WHERE agid=@agid AND paid=@paid " +
                                  "AND monat=@monat AND jahr=@jahr";

            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Pa.Id);
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Pur.Purchaser.Id);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.Year);
            DB.DBConnector.AddToCommand("@privat", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.PrivateKm);
            DB.DBConnector.AddToCommand("@stunde", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.WorkingHours);
            DB.DBConnector.AddToCommand("@kmabger", NpgsqlTypes.NpgsqlDbType.Integer, command, mb.BillableKm);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

            con.Close();
            return true;
        }

        public bool UpdateMonthlyBilling(MonthlyBilling mb)
        {
            //falsches update
            throw new NotImplementedException();
        }


        //TODO : implement select for all MonthlyBilling and Effortentries from DB for 1 Purchaser ans Month
        public List<MonthlyBillingPerPa> SelectMBperPa(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }


        public List<MonthlyBilling> selectAllFrom(DateTime? nullable)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            if (nullable == null) { nullable = new DateTime(2000, 01, 01); };
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select * from monatsabrechnung where (monat >= @monat and jahr=@jahr) or jahr > @jahr ";

            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, nullable.Value.Month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, nullable.Value.Year);

            NpgsqlDataReader reader = command.ExecuteReader();

            List<MonthlyBilling> mbs = new List<MonthlyBilling>();
            MonthlyBilling mb = new MonthlyBilling();
            mb.MbPerPaList = new List<MonthlyBillingPerPa>();
            MonthlyBillingPerPa currmba = null;

            while (reader.Read())
            {
                MonthlyBillingPerPa mbPerPA = new MonthlyBillingPerPa();

                if (!(reader.IsDBNull(0)))
                {
                    mbPerPA.PrivateKm = reader.GetInt32(0);
                }
                if (!(reader.IsDBNull(1)))
                {
                    mbPerPA.Month = Convert.ToInt32(reader.GetDecimal(1));
                    mb.Month = mbPerPA.Month;
                }
                if (!(reader.IsDBNull(2)))
                {
                    mbPerPA.Year = Convert.ToInt32(reader.GetDecimal(2));
                    mb.Year = mbPerPA.Year;
                }
                if (!(reader.IsDBNull(3)))
                {
                    mbPerPA.Pa = new PersonalAssistant(reader.GetInt32(3));
                }
                if (!(reader.IsDBNull(4)))
                {
                    mbPerPA.Pur = new PurchaserData();
                    mbPerPA.Pur.Purchaser = new Purchaser();
                    mbPerPA.Pur.Purchaser.Id = reader.GetInt32(4);
                    if (mb.Purchaser == null)
                    {
                        mb.Purchaser = new PurchaserData();
                        mb.Purchaser.Purchaser = new Purchaser();
                        mb.Purchaser.Purchaser.Id = mbPerPA.Pur.Purchaser.Id;
                    }
                }
                if (!(reader.IsDBNull(5)))
                {
                    mbPerPA.WorkingHours = reader.GetInt32(5);
                }
                if (!(reader.IsDBNull(6)))
                {
                    mbPerPA.BillableKm = reader.GetInt32(6);
                }

                if (currmba != null && currmba.Month == mbPerPA.Month && currmba.Year == mbPerPA.Year && currmba.Pur.Purchaser.Id == mbPerPA.Pur.Purchaser.Id)
                {
                }
                else if (currmba != null)
                {
                    mbs.Add(mb);
                    mb = new MonthlyBilling();
                    mb.MbPerPaList = new List<MonthlyBillingPerPa>();
                }
                else
                {
                    currmba = mbPerPA;
                }
                mb.MbPerPaList.Add(mbPerPA);
                currmba = mbPerPA;

            }
            reader.Close();
            con.Close();
            return mbs;
        }

        //for testing
        public void DeleteMonthlyBillingEntry(long agid, long paid, int month, int year)
        {
            Npgsql.NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM monatsabrechnung WHERE agid=@agid AND paid=@paid AND monat=@monat AND jahr=@jahr";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, agid);
            DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, command, paid);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
        }



	public List<int> getMBYears(Purchaser pur)
        {
            List<int> years = new List<int>();
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select distinct jahr from monatsabrechnung where agid=@agid";

      
            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    years.Add(Convert.ToInt32(reader.GetDecimal(0)));
                }
            }
            return years;
        }
        public List<int> getMBMonths(Purchaser pur, int year)
        {
            List<int> months = new List<int>();
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select distinct monat from monatsabrechnung where agid=@agid and jahr=@jahr";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    months.Add(Convert.ToInt32(reader.GetDecimal(0)));
                }
            }
            return months;
        }

        public List<PersonalAssistant> getPAsFromMB(Purchaser pur, int year, int month)
        {
            List<PersonalAssistant> pas = new List<PersonalAssistant>();

            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select p.personid,p.vorname,p.nachname from Person p Inner Join Monatsabrechnung mb On (p.PersonId=mb.paid) where agid=@agid and jahr=@jahr and monat=@monat";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, pur.Id);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, month);

            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                PersonalAssistant pa = new PersonalAssistant();
                if (!reader.IsDBNull(0))
                {
                    pa.Id = (reader.GetInt32(0));
                }
                if (!reader.IsDBNull(1))
                {
                    pa.FirstName = reader.GetString(1);
                }
                if (!reader.IsDBNull(2))
                {
                    pa.LastName = reader.GetString(2);
                }
                pas.Add(pa);
            }
            reader.Close();
            con.Close();
            return pas;
        }
        public int selectReha(long agid, int month, int year)
        {
            int reha = 0;
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "select tage from reha where agid=@agid and jahr=@jahr and monat=@monat";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, agid);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, month);

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    reha = reader.GetInt32(0);
                }
            }
            reader.Close();
            con.Close();
            return reha;
        }

        public void DeleteReha(int month, int year, long agid, int days)
        {
            Npgsql.NpgsqlConnection con = DB.DBConnector.GetConnection();
        
            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "DELETE FROM reha WHERE agid=@agid AND monat=@monat AND jahr=@jahr AND tage=@tage";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, agid);
            DB.DBConnector.AddToCommand("@tage", NpgsqlTypes.NpgsqlDbType.Numeric, command, days);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

        }

        public void InsertReha(long agid, int month, int year, int days)
        {
            Npgsql.NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "Insert into reha (agid,monat,jahr,tage) values (@agid,@monat,@jahr,@tage)";

            DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, command, agid);
            DB.DBConnector.AddToCommand("@tage", NpgsqlTypes.NpgsqlDbType.Numeric, command, days);
            DB.DBConnector.AddToCommand("@monat", NpgsqlTypes.NpgsqlDbType.Numeric, command, month);
            DB.DBConnector.AddToCommand("@jahr", NpgsqlTypes.NpgsqlDbType.Numeric, command, year);

            try { command.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }

        }
    }
}
