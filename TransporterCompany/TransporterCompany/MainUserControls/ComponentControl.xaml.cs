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

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для ComponentControl.xaml
    /// </summary>
    public partial class ComponentControl : UserControl
    {
        public ComponentControl(Component component)
        {
            InitializeComponent();

            IdTb.Text = component.Id_Component.ToString();
            NameTb.Text = component.Name_Component.ToString();
        }
    }
}
