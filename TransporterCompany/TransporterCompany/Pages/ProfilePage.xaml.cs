using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            ImageStockUser imageStockUser = App.transBase.ImageStockUser.FirstOrDefault(x => x.Id_Image == App.loggedUser.Id_Image);
            Avatarka.Source = GetImage(imageStockUser.ImageSource);
            NameDataTb.Text = App.loggedUser.Name + " " + App.loggedUser.Surname + " " + App.loggedUser.Patronymic;
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
    }
}
