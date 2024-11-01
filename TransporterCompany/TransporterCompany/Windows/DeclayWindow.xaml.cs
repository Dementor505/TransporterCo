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
    /// Логика взаимодействия для DeclayWindow.xaml
    /// </summary>
    public partial class DeclayWindow : Window
    {
        public DeclayWindow()
        {
            InitializeComponent();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            App.declayText = declayWords.Text;
            this.Close();
        }
    }
}
