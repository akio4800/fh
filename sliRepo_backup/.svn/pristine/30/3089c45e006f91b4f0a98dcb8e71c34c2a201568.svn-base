using System;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace SelvesSoftware.DB
{
    /// <summary>
    /// TS
    /// </summary>
    public static class DBConnector
    {

        static string server;
        static string port;
        static string user;
        static string password;

        static string _connstring = "Server=193.170.192.130;Port=5432;User Id=postgres;Password=1234;Database=postgres;CommandTimeout=5;";


        static NpgsqlConnection _conn;
        

        public static NpgsqlConnection GetConnection()
        {
            if (_conn == null)
            {
                //TODO EXCEPTION DB NOt possible
                _conn = new NpgsqlConnection(_connstring);
            }

            try
            {
                if (!(_conn.State == System.Data.ConnectionState.Open))
                {
                    _conn.Open();
                }
                else
                {
                    _conn.Close();
                    _conn.Open();
                }
                
                }catch(Exception e)
                {
                    MessageBox.Show("Datenbankverbindung nicht möglich\n" + e.Message.ToString(), "Error");
                }
            


            return _conn;


        }


        public static void getConfig(){


            try {
                string[] lines = System.IO.File.ReadAllLines(@"config.txt");

                String[] serverS = lines[0].Split('=');
                server = serverS[1];
                String[] portS = lines[1].Split('=');
                port = portS[1];
                String[] userS = lines[2].Split('=');
                user = userS[1];
                String[] pwS = lines[3].Split('=');
                password = pwS[1];

                string connstring = "Server=" + server + ";Port=" + port + ";User Id=" + user + ";Password=" + password + ";Database=postgres;CommandTimeout=3;";
                _connstring = connstring;
            }catch(Exception e)
            {
                MessageBox.Show("Datenbankverbindung nicht möglich"+e.Message.ToString(),"Error");
            }
        }

        public static void AddToCommand(string sub, NpgsqlTypes.NpgsqlDbType type, NpgsqlCommand command, Object o)
        {
            NpgsqlParameter param=null;
            if (o == null) {

                param = new NpgsqlParameter(sub, DBNull.Value);
            
            }else if(type == NpgsqlTypes.NpgsqlDbType.Date){
 
                DateTime date = (DateTime) o;
                param = new NpgsqlParameter(sub, type);


                param.Value = new NpgsqlTypes.NpgsqlDate((DateTime)o);
                
            }else if(type == NpgsqlTypes.NpgsqlDbType.Boolean){
                    param = new NpgsqlParameter(sub, type);
                    param.Value = ((bool)o) ? true : false;
                
            }else{
                param = new NpgsqlParameter(sub, type);
                param.Value = o.ToString();
                
            }
            command.Parameters.Add(param);
            }
        }
    }
