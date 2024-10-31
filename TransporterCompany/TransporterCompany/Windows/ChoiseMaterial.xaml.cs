using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TransporterCompany.MainDataBase;
using TransporterCompany.MainUserControls;
using TransporterCompany.Pages;

namespace TransporterCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChoiseMaterial.xaml
    /// </summary>
    public partial class ChoiseMaterial : Window
    {
        Material _material;
        public ChoiseMaterial(Material material)
        {
            InitializeComponent();
            _material = material;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AddEditMaterial(_material));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            App.transBase.Material.Remove(_material);
            App.transBase.SaveChanges();
            App.materialPanel.Children.Clear();
            foreach (var material in App.transBase.Material)
            {
                App.materialPanel.Children.Add(new MaterialControl(material));
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            Point cursorPosition = e.GetPosition(this);
            Rect windowRect = new Rect(0, 0, this.ActualWidth, this.ActualHeight);

            if (windowRect.Contains(cursorPosition))
            {
            }
            else
            {
                this.Close();
            }
        }
    }
}
