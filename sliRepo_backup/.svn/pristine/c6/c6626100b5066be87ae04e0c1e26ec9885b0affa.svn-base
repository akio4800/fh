using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace MainWindow.DB
{
    public class PersonalAssistentDAO : IPersonalAssistentDAO
    {
        DBConnector connector;
        public PersonalAssistentDAO()
        {
            connector = new DBConnector();
        }
        public void Insert(PersonalAssistant pa){
        SqlConnection con = connector.getConnection();
        using (con)
        {
            con.Open();

            SqlCommand command = new SqlCommand(null, con);
            command.CommandText = "Insert INTO persoenlicherassistentview" +
                                 "(personid,vorname,nachname,telnummer,mobiltelefonnummer,email," +
                                 "addressid,strasse,stadt,land,hausnummer,stiege,stock,plz,tuer,abgabeunterlagenbis,aktiv)" +
                                 "Values (@personid,@vorname,@nachname,@telnummer,@mobiltelefonnummer,@email," +
                                 "@addressid,@strass,@stadt,@land,@hausnummer,@stiege,@stock,@plz,@tuer@," +
                                 "@abgabeunterlagenbis,@aktiv)";
            SqlParameter personid = new SqlParameter("@personid", SqlDbType.Int);
        }
	}




    }
}
