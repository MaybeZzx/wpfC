using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace employee
{

    public partial class DepartmentInfo : Window
    {
        public string department_no = string.Empty;
        public DepartmentInfo()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            label1.Content = "Department: " + department_no;
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            string sql1 = "SELECT AVG(hourly_rate) from employee where department_no='" + department_no + "';";
            string sql2 = "SELECT MAX(hourly_rate) from employee where department_no='" + department_no + "';";
            string sql3 = "SELECT MIN(hourly_rate) from employee where department_no='" + department_no + "';";
            MySqlConnection connection = new MySqlConnection(Connect);

            connection.Open();

            MySqlCommand command = new MySqlCommand(sql1 + sql2 + sql3, connection);
            MySqlDataReader reader = command.ExecuteReader();
            label2.Content = string.Empty;

            do
            {
                while (reader.Read())
                {
                    for (int field = 0; field < reader.FieldCount; field++)
                    {
                        label2.Content = label2.Content + reader.GetName(field).ToString() +
                        ": " + reader.GetValue(field).ToString() + "\n";
                    }
                }
            } while (reader.NextResult());
            
            reader.Close();
            string sql = "SELECT COUNT(id) from employee where department_no='"
            + department_no + "';";
            command = new MySqlCommand(sql, connection);

            int i = Convert.ToInt32(command.ExecuteScalar()); ;
            label3.Content = "number of employees: " + i;
            connection.Close();
        }

    }
}
