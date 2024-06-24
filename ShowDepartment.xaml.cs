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

    public partial class ShowDepartment : Window
    {
        public ShowDepartment()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DepartmentInfo department = new DepartmentInfo();
            department.department_no = textBox1.Text;
            department.Show();
        }
    }
}
