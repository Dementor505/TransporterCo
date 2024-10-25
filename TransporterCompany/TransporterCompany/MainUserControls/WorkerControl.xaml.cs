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

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для WorkerControl.xaml
    /// </summary>
    public partial class WorkerControl : UserControl
    {
        public Worker _worker;
        public WorkerControl(Worker worker)
        {
            InitializeComponent();

            _worker = worker;

            ImageStockUser imageStockUser = App.transBase.ImageStockUser.FirstOrDefault(x => x.Id_Image == worker.User.Id_Image);
            workerImage.Source = GetImage(imageStockUser.ImageSource);
            workerImage2.Source = GetImage(imageStockUser.ImageSource);
            NameTb.Text = worker.User.Name;
            RoleTb.Text = worker.User.Role.Name_Role;
            Name2Tb.Text = worker.User.Name;
            SurnameTb.Text = worker.User.Surname;
            PatronymicTb.Text = worker.User.Patronymic;
            DateTb.Text = worker.DateBorn.ToString();
            EducationTb.Text = worker.Education;
            SkillsTb.Text = "";
            foreach (var skill in App.transBase.WorkerProcess.Where(x => x.Login_Worker == worker.Login))
            {
                SkillsTb.Text += skill.Name_Process + "   ";
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

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FirstLayer.Visibility = Visibility.Hidden;
            SecondLayer.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            FirstLayer.Visibility = Visibility.Visible;
            SecondLayer.Visibility = Visibility.Hidden;
        }

        //ChoiseWorker choiseWorker = new ChoiseWorker();
        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChoiseWorker choiseWorker = new ChoiseWorker(_worker);
            Point cursorPosition = Mouse.GetPosition(this);
            Point screenPosition = this.PointToScreen(cursorPosition);
            choiseWorker.Left = screenPosition.X - choiseWorker.Width/2;
            choiseWorker.Top = screenPosition.Y - choiseWorker.Height/2;
            if (!choiseWorker.IsLoaded) choiseWorker.Show();
        }
    }
}
