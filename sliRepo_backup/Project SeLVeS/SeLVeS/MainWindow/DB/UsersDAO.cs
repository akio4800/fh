using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SelvesSoftware.DB
{
    public class UsersDAO
    {
        public List<String> selectUserNames()
        {
            List<String> users = new List<String>();
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT username FROM users";

            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }

                con.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
            return users;
        }
        public String selectPassword(String username)
        {
            String password=null;
            NpgsqlConnection con = DB.DBConnector.GetConnection();

            NpgsqlCommand command = new NpgsqlCommand(null, con);
            command.CommandText = "SELECT passwort FROM users WHERE username=@username";
            DB.DBConnector.AddToCommand("@username", NpgsqlTypes.NpgsqlDbType.Varchar, command, username);
            try
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    password=(reader.GetString(0));
                }

                con.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
            return password;
        }
    }
}
