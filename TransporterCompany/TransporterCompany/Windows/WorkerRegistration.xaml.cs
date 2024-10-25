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
using TransporterCompany.MainDataBase;
using TransporterCompany.Pages;

namespace TransporterCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для WorkerRegistration.xaml
    /// </summary>
    public partial class WorkerRegistration : Window
    {
        List<CreatingProcess> currentSkills = new List<CreatingProcess>();

        public string _login;
        public string _password;
        public string _surname;
        public string _name;
        public string _patronymic;
        public int _idRole;
        public int _image;
        public WorkerRegistration(String login, String password, String surname, String name, String patronymic, int role, int image)
        {
            InitializeComponent();

            _login = login;
            _password = password;
            _surname = surname;
            _name = name;
            _patronymic = patronymic;
            _idRole = role;
            _image = image;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DateBorn.SelectedDate != null && CityTb.Text != "" && StreetTb.Text != "" && HouseTb.Text != "" &&
                EducationTb.Text != "" && SkillTb.Text != "")
            {
                if (ProcessSkills.Text != "")
                {
                    if (CheckSkills(ProcessSkills.Text) == true)
                    {
                        User newUser = new User()
                        {
                            Login = _login,
                            Password = _password,
                            Surname = _surname,
                            Name = _name,
                            Patronymic = _patronymic,
                            Id_Role = _idRole,
                            Id_Image = _image,
                        };
                        App.transBase.User.Add(newUser);
                        App.transBase.SaveChanges();
                        Worker newWorker = new Worker()
                        {
                            Login = _login,
                            DateBorn = DateBorn.SelectedDate,
                            Id_Address = AdressCheck(),
                            Education = EducationTb.Text,
                        };
                        App.transBase.Worker.Add(newWorker);
                        foreach (var process in currentSkills)
                        {
                            WorkerProcess workerProcess = new WorkerProcess()
                            {
                                Login_Worker = _login,
                                Name_Process = process.Process_Name,
                            };
                            App.transBase.WorkerProcess.Add(workerProcess);
                            App.transBase.SaveChanges();
                        }
                        App.transBase.SaveChanges();
                        this.Close();
                        App.menuFrame.Navigate(new ProfilePage());
                    }
                    else
                    {
                        MessageBox.Show("Заполните умения согласно шаблону");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните правильно ваши умения");
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
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


        SkillsInfo skillsInfo = new SkillsInfo();
        private void SkillsInfoBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            skillsInfo = new SkillsInfo();
            if (!skillsInfo.IsLoaded) skillsInfo.Show();

        }

        private void SkillsInfoBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            if (skillsInfo.IsLoaded) skillsInfo.Close();
        }

        public int AdressCheck()
        {

            foreach (Address address in App.transBase.Address)
            {
                if (address.City == CityTb.Text && address.Street == StreetTb.Text && address.HouseNumber == HouseTb.Text)
                {
                    return address.Id_Address;
                }
            }

            Address newAddress = new Address()
            {
                City = CityTb.Text,
                Street = StreetTb.Text,
                HouseNumber = HouseTb.Text,
            };
            App.transBase.Address.Add(newAddress);
            App.transBase.SaveChanges();
            return newAddress.Id_Address;
        }
    }
}
