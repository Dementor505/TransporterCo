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
    /// Логика взаимодействия для IconControl.xaml
    /// </summary>
    public partial class IconControl : UserControl
    {
        Icons _icon;
        public IconControl(Icons icon)
        {
            InitializeComponent();
            _icon = icon;

            NameTb.Text = icon.Name_Icon.ToString();
            IconImage.Source = GetImage(icon.Icon_Source);
            if (App.transBase.WorkshopIcons.FirstOrDefault(x => x.Id_Icon == icon.Id_Icon) == null)
            {
                UsingTb.Foreground = Brushes.Red;
                UsingTb.Text = "НЕ ИСПОЛЬЗУЕТСЯ";
            }
            else
            {
                UsingTb.Foreground = Brushes.White;
                UsingTb.Text = "ИСПОЛЬЗУЕТСЯ";
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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            App.transBase.Icons.Remove(_icon);
            App.transBase.SaveChanges();
            App.iconsPanel.Children.Clear();
            foreach (Icons icon in App.transBase.Icons)
            {
                App.iconsPanel.Children.Add(new IconControl(icon));
            }

            foreach (WorkshopIcons workshopIcon in App.transBase.WorkshopIcons)
            {
                if (workshopIcon.Id_Icon == _icon.Id_Icon) App.transBase.WorkshopIcons.Remove(workshopIcon);
            }
            App.transBase.SaveChanges();    
        }
    }
}
