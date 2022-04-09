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
using MySql.Data.MySqlClient;

namespace StudentInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[] box;

        private MySqlConnection conn;
        private MySqlCommand cmd;

        public MainWindow(string [] student)
        {
            InitializeComponent();
            this.box = new TextBox[6]
            {
                StudentID, StudentName, StudentCourse, StudentAddress
                , StudentContact, StudentGuardian
            };

            for (int i = 0; i < 6; i++) {
                this.box[i].Text = student[i];
            }

            this.conn = new MySqlConnection("server=localhost; port=3306; database=school_system; password=jeffkingz1243; user=root;");
            this.cmd = new MySqlCommand();

            StudentID.IsEnabled = false;
            StudentName.IsEnabled = false;
            StudentCourse.IsEnabled = false;
            return;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            return;
        }

        private void ExitWindow(object sender, RoutedEventArgs e)
        {

            StudentForm form = new StudentForm();
            form.Show();
            this.Close();
            
            return;
        }

        private void UpdateStudentInformation(object sender, RoutedEventArgs e)
        {
            string command = $"UPDATE student_information SET ";
            string[] column_name = { "STUDENT_ID","STUDENT_NAME", "STUDENT_COURSE",
            "STUDENT_ADDRESS", "STUDENT_CONTACT", "STUDENT_GUARDIAN"};
            try
            {
                this.conn.Open();
                for (int i = 0; i < 6; i++) {
                    command += $" {column_name[i]} = '{this.box[i].Text}' ";
                    if (i != 5)
                    {
                        command += " , ";
                    }
                    
                }
                command += $" WHERE STUDENT_ID = '{StudentID.Text}';";
         
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
            MessageBox.Show("Successfully Updated...");
            return;
        }

        private void RequestStudentGradeInformation(object sender, RoutedEventArgs e)
        {

            ViewGrade grade = new ViewGrade(this, StudentName.Text);
            grade.Show();
            this.WindowState = WindowState.Minimized;
            return;
        }
    }
}
