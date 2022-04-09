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
using StudentInformation.Forms;
using MySql.Data.MySqlClient;

namespace StudentInformation
{
    /// <summary>
    /// Interaction logic for StudentDatabase.xaml
    /// </summary>

    public class StudentGrade
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Math { get; set; }
        public string Science { get; set; }
        public string English { get; set; }
        public string Filipino { get; set; }
        public string PE { get; set; }
    }
    public partial class StudentDatabase : Window
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataReader rdr;

        private string connection;

        public StudentDatabase()
        {
            InitializeComponent();

            this.connection = "server=localhost;port=3306;user=root; password=jeffkingz1243; database=school_system;";
            this.conn = new MySqlConnection(this.connection);
            this.cmd = new MySqlCommand();
            DisplayStudent();

            return;
        }

        private void DisplayStudent()
        {
            Display.Items.Clear();
            string command = $"SELECT * FROM student_grade;";
            try
            {
                this.conn.Open();

                this.cmd.Connection = this.conn;
                this.cmd.CommandText = command;
                this.rdr = this.cmd.ExecuteReader();

                while (this.rdr.Read())
                {
                    StudentGrade student = new StudentGrade()
                    {
                        ID = rdr[0].ToString(),
                        Name = rdr[1].ToString(),
                        Math = rdr[2].ToString(),
                        Science = rdr[3].ToString(),
                        English = rdr[4].ToString(),
                        Filipino = rdr[5].ToString(),
                        PE = rdr[6].ToString()
                    };
                    Display.Items.Add(student);
                }
                this.rdr.Close();
            } 
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                this.conn.Close();
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

        private void SubmitGradeButton(object sender, RoutedEventArgs e)
        {
            TextBox[] box = { IDBox, MathBox, ScienceBox, EnglishBox, FilipinoBox, PEBox };
            string[] column = { "MATH", "SCIENCE", "ENGLISH", "FILIPINO", "PE" };
            if (!string.IsNullOrWhiteSpace(IDBox.Text) &&
                !string.IsNullOrWhiteSpace(MathBox.Text) &&
                !string.IsNullOrWhiteSpace(ScienceBox.Text) &&
                !string.IsNullOrWhiteSpace(EnglishBox.Text) &&
                !string.IsNullOrWhiteSpace(FilipinoBox.Text) &&
                !string.IsNullOrWhiteSpace(PEBox.Text) 
                )
            {
                string command = $"UPDATE student_grade SET ";
                for (int i=0; i<box.Length-1; i++)
                {
                    command += $" {column[i]} = '{box[i + 1].Text}' ";
                    if (i != (box.Length - 2))
                    {
                        command += " , ";
                    }
                }
                command += $" WHERE STUDENT_ID = '{IDBox.Text}';";
              
                try
                {
                    this.conn.Open();
                    this.cmd.Connection = this.conn;
                    this.cmd.CommandText = command;
                    this.cmd.ExecuteNonQuery();
                } 
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    this.conn.Close();
                }
                DisplayStudent();
                for (int i=0; i<box.Length; i++)
                {
                    box[i].Clear();
                }
            } 
            else
            {
                for (int i=0; i<box.Length; i++)
                {
                    box[i].Text = "Invalid Input";
                    box[i].Foreground = Brushes.Red;
                }   
            }
            return;
        }

        private void SignOutButton(object sender, RoutedEventArgs e)
        {
            StudentForm form = new StudentForm();
            form.Show();
            this.Close();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                DisplayStudent();
            } else
            {
                Display.Items.Clear();
                string command =$"SELECT * FROM student_grade WHERE STUDENT_NAME LIKE '{SearchBox.Text}%';";

                try
                {
                    this.conn.Open();
                    this.cmd.Connection = this.conn;
                    this.cmd.CommandText = command;
                    this.rdr = this.cmd.ExecuteReader();

                    while (this.rdr.Read())
                    {
                        StudentGrade student = new StudentGrade()
                        {
                            ID = rdr[0].ToString(),
                            Name = rdr[1].ToString(),
                            Math = rdr[2].ToString(),
                            Science = rdr[3].ToString(),
                            English = rdr[4].ToString(),
                            Filipino = rdr[5].ToString(),
                            PE = rdr[6].ToString()
                        };
                        Display.Items.Add(student);
                    }
                    this.rdr.Close();

                } catch(Exception err)
                {
                    MessageBox.Show(err.Message);
                } finally
                {
                    this.conn.Close();
                }
            }
            return;
        }


        private void IDBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IDBox.Text == "Student ID" || IDBox.Text == "Invalid Input") 
            {
                IDBox.Text = "";
                IDBox.Foreground = Brushes.Black;
            }
            return;
        }
        private void MathBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (MathBox.Text == "Math Grade" || MathBox.Text == "Invalid Input")
            {
                MathBox.Text = "";
                MathBox.Foreground = Brushes.Black;
            }
            return;
        }

        private void ScienceBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (ScienceBox.Text == "Science Grade" || ScienceBox.Text == "Invalid Input")
            {
                ScienceBox.Text = "";
                ScienceBox.Foreground = Brushes.Black;
            }
            return;
        }

        private void EnglishBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (EnglishBox.Text == "English Grade" || EnglishBox.Text == "Invalid Input")
            {
                EnglishBox.Text = "";
                EnglishBox.Foreground = Brushes.Black;
            }
            return;
        }

        private void FilipinoBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (FilipinoBox.Text == "Filipino Grade" || FilipinoBox.Text == "Invalid Input")
            {
                FilipinoBox.Text = "";
                FilipinoBox.Foreground = Brushes.Black;
            }
            return;
        }

        private void PEBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (PEBox.Text == "PE Grade" || PEBox.Text == "Invalid Input")
            {
                PEBox.Text = "";
                PEBox.Foreground = Brushes.Black;
            }
            return;
        }
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (SearchBox.Text == "Search")
            {
                SearchBox.Text = "";
                SearchBox.Foreground = Brushes.Black;
            }
            return;
        }

        private void Reset_KeyUp(object sender, KeyEventArgs e)
        {
            var br = new BrushConverter();
            if (MathBox.Text == "")
            {
                MathBox.Text = "Math Grade";
                MathBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            if (ScienceBox.Text == "")
            {
                ScienceBox.Text = "Science Grade";
                ScienceBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            if (EnglishBox.Text == "")
            {
                EnglishBox.Text = "English Grade";
                EnglishBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            if (FilipinoBox.Text == "")
            {
                FilipinoBox.Text = "Filipino Grade";
                FilipinoBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            if (PEBox.Text == "")
            {
                PEBox.Text = "PE Grade";
                PEBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }
            if (IDBox.Text == "")
            {
                IDBox.Text = "Student ID";
                IDBox.Foreground = (Brush)br.ConvertFrom("#B5B5B5");
            }

            return;
        }

    }
}
