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

namespace StudentInformation.Forms
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : Window
    {
        public StudentForm()
        {
            InitializeComponent();
        }
        private void MoveWidow(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            return;
        }
        
        private void ChangeView(UserControl title, UserControl content)
        {
            UserControl[] user = { LoginTitle, LoginView, SignInTitle, SignInView };
            for (int i = 0; i < user.Length; i++)
            {
                user[i].Visibility = Visibility.Collapsed;
            }

            title.Visibility = Visibility.Visible;
            content.Visibility = Visibility.Visible;

            return;
        }

        private void ChangeButton(Button mybtn, Image img)
        {
            var br = new BrushConverter();
            Button[] btn = { BtnLogin, BtnSignIn };

            for (int i=0; i<btn.Length; i++)
            {
                btn[i].Background = (Brush)br.ConvertFrom("#FFFFFF");
            }

            ImageLogin.Source = new BitmapImage(new Uri("RedReader.png", UriKind.Relative));
            ImageSignIn.Source = new BitmapImage(new Uri("RedHat.png", UriKind.Relative));


            mybtn.Background = (Brush)br.ConvertFrom("#C92121");
            return;
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
          
            ChangeView(LoginTitle, LoginView);
            ChangeButton(BtnLogin, ImageLogin);
            ImageLogin.Source = new BitmapImage(new Uri("WhiteReader.png", UriKind.Relative));
            return;
        }

        private void SignInButton(object sender, RoutedEventArgs e)
        {
            ChangeView(SignInTitle, SignInView);
            ChangeButton(BtnSignIn, ImageSignIn);
            ImageSignIn.Source = new BitmapImage(new Uri("WhiteHat.png", UriKind.Relative));
            return;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {}
    }
}
