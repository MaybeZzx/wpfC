using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using System.Data;
using MySql.Data;
using WpfApplication1;

namespace employee
{

    public partial class ListEmployee : Window
    {
        public ListEmployee()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            MySqlConnection connection = new MySqlConnection(Connect);

            List<Employee> my_list = new List<Employee>();
            string sql = "SELECT id, first_name, last_name, hourly_rate from employee;";
            MySqlCommand command1 = new MySqlCommand(sql, connection);
            connection.Open();

            MySqlDataReader reader = command1.ExecuteReader();

            double number;

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    Employee my = new Employee("", "", 0.0);

                    my.set_ID(Convert.ToInt32(reader["id"]));
                    my.set_First_Name(reader["first_name"].ToString());
                    my.set_Last_Name(reader["last_name"].ToString());
                    if (double.TryParse(reader["hourly_rate"].ToString(), out number))
                        my.set_Hourly_Rate(number);

                    else
                        my.set_Hourly_Rate(0);
                    my_list.Add(my);
                }

                textBox1.ScrollToEnd();

                foreach (var emp in my_list)
                {
                    textBox1.Text = textBox1.Text + "ID:" + emp.get_ID() +
                    "\n First Name: " + emp.get_First_Name() +
                    "\n Last Name: " + emp.get_Last_Name() +
                    "\n Hourly Rate " + emp.get_Hourly_Rate() + "\n";
                };

            }
            else
            {
                MessageBox.Show("No rows found.");
            }

            reader.Close();
            connection.Close();

        }
    }
}



