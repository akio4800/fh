using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace SelvesSoftware.DB
{
    public class PAtoPurchaserDAO
    {
        internal void add(PersonalAssistant pa, Purchaser p)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand cmd = new NpgsqlCommand(null, con);

            try
            {

                cmd.CommandText = "INSERT INTO Dienstverhaeltnis VALUES (@agid,@paid,@dienstvertrag)";

                DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, cmd, p.Id);
                DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, cmd, pa.Id);
                DB.DBConnector.AddToCommand("@dienstvertrag", NpgsqlTypes.NpgsqlDbType.Varchar, cmd, "null");


                try { cmd.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }

            con.Close();
        }

        internal void delete(PersonalAssistant pa, Purchaser p)
        {
            NpgsqlConnection con = DB.DBConnector.GetConnection();
            NpgsqlCommand cmd = new NpgsqlCommand(null, con);

            try
            {

                cmd.CommandText = "DELETE FROM Dienstverhaeltnis WHERE \"agid\"=@agid AND \"paid\"=@paid";

                DB.DBConnector.AddToCommand("@agid", NpgsqlTypes.NpgsqlDbType.Numeric, cmd, p.Id);
                DB.DBConnector.AddToCommand("@paid", NpgsqlTypes.NpgsqlDbType.Numeric, cmd, pa.Id);


                try { cmd.ExecuteNonQuery(); } catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Error"); }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
            /*
            NpgsqlCommand commit = new NpgsqlCommand(null, con);
            commit.CommandText = "COMMIT";
            cmd.ExecuteNonQuery();
            */
            con.Close();
        }
    }
}
