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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransporterCompany.MainDataBase;

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для ComponentControl.xaml
    /// </summary>
    public partial class ComponentControl : UserControl
    {
        public ComponentControl(Component component)
        {
            InitializeComponent();

            IdTb.Text = component.Id_Component.ToString();
            NameTb.Text = component.Name_Component.ToString();

            if (component.ImageStockComponent.ImageSource != null) componentImage.Source = GetImage(component.ImageStockComponent.ImageSource);
            if (component.ImageStockComponent.ImageSource == null) componentImage.Source = new BitmapImage(new Uri(@"\Resources\NoPhotoNew.png", UriKind.Relative));

            string nameProvider = App.transBase.Provider.Where(z => z.Id_Provider == component.Id_Provider).Select(z => z.Name_Provider).FirstOrDefault();

            ProviderTb.Text = nameProvider;
            CountTb.Text = "Кол-вл " + component.Count.ToString();

            string sizeTypeString = App.transBase.SizeType.FirstOrDefault(x => x.Id_SizeType == component.Id_SizeType).Name_SizeType;
            UnitTb.Text = "Ед-изм " + sizeTypeString;
            //DateTb.Text = material.DeliveryDate.ToString();
            costTb.Text = component.Cost.ToString() + ".р";

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
