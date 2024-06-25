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
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.IO;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace employee
{

    public partial class InsertDB : Window
    {
        private string photo;
        public InsertDB()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string resetAutoIncrementSql;
            MySqlCommand resetAutoIncrementCommand;
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            MySqlConnection connection = new MySqlConnection(Connect);
            string command = "select department_no, department_name, department_manager from department";
            connection.Open();
            string countQuery = "SELECT COUNT(*) FROM employee;";
            MySqlCommand countCommand = new MySqlCommand(countQuery, connection);
            int count = Convert.ToInt32(countCommand.ExecuteScalar());

            if (count == 0)
            {
                resetAutoIncrementSql = "ALTER TABLE employee AUTO_INCREMENT = 1;";
                resetAutoIncrementCommand = new MySqlCommand(resetAutoIncrementSql, connection);
                resetAutoIncrementCommand.ExecuteNonQuery();
            }
            else
            {
                count++;
                resetAutoIncrementSql = $"ALTER TABLE employee AUTO_INCREMENT = {count};";
                resetAutoIncrementCommand = new MySqlCommand(resetAutoIncrementSql, connection);
                resetAutoIncrementCommand.ExecuteNonQuery();
            }
            connection.Close();
            MySqlDataAdapter da = new MySqlDataAdapter(command, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            Departments.DataContext = dt;
            Departments.SelectedIndex = 0;
            textBox_HourlyRate.Text = "0.00";
            BitmapImage bm1 = new BitmapImage();

           

        }

        private void Departments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Departments_Value();
        }
        private string Departments_Value()
        {
            DataRowView myDataRowView = Departments.SelectedItem as DataRowView;

            string sValue = string.Empty;
            if (myDataRowView != null)
            {
                sValue = myDataRowView.Row["department_no"] as string;
            }
            return sValue;
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Images(*.JPG;*.GIF;*.PNG)|*.JPG;*.GIF;*.PNG" + "|All files (*.*)|*.* ";
            myDialog.CheckFileExists = true;
            if (myDialog.ShowDialog() == true)
            {
                try
                {
                    photo = myDialog.FileName;
                    string fileName = Path.GetFileName(photo);
                    BitmapImage bm1 = new BitmapImage();
                    bm1.BeginInit();
                    bm1.UriSource = new Uri(myDialog.FileName, UriKind.Relative);
                    bm1.CacheOption = BitmapCacheOption.OnLoad;
                    bm1.EndInit();
                    image1.Source = bm1;

                   
                }
                catch
                {
                    MessageBox.Show("Unable to Load Image");
                    image1.Source = new BitmapImage(new Uri(@"..\image\noname.png", UriKind.Relative));

                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string Connect = "server=127.0.0.1; port=3306; userid=admin; password=1234567890Qwe.; database=my_company; sslmode=none;";
            MySqlConnection con = new MySqlConnection(Connect);
            try
            {
                con.Open();
                MySqlCommand comm = con.CreateCommand();
                string first_name, last_name;
                
                if (string.IsNullOrEmpty(textBox_FirstName.Text) ||
                string.IsNullOrWhiteSpace(textBox_FirstName.Text))
                {
                    InputControl inputDialog = new InputControl("Please enter your name:", "");
                    if (inputDialog.ShowDialog() == true)
                        textBox_FirstName.Text = inputDialog.Answer;
                }
                if (string.IsNullOrEmpty(textBox_LastName.Text) ||
                string.IsNullOrWhiteSpace(textBox_LastName.Text))
                {
                    InputControl inputDialog = new InputControl("Please enter your family name:", "");
                    if (inputDialog.ShowDialog() == true)
                        textBox_LastName.Text = inputDialog.Answer;
                }
                if (!Regex.IsMatch(textBox_FirstName.Text, @"^[a-zA-Z]{1,20}$"))
                {
                    MessageBox.Show("First Name - Invalid text");
                    textBox_FirstName.Text = String.Empty;
                    first_name = String.Empty;
                }
                else { first_name = textBox_FirstName.Text; };

                if (!Regex.IsMatch(textBox_LastName.Text, @"^[a-zA-Z]{1,20}$"))
                {
                    MessageBox.Show("Last Name - Invalid text");
                    textBox_LastName.Text = String.Empty;
                    last_name = String.Empty;
                }
                else { last_name = textBox_LastName.Text; };

                string department_no = Departments_Value();
                string CultureName = Thread.CurrentThread.CurrentCulture.Name;
                CultureInfo ci = new CultureInfo(CultureName);
                if (ci.NumberFormat.NumberDecimalSeparator != ".")
                {
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = ci;
                }
                else
                {
                    MessageBox.Show(" Missing record");
                }
                double hourly_rate;

                if ((string.IsNullOrEmpty(textBox_FirstName.Text)) &&
                (string.IsNullOrEmpty(textBox_LastName.Text)))
                {
                    MessageBox.Show("Missing file name");
                }
                else
                {
                    JpegBitmapEncoder jpegBitmapEncoder = new JpegBitmapEncoder();
                    jpegBitmapEncoder.Frames.Add(BitmapFrame.Create(image1.Source as BitmapSource));
                    string fileName = $"{textBox_FirstName.Text}_{textBox_LastName.Text}.jpg";
                    string filePath = $@"D:\wpf\image\{fileName}";
                    photo = filePath;
                    string directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (FileStream file = new FileStream(filePath, FileMode.Create))
                    {
                        jpegBitmapEncoder.Save(file);
                    }
                }
                if (!string.IsNullOrEmpty(textBox_FirstName.Text) ||
                (!string.IsNullOrEmpty(textBox_LastName.Text)))
                {
                    if (double.TryParse(textBox_HourlyRate.Text, out hourly_rate))
                    {
                        comm.CommandText = "INSERT INTO employee (first_name, last_name, hourly_rate, department_no, photo) " +
                            "VALUES('" + first_name + "', '" + last_name + "', '" + hourly_rate+"','"+department_no+"','"+photo+"');";
                        MessageBox.Show(comm.CommandText);
                        comm.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Missing hourly rate");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
