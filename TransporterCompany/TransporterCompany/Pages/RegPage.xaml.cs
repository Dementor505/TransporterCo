using Microsoft.Win32;
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
using TransporterCompany.Windows;
using System.IO;
using static System.Net.WebRequestMethods;
using TransporterCompany.MainDataBase;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public ImageStockUser addedImage;
        public RegPage()
        {
            InitializeComponent();
            App.mainButtons.ClearWrapPanel();
            RoleCb.ItemsSource = App.transBase.Role.Select(x => x.Name_Role).ToList();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] testImage = new byte[] { };
            if (LoginTb.Text != "" && PasswordTb.Text != "" && SurnameTb.Text != "" &&
                NameTb.Text != "" && PatronymicTb.Text != "" && IndicatorImage.Fill == Brushes.Green)
            {
                string pattern = @"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d^(*&{}|+)]{4,16}$";

                if (Regex.IsMatch(PasswordTb.Text, pattern))
                {
                    if (!App.transBase.User.Any(x => x.Login == LoginTb.Text))
                    {
                        if (RoleCb.SelectedIndex == 0)
                        {
                            User newUser = new User()
                            {
                                Login = LoginTb.Text,
                                Password = PasswordTb.Text,
                                Surname = SurnameTb.Text,
                                Name = NameTb.Text,
                                Patronymic = PatronymicTb.Text,
                                Id_Role = RoleCb.SelectedIndex + 1,
                                Id_Image = addedImage.Id_Image,
                            };
                            App.transBase.User.Add(newUser);
                            App.transBase.SaveChanges();
                            App.loggedUser = newUser;
                            App.menuFrame.Navigate(new ProfilePage());
                        }
                        else
                        {
                            WorkerRegistration workerRegistration = new WorkerRegistration(LoginTb.Text, PasswordTb.Text, SurnameTb.Text, NameTb.Text, PatronymicTb.Text, RoleCb.SelectedIndex + 1, addedImage.Id_Image);
                            workerRegistration.ShowDialog();

                        }
                    }
                    else MessageBox.Show("Такой пользователь уже есть");
                }
                else
                {
                    MessageBox.Show("Пароль должен содержать от 4 до 16 символов. С заглавной буквой. С цифрой. и без спец.знаков.");
                }
            }
        }

        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            byte[] currentImage;
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png.|*jpg.|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                currentImage = System.IO.File.ReadAllBytes(openFile.FileName);
                ImageStockUser imageStockUser = new ImageStockUser()
                {
                    ImageSource = currentImage,
                };
                if (!App.transBase.ImageStockUser.Any(x => x.ImageSource == currentImage))
                {
                    App.transBase.ImageStockUser.Add(imageStockUser);
                    App.transBase.SaveChanges();
                    addedImage = imageStockUser;
                }
                else
                {
                    addedImage = App.transBase.ImageStockUser.FirstOrDefault(x => x.ImageSource == currentImage);
                }
                IndicatorImage.Fill = Brushes.Green;
            }
        }
        public void Aaa()
        {
        }
    }
}
