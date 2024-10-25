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

namespace TransporterCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для SkillsInfo.xaml
    /// </summary>
    public partial class SkillsInfo : Window
    {
        public SkillsInfo()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            this.Left = (screenWidth / 2) - (windowWidth / 2) + 470;
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            mainListView.ItemsSource = App.transBase.CreatingProcess.ToList();
        }
    }
}
