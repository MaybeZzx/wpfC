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
using MySql.Data.MySqlClient;
using System.Data;
using MySql.Data;
using System.Runtime.Remoting.Messaging;
using WpfApplication1;


namespace employee
{
    public partial class DeleteInfo : Window
    {
        DataTable dt = new DataTable();

        public DeleteInfo()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            string sql = "SELECT * FROM employee";
            using (MySqlConnection connection = new MySqlConnection(Connect))
            {

                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    dt.Columns.Add("ID", typeof(String));
                    dt.Columns.Add("First_Name", typeof(String));
                    dt.Columns.Add("Last_Name", typeof(String));
                    dt.Columns.Add("Hourly_Rate", typeof(String));
                    dt.Columns.Add("Department_No", typeof(String));
                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object first_name = reader.GetValue(1);
                        object last_name = reader.GetValue(2);
                        object hourly_rate = reader.GetValue(3);
                        object department_no = reader.GetValue(4);

                        dt.Rows.Add(id.ToString(), first_name.ToString(), last_name.ToString(),
                        hourly_rate.ToString(), department_no.ToString());
                    }

                    Employees.ItemsSource = dt.DefaultView;
                }
                reader.Close();
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt1 = new DataTable();

                dt1 = dt;
                int index = Employees.SelectedIndex;
                int id = Convert.ToInt32(dt1.DefaultView[index]["id"]);
                DataRow b = dt1.Rows[index];
                dt1.Rows.Remove(b);
                Employees.ItemsSource = dt1.DefaultView;
                string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";

                string sql = "delete FROM employee where id=" + id + ";";

                MySqlConnection connection = new MySqlConnection(Connect);
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                int countLine = command.ExecuteNonQuery();
                MessageBox.Show("Количество удаленных строк " + countLine);
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format($"last_Name like '{textBox1.Text}%'");
        }
}

}
