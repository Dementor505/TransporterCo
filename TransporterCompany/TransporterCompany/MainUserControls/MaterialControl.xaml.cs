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
    /// Логика взаимодействия для MaterialControl.xaml
    /// </summary>
    public partial class MaterialControl : UserControl
    {
        Material _material;
        public MaterialControl(Material material)
        {
            InitializeComponent();
            _material = material;

            IdTb.Text = material.Id_Material;
            NameTb.Text = material.Name_Material;
            if (material.ImageStockMaterial.ImageSource != null) materialImage.Source = GetImage(material.ImageStockMaterial.ImageSource);
            if (material.ImageStockMaterial.ImageSource == null) materialImage.Source = new BitmapImage(new Uri(@"\Resources\NoPhotoNew.png", UriKind.Relative));

            string nameProvider = App.transBase.Provider.Where(z => z.Id_Provider == material.Id_Provider).Select(z => z.Name_Provider).FirstOrDefault();

            ProviderTb.Text = nameProvider;
            CountTb.Text = "Кол-вл " + material.Count.ToString();
            UnitTb.Text = "Ед-изм " + material.SizeType.Name_SizeType;
            DateTb.Text = material.DeliveryDate.ToString();
            costTb.Text = material.Cost_Material.ToString() + ".р";
            StorageS.Text = material.Id_Storage.ToString();

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

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (App.loggedUser.Id_Role == 2 || App.loggedUser.Id_Role == 3)
            {
                //MessageBox.Show("click");
                ChoiseMaterial choiseMaterial = new ChoiseMaterial(_material);
                Point cursorPosition = Mouse.GetPosition(this);
                Point screenPosition = this.PointToScreen(cursorPosition);
                choiseMaterial.Left = screenPosition.X - choiseMaterial.Width / 2;
                choiseMaterial.Top = screenPosition.Y - choiseMaterial.Height / 2;
                if (!choiseMaterial.IsLoaded) choiseMaterial.Show();
            }
        }
    }
}
