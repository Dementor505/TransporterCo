using Microsoft.Win32;
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
using TransporterCompany.Windows;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditOrder.xaml
    /// </summary>
    public partial class AddEditOrder : Page
    {
        public Order _order;
        public AddEditOrder(Order order)
        {
            InitializeComponent();

            App.docsPanel = DocsWp;
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "Text files (*.txt;*.doc;*.docx)|*.txt;*.doc;*.docx|Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                byte[] fileData = System.IO.File.ReadAllBytes(openFile.FileName);

                AddDocsWindow addDocsWindow = new AddDocsWindow(fileData);
                double screenWidth = SystemParameters.PrimaryScreenWidth;
                double screenHeight = SystemParameters.PrimaryScreenHeight;
                double windowWidth = addDocsWindow.Width;
                double windowHeight = addDocsWindow.Height;
                addDocsWindow.Left = (screenWidth / 2) - (windowWidth / 2);
                addDocsWindow.Top = (screenHeight / 2) - (windowHeight / 2);

                if (openFile.FilterIndex == 1)
                {
                    DocsWp.Children.Add(new DocsControls("text"));
                    addDocsWindow.ShowDialog();
                }
                if (openFile.FilterIndex == 2)
                {
                    DocsWp.Children.Add(new DocsControls("image"));
                    addDocsWindow.ShowDialog();
                }
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Save2Btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load2Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void resetAllDocsBtn_Click(object sender, RoutedEventArgs e)
        {
            DocsWp.Children.Clear();
            App.documentsList.Clear();
            App.docNameList.Clear();
            App.docTypeList.Clear();
        }
    }
}
