using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TransporterCompany.MainUserControls;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для MaterialPage.xaml
    /// </summary>
    public partial class MaterialPage : Page
    {
        public MaterialPage()
        {
            InitializeComponent();

            App.materialPanel = materialWp;

            foreach (var material in App.transBase.Material)
            {
                materialWp.Children.Add(new MaterialControl(material));
            }

            ObservableCollection<string> storageNames = new ObservableCollection<string>(App.transBase.Storage.Select(x => x.Name_Storage));
            storageNames.Insert(0, "Все склады");
            StorageCb.ItemsSource = storageNames;
            StorageCb.SelectedIndex = 0;
        }
        public void Refresh()
        {
            materialWp.Children.Clear();

            List<Material> materialList = App.transBase.Material.ToList();

            if(StorageCb.SelectedItem.ToString() != "Всё склады") materialList = materialList.Where(x => x.Id_Storage == StorageCb.SelectedIndex+1).ToList();

            foreach (var material in materialList)
            {
                materialWp.Children.Add(new MaterialControl(material));
            }
        }

        private void StorageCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AddEditMaterial(new Material()));
        }
    }
}
