using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace DB_TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           SqlDbType.
            InitializeComponent();
        }

        private void Button_Click(object Sender, RoutedEventArgs e){
            SQLPreparedCommand("Data Source=PostgreSQL 9.4\\postgres;AttachDbFilename=\"U:\\!!Projekt eigen\\TESTDB.mdf\";Integrated Security=true;Connect Timeout=10");
        }

        


        private void SQLPreparedCommand(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();

                SqlCommand command = new SqlCommand(null, connection);
                command.CommandText = "Insert INTO PERSONS (id,LastName,FirstName,Description)" + "Values (@id, @ln, @fn,@dsc)";
                SqlParameter id = new SqlParameter("@id", SqlDbType.Int, 3);
                SqlParameter ln = new SqlParameter("@ln", SqlDbType.Text, 100);
                SqlParameter fn = new SqlParameter("@fn", SqlDbType.Text, 100);
                SqlParameter dsc = new SqlParameter("@dsc", SqlDbType.Text, 2000);
                id.Value = "2";
                ln.Value = "Stark";
                fn.Value = "Eddard";
                dsc.Value = "One head short";

                command.Parameters.Add(id);
                command.Parameters.Add(ln);
                command.Parameters.Add(fn);
                command.Parameters.Add(dsc);

                command.Prepare();
                command.ExecuteNonQuery();

               
        }

        }
    }
}
