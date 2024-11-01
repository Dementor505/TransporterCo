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
using TransporterCompany.MainDataBase;

namespace TransporterCompany.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddDocsWindow.xaml
    /// </summary>
    public partial class AddDocsWindow : Window
    {
        byte[] _data;
        public AddDocsWindow(byte[] data)
        {
            InitializeComponent();
            _data = data;
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            App.documentsList.Add(_data);
            App.docTypeList.Add(TypeTb.Text);
            App.docNameList.Add(NameTb.Text);
            this.Close();
        }
    }
}
