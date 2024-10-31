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
using TransporterCompany.Pages;

namespace TransporterCompany.UserButtons
{
    /// <summary>
    /// Логика взаимодействия для OrderBtn.xaml
    /// </summary>
    public partial class OrderBtn : UserControl
    {
        public OrderBtn()
        {
            InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new OrderPage());
        }
    }
}
