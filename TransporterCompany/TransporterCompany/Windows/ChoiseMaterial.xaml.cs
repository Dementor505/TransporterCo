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
        bool isDelete = false;
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
            isDelete = true;
            MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.transBase.Material.Remove(_material);
                App.transBase.SaveChanges();
                App.materialPanel.Children.Clear();
                foreach (var material in App.transBase.Material)
                {
                    App.materialPanel.Children.Add(new MaterialControl(material));
                }
                isDelete = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ну как знаешь =)");
                isDelete = false;
                this.Close();
            }
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDelete == false) this.Close();
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
