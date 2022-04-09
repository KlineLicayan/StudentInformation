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
    /// Interaction logic for StudentSignIn.xaml
    /// </summary>
    public partial class StudentSignIn : UserControl
    {
        private string StudentID;

        public StudentSignIn()
        {
            InitializeComponent();
            GenerateSchoolID();
        }

        private void GenerateSchoolID()
        {
            this.StudentID = "";
            Random random = new Random();
            int random_num = 0;
            for (int i=0; i < 11; i++)
            {
                random_num = random.Next(10);
                this.StudentID += (i == 2 || i == 7) ? "-" : $"{random_num}";
            }
            return;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string database = "server=localhost; database=school_system; user=root; password=jeffkingz1243; port=3306";
            string command = "";
            string[] information = new string[6];
            bool isExist = false;

            MySqlConnection conn = new MySqlConnection(database);
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader rdr;

            try
            {
                conn.Open();
                command = $"SELECT COUNT(STUDENT_ID) FROM student_information WHERE STUDENT_ID = '{this.StudentID}'";

                cmd.Connection = conn;
                cmd.CommandText = command;
                rdr = cmd.ExecuteReader();

                rdr.Read();
                isExist = (rdr[0].ToString() == "0") ? true : false;
                rdr.Close();

            } catch (Exception err)
            {
                MessageBox.Show(err.Message);
            } finally
            {
                conn.Close();
            }

            TextBox[] box = {StudentName, StudentCourse, StudentAddress,
                                StudentContact, StudentGuardian};

            if (isExist)
            {
                
                try
                {
                    conn.Open();
                    information[0] = Convert.ToString(this.StudentID);
                    for (int i=0; i<5; i++)
                    {
                        information[i+1] = box[i].Text;
                    }
                    command = $"INSERT INTO student_information VALUES (";
                    for (int i = 0; i < 6; i++)
                    {
                        command += $"'{information[i]}'";
                        if (i != 5)
                        {
                            command += ",";
                        }
                    }
                    command += ");";

                    cmd.Connection = conn;
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();

                    command = $"INSERT INTO student_grade VALUES ('{this.StudentID}', '{StudentName.Text}', 0,0,0,0,0);";
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();

                    MainWindow main = new MainWindow(information);
                    var mywindow = Window.GetWindow(this);
                    main.Show();
                    mywindow.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    conn.Close();
                }
            } else
            {
                for (int i=0; i<5; i++)
                {
                    box[i].Text = "Invalid Input";
                    box[i].Foreground = Brushes.Red;
                }
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

        private void StudentCourse_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentCourse.Text == "Student Course" || StudentCourse.Text == "Invalid Input")
            {
                StudentCourse.Text = "";
                StudentCourse.Foreground = Brushes.Black;
            }
            return;
        }

        private void StudentAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentAddress.Text == "Student Address" || StudentAddress.Text == "Invalid Input")
            {
                StudentAddress.Text = "";
                StudentAddress.Foreground = Brushes.Black;
            }
            return;
        }

        private void StudentContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentContact.Text == "Student Contact" || StudentContact.Text == "Invalid Input")
            {
                StudentContact.Text = "";
                StudentContact.Foreground = Brushes.Black;
            }
            return;
        }

        private void StudentGuardian_KeyDown(object sender, KeyEventArgs e)
        {
            if (StudentGuardian.Text == "Student Guardian" || StudentGuardian.Text == "Invalid Input")
            {
                StudentGuardian.Text = "";
                StudentGuardian.Foreground = Brushes.Black;
            }
            return;
        }

        private void Reset_KeyUp(object sender, KeyEventArgs e)
        {
            var br = new BrushConverter();
            
            if (StudentCourse.Text == "")
            {
                StudentCourse.Text = "Student Course";
                StudentCourse.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }

            if (StudentAddress.Text == "")
            {
                StudentAddress.Text = "Student Address";
                StudentAddress.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }

            if (StudentContact.Text == "")
            {
                StudentContact.Text = "Student Contact";
                StudentContact.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }

            if (StudentGuardian.Text == "")
            {
                StudentGuardian.Text = "Student Guardian";
                StudentGuardian.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
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
