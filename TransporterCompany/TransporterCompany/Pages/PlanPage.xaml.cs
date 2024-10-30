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
using System.Windows.Threading;
using TransporterCompany.MainDataBase;
using System.Windows.Media.Imaging;

namespace TransporterCompany.Pages
{
    public partial class PlanPage : Page
    {
        private Point _startPoint;
        private bool _isDragging;

        Image selectedImage;
        public PlanPage()
        {
            InitializeComponent();

            //OpenFileDialog openFile = new OpenFileDialog()
            //{
            //    Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*"
            //};
            //if (openFile.ShowDialog().GetValueOrDefault())
            //{
            //    byte[] currentImage = null;
            //    currentImage = System.IO.File.ReadAllBytes(openFile.FileName);
            //    ImageStockUser icon = new ImageStockUser()
            //    {
            //        ImageSource = currentImage,
            //    };
            //    Workshop workshop = new Workshop()
            //    {
            //        Id_Workshop = 5,
            //        Name_Workshop = "Цех",
            //        Workshop_Image = currentImage,
            //    };
            //    App.transBase.Workshop.Add(workshop);
            //    App.transBase.SaveChanges();
            //}


            foreach (Icons icon in App.transBase.Icons)
            {
                Image image = new Image();
                image.Source = GetImage(icon.Icon_Source);
                image.Width = 150;
                image.Height = 150;
                image.Margin = new Thickness(10);
                image.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(Png_MouseMove), true);
                image.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(Png_MouseDown), true);
                IconsWp.Children.Add(image);
            }

            WorkshopCb.ItemsSource = App.transBase.Workshop.Select(x => x.Name_Workshop).ToList();
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

        private void Png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            selectedImage = image;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _startPoint = e.GetPosition(MainCanvas);
                _isDragging = true;
                selectedImage.CaptureMouse();
            }
        }

        private void Png_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image draggedImage = sender as Image;
                if (draggedImage != null)
                {
                    BitmapImage bitmap = draggedImage.Source as BitmapImage;
                    if (bitmap != null)
                    {
                        DataObject data = new DataObject(DataFormats.Serializable, bitmap);
                        DragDrop.DoDragDrop(draggedImage, data, DragDropEffects.Move);
                    }
                }
            }
        }

        private void MainCanvas_Drop(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(MainCanvas);
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data != null)
            {
                BitmapImage bitmap = data as BitmapImage;
                if (bitmap != null)
                {
                    Image newImage = new Image
                    {
                        Source = bitmap,
                        Width = bitmap.PixelWidth / 3,
                        Height = bitmap.PixelHeight / 3,
                        Stretch = Stretch.Fill
                    };

                    double left = dropPosition.X - newImage.Width / 2;
                    double top = dropPosition.Y - newImage.Height / 2;

                    if (left < 0) left = 0;
                    if (top < 0) top = 0;
                    if (left + newImage.Width > MainCanvas.Width) left = MainCanvas.Width - newImage.Width;
                    if (top + newImage.Height > MainCanvas.Height) top = MainCanvas.Height - newImage.Height;

                    Canvas.SetLeft(newImage, left);
                    Canvas.SetTop(newImage, top);
                    MainCanvas.Children.Add(newImage);
                }
                else
                {
                    MessageBox.Show("BitmapImage is null");
                }
            }
            else
            {
                MessageBox.Show("Data is null");
            }
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(MainCanvas);
            var hitElement = MainCanvas.InputHitTest(clickPosition) as DependencyObject;
            if (hitElement is UIElement element)
            {
                MainCanvas.Children.Remove(element);
            }
        }

        private void WorkshopCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Workshop workshop in App.transBase.Workshop)
            {
                if (workshop.Name_Workshop == WorkshopCb.SelectedItem.ToString())
                {
                    WorkshopImage.Source = GetImage(workshop.Workshop_Image);
                    BitmapImage bitmapImage = GetImage(workshop.Workshop_Image);
                }
            }
        }
    }
}