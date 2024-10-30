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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransporterCompany.MainDataBase;
using TransporterCompany.MainUserControls;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для IconsPage.xaml
    /// </summary>
    public partial class IconsPage : Page
    {
        public IconsPage()
        {
            InitializeComponent();
            App.iconsPanel = IconWp;

            foreach(Icons icon in App.transBase.Icons)
            {
                IconWp.Children.Add(new IconControl(icon));
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AddIconPage());
        }
    }
}
