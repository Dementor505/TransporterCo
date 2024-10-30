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

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для MaterialControl.xaml
    /// </summary>
    public partial class MaterialControl : UserControl
    {
        public MaterialControl(Material material)
        {
            InitializeComponent();

            IdTb.Text = material.Id_Material;
            NameTb.Text = material.Name_Material;
            if (material.ImageStockMaterial.ImageSource != null) materialImage.Source = GetImage(material.ImageStockMaterial.ImageSource);
            if (material.ImageStockMaterial.ImageSource == null) materialImage.Source = new BitmapImage(new Uri(@"\Resources\NoPhotoNew.png", UriKind.Relative)); ;
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
    }
}
