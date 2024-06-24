using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace WpfApplication1
{
    class Employee
    {
        private int id;
        private string first_name;
        private string last_name;
        private double hourly_rate;

        public Employee(string first_name, string last_name, double hourly_rate)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.hourly_rate = hourly_rate;

        }
        public void set_ID(int id) { this.id = id; }
        public int get_ID() { return id; }
        public void set_First_Name(string first_name) { this.first_name = first_name; }
        public string get_First_Name() { return first_name; }
        public void set_Last_Name(string last_name) { this.last_name = last_name; }
        public string get_Last_Name() { return last_name; }
        public void set_Hourly_Rate(double hourly_rate) { this.hourly_rate = hourly_rate; }
        public double get_Hourly_Rate() { return hourly_rate; }
    }
}