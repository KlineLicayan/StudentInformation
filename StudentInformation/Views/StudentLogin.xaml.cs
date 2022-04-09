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
using StudentInformation.Forms;
using StudentInformation;
using MySql.Data.MySqlClient;

namespace StudentInformation.Views
{
    /// <summary>
    /// Interaction logic for StudentLogin.xaml
    /// </summary>
    public partial class StudentLogin : UserControl
    {
        public StudentLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string database = "server=localhost; database=school_system; user=root; password=jeffkingz1243; port=3306;";
            string command = "";
            string[] information = new string[6];

            MySqlConnection conn = new MySqlConnection(database);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr;

            if (StudentID.Text == "ADMIN" && StudentName.Text == "ADMIN")
            {
                StudentDatabase student_database = new StudentDatabase();
                student_database.Show();
                var window = Window.GetWindow(this);
                window.Close();
            } else
            {
                try
                {
                    conn.Open();

                    command = $"SELECT * FROM student_information WHERE STUDENT_ID = '{StudentID.Text}';";

                    cmd.Connection = conn;
                    cmd.CommandText = command;
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            information[i] = rdr[i].ToString();
                        }

                    }
                    rdr.Close();

                    if (information[0] == StudentID.Text &&
                        information[1] == StudentName.Text)
                    {
                        MainWindow main = new MainWindow(information);
                        var mywindow = Window.GetWindow(this);

                        main.Show();
                        mywindow.Close();
                    }
                    else
                    {
                        StudentName.Text = "Invalid Input";
                        StudentID.Text = "Invalid Input";
                        StudentName.Foreground = Brushes.Red;
                        StudentID.Foreground = Brushes.Red;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return;
        }

        private void ForgotID(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Requesting...");
            return;
        }

        private void StudentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentID.Text == "Student ID" || StudentID.Text == "Invalid Input")
            {
                StudentID.Text = "";
                StudentID.Foreground = Brushes.Black;
            }
            return;
        }

        private void StudentName_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentName.Text == "Student Name" || StudentName.Text == "Invalid Input")
            {
                StudentName.Text = "";
                StudentName.Foreground = Brushes.Black;
            }
            return;
        }

        private void Reset_KeyUp(object sender, KeyEventArgs e)
        {
            var br = new BrushConverter();
            if (StudentID.Text == "")
            {
                StudentID.Text = "Student ID";
                StudentID.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }

            if (StudentName.Text == "")
            {
                StudentName.Text = "Student Name";
                StudentName.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            return;
        }

    }
}
