using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using TransporterCompany.Windows;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditWorker.xaml
    /// </summary>
    public partial class AddEditWorker : Page
    {
        public Worker _worker;
        public byte[] newImagePath;
        public Address newAddress2;
        public bool itsAdd = false;
        List<CreatingProcess> currentSkills = new List<CreatingProcess>();
        public AddEditWorker(Worker worker)
        {
            InitializeComponent();
            _worker = worker;
            DataContext = worker;
            RoleCb.ItemsSource = App.transBase.Role.Select(x => x.Name_Role).ToList();
            if (worker.Login == null) itsAdd = true;
            if (itsAdd == true) LoginTb.IsReadOnly = false;
            if (itsAdd == false) RoleCb.SelectedIndex = _worker.User.Id_Role - 1;
            if (itsAdd == false)
            {
                ImageStockUser imageStockUser = App.transBase.ImageStockUser.FirstOrDefault(x => x.Id_Image == worker.User.Id_Image);
                profileImage.Source = GetImage(imageStockUser.ImageSource);
            };

            if (itsAdd == false)
            {
                foreach (WorkerProcess skill in App.transBase.WorkerProcess)
                {
                    if (skill.Login_Worker == _worker.Login) SkillsTb.Text += skill.CreatingProcess.Process_Name + ", ";
                }
            }
        }
        public BitmapImage GetImage(byte[] byteImage)
        {
            if (byteImage != null)
            {
                MemoryStream byteStream = new MemoryStream(byteImage);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.EndInit();
                return image;
            }
            else
                return new BitmapImage(new Uri(@"\Resources\NoPhotoNew.png", UriKind.Relative));
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] currentImage;
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                currentImage = System.IO.File.ReadAllBytes(openFile.FileName);
                profileImage.Source = GetImage(currentImage);
                newImagePath = currentImage;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            profileImage.Source = new BitmapImage(new Uri("/Resources/NoPhotoNew.png", UriKind.Relative));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new WorkerPage());
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (itsAdd == true) _worker = new Worker();

            bool addressUpdated = false;
            foreach (Address address in App.transBase.Address)
            {
                if (address.City == CityTb.Text && address.Street == StreetTb.Text && address.HouseNumber == HouseNumberTb.Text)
                {
                    _worker.Id_Address = address.Id_Address;
                    addressUpdated = true;
                }
            }
            App.transBase.SaveChanges();

            if (addressUpdated == false)
            {
                MessageBox.Show("новый адресс");
                Address newAddress = new Address()
                {
                    City = CityTb.Text,
                    Street = StreetTb.Text,
                    HouseNumber = HouseNumberTb.Text,
                };
                App.transBase.Address.Add(newAddress);
                App.transBase.SaveChanges();
                _worker.Id_Address = newAddress.Id_Address;
                newAddress2 = newAddress;
                App.transBase.SaveChanges();
            }

            ImageStockUser newImage = new ImageStockUser()
            {
                ImageSource = newImagePath,
            };
            App.transBase.ImageStockUser.Add(newImage);
            App.transBase.SaveChanges();

            if (LoginTb.Text != "" && PasswordTb.Text != "" && SurnameTb.Text != "" && NameTb.Text != "" &&
                PatronymicTb.Text != "" && EducationTb.Text != "" && DateBornDp.SelectedDate != null &&
                CityTb.Text != "" && StreetTb.Text != "" && HouseNumberTb.Text != "" && profileImage.Source != null &&
                RoleCb.SelectedItem != null)
            {
                if (CheckSkills(SkillsTb.Text) == true && SkillsTb.Text != "")
                {
                    if (itsAdd == false)
                    {
                        _worker.User.Password = PasswordTb.Text;
                        _worker.User.Surname = SurnameTb.Text;
                        _worker.User.Name = NameTb.Text;
                        _worker.User.Patronymic = PatronymicTb.Text;
                        _worker.DateBorn = DateBornDp.SelectedDate;
                        _worker.Education = EducationTb.Text;
                        _worker.User.Id_Image = newImage.Id_Image;
                        App.transBase.SaveChanges();

                        foreach (var process in currentSkills)
                        {
                            WorkerProcess workerProcess = new WorkerProcess()
                            {
                                Login_Worker = _worker.Login,
                                Name_Process = process.Process_Name,
                            };
                            foreach (var skill in App.transBase.WorkerProcess)
                            {
                                if (skill.Name_Process == process.Process_Name && skill.Login_Worker == _worker.Login)
                                {

                                }
                                else if (process.Process_Name == skill.Name_Process)
                                {
                                    App.transBase.WorkerProcess.Add(workerProcess);
                                }
                            }
                            App.transBase.SaveChanges();
                        }
                    }
                    else
                    {
                        User newUser = new User()
                        {
                            Login = LoginTb.Text,
                            Password = PasswordTb.Text,
                            Surname = SurnameTb.Text,
                            Name = NameTb.Text,
                            Patronymic = PatronymicTb.Text,
                            Id_Role = RoleCb.SelectedIndex + 1,
                            Id_Image = newImage.Id_Image,
                        };
                        App.transBase.User.Add(newUser);
                        App.transBase.SaveChanges();
                        Worker newWorker = new Worker()
                        {
                            Login = newUser.Login,
                            DateBorn = DateBornDp.SelectedDate,
                            Id_Address = _worker.Id_Address,
                            Education = EducationTb.Text,
                        };
                        App.transBase.Worker.Add(newWorker);
                        App.transBase.SaveChanges();
                        foreach (var process in currentSkills)
                        {
                            WorkerProcess workerProcess = new WorkerProcess()
                            {
                                Login_Worker = newWorker.Login,
                                Name_Process = process.Process_Name,
                            };
                            App.transBase.WorkerProcess.Add(workerProcess);
                        }
                        App.transBase.SaveChanges();
                    }
                    App.menuFrame.Navigate(new WorkerPage());
                }
                else
                {
                    MessageBox.Show("Заполни умения правильно, через запятую и из представленного списка");
                }
            }
            else
            {
                MessageBox.Show("Не все данные заполнены, проверь и дозаполни");
            }
        }

        SkillsInfo skillsInfo = new SkillsInfo();
        private void SkillsBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            skillsInfo = new SkillsInfo();
            if (!skillsInfo.IsLoaded) skillsInfo.Show();
            skillsInfo.Top -= 100;
        }
        private void SkillsBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (skillsInfo.IsLoaded) skillsInfo.Close();
        }
        public bool CheckSkills(string skillsString)
        {
            List<String> requiredSkills = App.transBase.CreatingProcess.Select(x => x.Process_Name).ToList();
            List<String> enteredSkills = skillsString.Split(',').Select(s => s.Trim()).ToList();

            foreach (var skill in enteredSkills)
            {
                if (!requiredSkills.Contains(skill))
                {
                    MessageBox.Show($"Умение '{skill}' не найдено в списке обязательных умений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            currentSkills = TakeSkills(enteredSkills);
            return true;
        }
        public List<CreatingProcess> TakeSkills(List<String> skillsString)
        {
            List<CreatingProcess> creatingProcesses = new List<CreatingProcess>();
            foreach (var skillString in skillsString)
            {
                foreach (CreatingProcess skill in App.transBase.CreatingProcess)
                {
                    if (skillString == skill.Process_Name)
                    {
                        creatingProcesses.Add(skill);
                    }
                }
            }
            return creatingProcesses;
        }
    }
}
