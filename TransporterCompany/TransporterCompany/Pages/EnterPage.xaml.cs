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
using WpfAnimatedGif;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для EnterPage.xaml
    /// </summary>
    public partial class EnterPage : Page
    {
        public EnterPage()
        {
            InitializeComponent();
            App.mainButtons.ClearWrapPanel();
            ImageBehavior.SetAnimatedSource(ImageConveer, new BitmapImage(new Uri("/Resources/Conveer_Basic.gif",UriKind.Relative)));
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AuthPage());
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new RegPage());
        }
    }
}
