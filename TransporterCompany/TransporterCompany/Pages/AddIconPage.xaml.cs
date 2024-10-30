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

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddIconPage.xaml
    /// </summary>
    public partial class AddIconPage : Page
    {
        byte[] currentImage;
        public AddIconPage()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IconName.Text != "" && currentImage != null)
            {
                Icons newIcon = new Icons()
                {
                    Icon_Source = currentImage,
                    Name_Icon = IconName.Text,
                };
                App.transBase.Icons.Add(newIcon);
                App.transBase.SaveChanges();
                App.menuFrame.Navigate(new IconsPage());
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new IconsPage());
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                currentImage = System.IO.File.ReadAllBytes(openFile.FileName);
                ImageStockUser icon = new ImageStockUser()
                {
                    ImageSource = currentImage,
                };
                IconImage.Source = GetImage(currentImage);
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
    }
}
