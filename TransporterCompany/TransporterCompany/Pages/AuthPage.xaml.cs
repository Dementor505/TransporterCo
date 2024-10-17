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
using TransporterCompany.MainDataBase;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            App.mainButtons.ClearWrapPanel();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = App.transBase.User.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordPb.Password);
            if (user != null)
            {
                if (RemMe.IsChecked == true)
                {
                    ActiveSession session = new ActiveSession
                    {
                        Login_User = user.Login,
                        Computer_Number = 1
                    };
                    App.transBase.ActiveSession.Add(session);
                    App.transBase.SaveChanges();
                }
                App.loggedUser = user;
                App.menuFrame.Navigate(new ProfilePage());
            }
            else
            {
                MessageBox.Show("Вы ввели что-то неправильно","Ты наложал",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new EnterPage());
        }
    }
}
