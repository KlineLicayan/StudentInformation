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

namespace StudentInformation
{
    /// <summary>
    /// Interaction logic for ViewGrade.xaml
    /// </summary>
    public partial class ViewGrade : Window
    {
        private MainWindow main;
        public ViewGrade( MainWindow main, string student_name)
        {
            InitializeComponent();
            this.main = main;
            NameHere.Text = student_name;
            DisplayGrade(student_name);
        }

        private void DisplayGrade(string student_name)
        {

            MySqlConnection conn = new MySqlConnection("server=localhost; port=3306; database=school_system; user=root; password=jeffkingz1243;");
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr;

            try
            {
                conn.Open();
                string command = $"SELECT MATH, SCIENCE, ENGLISH, FILIPINO, PE FROM student_grade WHERE STUDENT_NAME = '{student_name}';";

                cmd.Connection = conn;
                cmd.CommandText = command;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    MathGrade.Content = (rdr[0].ToString() == "0")? "INC" : rdr[0].ToString();   
                    ScienceGrade.Content = (rdr[1].ToString() == "0") ? "INC" : rdr[1].ToString();
                    EnglishGrade.Content = (rdr[2].ToString() == "0") ? "INC" : rdr[2].ToString();
                    FilipinoGrade.Content = (rdr[3].ToString() == "0") ? "INC" : rdr[3].ToString();
                    PEGrade.Content = (rdr[4].ToString() == "0") ? "INC" : rdr[4].ToString();
                }
                rdr.Close();

            } catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                conn.Close();
            }

            return;
        }

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            return;
        }

        private void ExitWindow(object sender, RoutedEventArgs e)
        {
            this.main.WindowState = WindowState.Normal;
            this.Close();

            return;
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            return;
        }
    }
}
