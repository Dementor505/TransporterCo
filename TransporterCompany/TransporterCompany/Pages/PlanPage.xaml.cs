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
        public bool wasDeleted = false;
        public bool wasAdded = false;
        private Point _startPoint;
        private bool _isDragging;
        private UIElement _draggedElement;

        Image selectedImage;

        Workshop currentWorkshop;

        float size;

        List<UIElement> deleteList = new List<UIElement>();
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
                image.Tag = icon.Id_Icon;
            }
            WorkshopCb.ItemsSource = App.transBase.Workshop.Select(x => x.Name_Workshop).ToList();
            currentWorkshop = App.transBase.Workshop.FirstOrDefault(x => x.Name_Workshop == WorkshopCb.SelectedItem.ToString());
            LoadIcons(currentWorkshop);

            //foreach (UIElement element in MainCanvas.Children)
            //{
            //    element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            //    element.MouseMove += Element_MouseMove;
            //    element.MouseLeftButtonUp += Element_MouseLeftButtonUp;
            //    element.MouseRightButtonDown += Element_MouseRightButtonDown;
            //}
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

                DataObject data = new DataObject();
                data.SetData(DataFormats.Serializable, image.Source);
                data.SetData("ImageTag", image.Tag);

                DragDrop.DoDragDrop(image, data, DragDropEffects.Move);
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
            object tag = e.Data.GetData("ImageTag");
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
                        Stretch = Stretch.Fill,
                        Tag = tag
                    };
                    size = bitmap.PixelHeight / 3;
                    double left = dropPosition.X - newImage.Width / 2;
                    double top = dropPosition.Y - newImage.Height / 2;

                    if (left < 0) left = 0;
                    if (top < 0) top = 0;
                    if (left + newImage.Width > MainCanvas.Width) left = MainCanvas.Width - newImage.Width;
                    if (top + newImage.Height > MainCanvas.Height) top = MainCanvas.Height - newImage.Height;

                    Canvas.SetLeft(newImage, left);
                    Canvas.SetTop(newImage, top);
                    MainCanvas.Children.Add(newImage);

                    newImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                    newImage.MouseMove += Element_MouseMove;
                    newImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
                    newImage.MouseRightButtonDown += Element_MouseRightButtonDown;
                    wasAdded = true;
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

        private void Element_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                MainCanvas.Children.Remove(element);
                deleteList.Add(element);
                wasDeleted = true;
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
                    currentWorkshop = workshop;
                    LoadIcons(currentWorkshop);
                }
            }
            wasAdded = false;
            wasDeleted = false;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            int index = WorkshopCb.SelectedIndex;
            if (index == 0) WorkshopCb.SelectedIndex = index + 1;
            else if (index != 0) WorkshopCb.SelectedIndex = index - 1;
            WorkshopCb.SelectedIndex = index;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (wasDeleted == true || wasAdded == true)
            {
                foreach (UIElement icon in deleteList)
                {

                    //App.transBase.WorkshopIcons.Remove(icon);
                }
                App.transBase.SaveChanges();
                UIElementCollection iconsCanvas = MainCanvas.Children;
                //MessageBox.Show(MainCanvas.Children.Count.ToString());
                List<Icons> iconsList = App.transBase.Icons.ToList();

                foreach (UIElement iconElement in iconsCanvas)
                {
                    Image image = iconElement as Image;
                    if (image != null)
                    {
                        int? iconId = image.Tag as int?;
                        if (iconId.HasValue)
                        {
                            foreach (var item in iconsList)
                            {
                                if (iconId == item.Id_Icon)
                                {
                                    WorkshopIcons workshopIcons = new WorkshopIcons()
                                    {
                                        Id_Icon = item.Id_Icon,
                                        Id_Workshop = currentWorkshop.Id_Workshop,
                                        X_Position = Canvas.GetLeft(image),
                                        Y_Position = Canvas.GetTop(image),
                                        Width_Icon = size,
                                        Heigth_Icon = size,
                                    };
                                    App.transBase.WorkshopIcons.Add(workshopIcons);
                                    break;
                                }
                            }
                        }
                    }
                }
                App.transBase.SaveChanges();
            }
            wasAdded = false;
            wasDeleted = false;
        }

        public void LoadIcons(Workshop workshop)
        {
            MainCanvas.Children.Clear();
            foreach (WorkshopIcons icon in App.transBase.WorkshopIcons)
            {
                if (icon.Id_Workshop == workshop.Id_Workshop)
                {
                    Image iconImage = new Image();
                    iconImage.Source = GetImage(icon.Icons.Icon_Source);

                    TranslateTransform translateTransform = new TranslateTransform
                    {
                        X = Convert.ToDouble(icon.X_Position),
                        Y = Convert.ToDouble(icon.Y_Position)
                    };

                    iconImage.RenderTransform = translateTransform;
                    iconImage.Width = Convert.ToDouble(icon.Width_Icon);
                    iconImage.Height = Convert.ToDouble(icon.Heigth_Icon);

                    iconImage.MouseLeftButtonDown += Element_MouseLeftButtonDown;
                    iconImage.MouseMove += Element_MouseMove;
                    iconImage.MouseLeftButtonUp += Element_MouseLeftButtonUp;
                    iconImage.MouseRightButtonDown += Element_MouseRightButtonDown;

                    MainCanvas.Children.Add(iconImage);
                }
            }
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _startPoint = e.GetPosition(MainCanvas);
            _draggedElement = sender as UIElement;
            _draggedElement.CaptureMouse();
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedElement != null)
            {
                Point currentPosition = e.GetPosition(MainCanvas);
                double offsetX = currentPosition.X - _startPoint.X;
                double offsetY = currentPosition.Y - _startPoint.Y;

                double newLeft = Canvas.GetLeft(_draggedElement) + offsetX;
                double newTop = Canvas.GetTop(_draggedElement) + offsetY;

                if (newLeft < 0) newLeft = 0;
                if (newTop < 0) newTop = 0;
                if (newLeft + _draggedElement.RenderSize.Width > MainCanvas.ActualWidth) newLeft = MainCanvas.ActualWidth - _draggedElement.RenderSize.Width;
                if (newTop + _draggedElement.RenderSize.Height > MainCanvas.ActualHeight) newTop = MainCanvas.ActualHeight - _draggedElement.RenderSize.Height;

                Canvas.SetLeft(_draggedElement, newLeft);
                Canvas.SetTop(_draggedElement, newTop);

                _startPoint = currentPosition;
            }
        }

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging && _draggedElement != null)
            {
                _isDragging = false;
                _draggedElement.ReleaseMouseCapture();
                _draggedElement = null;
            }
        }
    }
}