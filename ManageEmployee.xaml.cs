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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace employee
{

    public partial class ManageEmployee : Window
    {
        int Record;
        public ManageEmployee()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT id, first_name, last_name, hourly_rate from employee where id=" +
            "(select min(id) from employee_photo where department_no='L004');";
            myRecord(sql);
        }
        public void myRecord(string sql)
        {
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            using (MySqlConnection connection = new MySqlConnection(Connect))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object first_name = reader.GetValue(1);
                        object last_name = reader.GetValue(2);
                        object hourly_rate = reader.GetValue(3);
                        Record = Convert.ToInt32(id);
                        try
                        {
                            BitmapImage bm1 = new BitmapImage();
                            bm1.BeginInit();
                            bm1.UriSource = new Uri(@"e:\\images\" + id + ".jpg", UriKind.Relative);
                            bm1.CacheOption = BitmapCacheOption.OnLoad;
                            bm1.EndInit();
                            image1.Source = bm1;
                        }
                        catch
                        {
                            MessageBox.Show("Missing Photo ");

                        };
                        textBox1.Text = "id: " + Convert.ToString(id);
                        textBox2.Text = "first name: " + Convert.ToString(first_name);
                        textBox3.Text = "last name: " + Convert.ToString(last_name);
                        textBox4.Text = "hourly rate: " + Convert.ToString(hourly_rate);
                    }
                }
                reader.Close();
            }

        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT id, first_name, last_name, hourly_rate from employee where" +
            " (department_no='L004' AND id>" + Record + " ) ORDER BY id limit 1;";
            myRecord(sql);
        }
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT id, first_name, last_name, hourly_rate from employee where" +
            "(department_no='L004' AND id<" + Record + " ) ORDER BY id DESC limit 1;";
            myRecord(sql);
        }

       
    }
}
