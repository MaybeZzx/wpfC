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

namespace employee
{
    public partial class ReadInfo : Window
    {
        public ReadInfo()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            string sql = "SELECT id,first_name, last_name, hourly_rate, department_no FROM employee_photo; ";
            using (MySqlConnection connection = new MySqlConnection(Connect))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGrid1.DataContext = dt;
                    dataGrid1.ItemsSource = dt.DefaultView;
                }
                connection.Close();
                textBlock1.Text = string.Empty;
            }
        }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format($"last_name  like '{textBox1.Text}%'");
        }
        private void dataGrid1_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dataGrid1.SelectedIndex >= 0)
            {
                string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
                string x = dt.DefaultView[dataGrid1.SelectedIndex]["id"].ToString();
                string sql = "SELECT id, first_name, last_name, hourly_rate, department.department_no, photo, department_name, department_manager "
                    + "FROM employee_photo inner join department on employee_photo.department_no = department.department_no where id = '" + x + "'; ";
                image1.Visibility = Visibility.Visible;
                buttonHide.Visibility = Visibility.Visible;

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
                            object department_no = reader.GetValue(4);
                            object department_name = reader.GetValue(6);
                            object department_manager = reader.GetValue(7);
                            try
                            {
                                byte[] data = (byte[])reader[5];

                                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                                {
                                    var imageSource = new BitmapImage();
                                    imageSource.BeginInit();
                                    imageSource.StreamSource = ms;
                                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                    imageSource.EndInit();

                                    image1.Source = imageSource;
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Missing Photo");
                                image1.Source = new BitmapImage(new Uri("Images/noname.jpg", UriKind.Relative));
                            };
                            textBlock1.Text = "id: " + Convert.ToString(id) + "\n"
                            + "First Name: " + Convert.ToString(first_name) + "\n"
                            + "Last Name: " + Convert.ToString(last_name) + "\n"
                            + "Hourly Rate: " + Convert.ToString(hourly_rate) + "\n"
                            + "Department No: " + Convert.ToString(department_no) + "\n"
                            + "Department: " + Convert.ToString(department_name) + "\n"
                            + "Department Manager: " + "\n" + Convert.ToString(department_manager);
                        }
                    }
                    reader.Close();
                }
                textBlock1.Visibility = Visibility.Visible;
            }

        }
        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            buttonHide.Visibility = Visibility.Hidden;
            image1.Visibility = Visibility.Hidden;
            textBlock1.Visibility = Visibility.Hidden;
        }


    }
}
