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

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для DocsControls.xaml
    /// </summary>
    public partial class DocsControls : UserControl
    {
        public DocsControls(string docType)
        {
            InitializeComponent();

            if (docType == "image")
            {
                ImageDoc.Source = new BitmapImage(new Uri("/Resources/Imgs.png",UriKind.Relative));
            }
            if (docType == "text")
            {
                ImageDoc.Source = new BitmapImage(new Uri("/Resources/Docs.png",UriKind.Relative));
            }
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void UserControl_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
