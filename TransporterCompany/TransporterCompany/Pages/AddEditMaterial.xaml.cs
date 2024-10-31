using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using TransporterCompany.Pages;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditMaterial.xaml
    /// </summary>
    public partial class AddEditMaterial : Page
    {
        Material _material;
        public AddEditMaterial(Material material)
        {
            InitializeComponent();
            _material = material;

            DataContext = material;

            TypeMaterialCb.ItemsSource = App.transBase.MaterialType.Select(x => x.Name_MaterialType).ToList();
            TypeMaterialCb.SelectedItem = App.transBase.MaterialType.FirstOrDefault(x => x.Id_MaterialType == material.Id_TypeMaterial).Name_MaterialType;

            StandartCb.ItemsSource = App.transBase.Standart.Select(x => x.Name_Standart).ToList();
            StandartCb.SelectedItem = App.transBase.Standart.FirstOrDefault(x => x.Id_Standart == material.Id_Standart).Name_Standart;

            ProviderCb.ItemsSource = App.transBase.Provider.Select(x => x.Name_Provider).ToList();
            ProviderCb.SelectedItem = App.transBase.Provider.FirstOrDefault(x => x.Id_Provider == material.Id_Provider).Name_Provider;

            UnitTypeCb.ItemsSource = App.transBase.SizeType.Select(x => x.Name_SizeType).ToList();
            UnitTypeCb.SelectedItem = App.transBase.SizeType.FirstOrDefault(x => x.Id_SizeType == material.Id_SizeType).Name_SizeType;

            DeliveryDateDp.SelectedDate = material.DeliveryDate;

            StorageCb.ItemsSource = App.transBase.Storage.Select(x => x.Name_Storage).ToList();
            StorageCb.SelectedItem = App.transBase.Storage.FirstOrDefault(x => x.Id_Storage == material.Id_Storage).Name_Storage;

            materialImage.Source = GetImage(material.ImageStockMaterial.ImageSource);


            if (float.TryParse(CostTb.Text, out float result))
            {
                CostTb.Text = result.ToString();
            }
            else
            {
                CostTb.Text = material.Cost_Material.ToString();
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
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new MaterialPage());
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
                materialImage.Source = GetImage(currentImage);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            materialImage.Source = new BitmapImage(new Uri("/Resources/NoPhotoNew.png", UriKind.Relative));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NumberTb.Text != "" && NameTb.Text != "" && UnitTypeCb.SelectedItem != null && CountTb.Text != "" &&
                ProviderCb.SelectedItem != null && materialImage.Source != null && TypeMaterialCb.SelectedItem != null &&
                CostTb.Text != "" && LengthTb.Text != "" && MassTb.Text != "" && StandartCb.SelectedItem != null &&
                DeliveryDateDp.SelectedDate != null && StorageCb.SelectedItem != null)
            {
                if (_material == null)
                {
                    Material newMaterial = new Material()
                    {
                        Name_Material = NameTb.Text,
                        Id_SizeType = App.transBase.SizeType.FirstOrDefault(x => x.Name_SizeType == UnitTypeCb.SelectedItem.ToString()).Id_SizeType,
                        Count = Convert.ToInt32(CountTb.Text)
                    };
                    App.transBase.Material.Add(newMaterial);

                }
                else
                {
                    _material.Name_Material = NameTb.Text;
                    _material.Id_SizeType = App.transBase.SizeType.FirstOrDefault(x => x.Name_SizeType == UnitTypeCb.SelectedItem.ToString()).Id_SizeType;
                    _material.Count = Convert.ToInt32(CountTb.Text);
                    _material.Id_Provider = App.transBase.Provider.FirstOrDefault(x => x.Name_Provider == ProviderCb.SelectedItem.ToString()).Id_Provider;
                    //_material.Id_Image = App.transBase.ImageStockMaterial.FirstOrDefault(x => x.ImageSource == ImageToByteArray(materialImage)).Id_Image;
                    _material.Id_TypeMaterial = App.transBase.MaterialType.FirstOrDefault(x => x.Name_MaterialType == TypeMaterialCb.SelectedItem.ToString()).Id_MaterialType;

                    if (float.TryParse(CostTb.Text, out float result))
                    {
                        _material.Cost_Material = result;
                    }
                    _material.Length_Material = Convert.ToDouble(LengthTb.Text);
                    _material.Mass_Material = Convert.ToDouble(MassTb.Text);
                    _material.Id_Standart = App.transBase.Standart.FirstOrDefault(x => x.Name_Standart == StandartCb.SelectedItem.ToString()).Id_Standart;
                    _material.DeliveryDate = DeliveryDateDp.SelectedDate;
                    _material.Id_Storage = App.transBase.Storage.FirstOrDefault(x => x.Name_Storage == StorageCb.SelectedItem.ToString()).Id_Storage;
                    App.transBase.SaveChanges();
                    App.menuFrame.Navigate(new MaterialPage());
                }
            }
        }
        public static byte[] ImageToByteArray(Image image)
        {
            // Получаем BitmapSource из Image.Source
            BitmapSource bitmapSource = image.Source as BitmapSource;

            if (bitmapSource == null)
            {
                throw new ArgumentException("Image.Source is not a BitmapSource");
            }

            // Создаем объект JpegBitmapEncoder для кодирования изображения в JPEG формат
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            // Создаем MemoryStream для хранения байтов
            using (MemoryStream ms = new MemoryStream())
            {
                // Сохраняем изображение в MemoryStream
                encoder.Save(ms);
                return ms.ToArray();
            }
        }
    }
}
