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

namespace employee
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonTask1_Click(object sender, RoutedEventArgs e)
        {
            ListEmployee listEmployee = new ListEmployee();
            listEmployee.Show();
        }

        private void buttonTask2_Click(object sender, RoutedEventArgs e)
        {
            InsertDB insertDB = new InsertDB();
            insertDB.Show();
        }

        private void buttonTask3_Click(object sender, RoutedEventArgs e)
        {
            ReadInfo readInfo = new ReadInfo();
            readInfo.Show();
        }

        private void buttonTask4_Click(object sender, RoutedEventArgs e)
        {
            DeleteInfo deleteInfo = new DeleteInfo();
            deleteInfo.Show();
        }

        private void buttonTask5_Click(object sender, RoutedEventArgs e)
        {
            ShowDepartment department = new ShowDepartment();
            department.Show();
        }

        private void buttonTask6_Click(object sender, RoutedEventArgs e)
        {
            ManageEmployee manageEmployee = new ManageEmployee();
            manageEmployee.Show();
        }
    }
}
