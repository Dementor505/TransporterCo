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
    /// Логика взаимодействия для ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : Page
    {
        public ComponentPage()
        {
            InitializeComponent();

            foreach (var component in App.transBase.Component)
            {
                componentWp.Children.Add(new ComponentControl(component));
            }

            ObservableCollection<string> storageNames = new ObservableCollection<string>(App.transBase.Storage.Select(x => x.Name_Storage));
            storageNames.Insert(0, "Все склады");
            StorageCb.ItemsSource = storageNames;
            StorageCb.SelectedIndex = 0;
        }
        public void Refresh()
        {
            componentWp.Children.Clear();

            List<Component> componentList = App.transBase.Component.ToList();

            componentList = componentList.Where(x => x.Id_Storage == StorageCb.SelectedIndex + 1).ToList();

            foreach (var component in componentList)
            {
                componentWp.Children.Add(new ComponentControl(component));
            }
        }

        private void StorageCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
