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
        }
    }
}
