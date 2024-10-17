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
using TransporterCompany.Pages;

namespace TransporterCompany.UserButtons
{
    /// <summary>
    /// Логика взаимодействия для ExitAccountBtn.xaml
    /// </summary>
    public partial class ExitAccountBtn : UserControl
    {
        public ExitAccountBtn()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ActiveSession session = App.transBase.ActiveSession.FirstOrDefault(x => x.Computer_Number == 1 && x.Login_User != null);
            App.loggedUser = null;
            if(session != null) App.transBase.ActiveSession.Remove(session);
            App.transBase.SaveChanges();
            App.menuFrame.Navigate(new EnterPage());
        }
    }
}
