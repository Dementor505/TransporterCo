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

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            App.menuFrame = MenuFrame;
            MainButtons mainButtons = new MainButtons();
            mainButtons.mainWrapPanel = mainWrapPanel;
            App.mainButtons = mainButtons;

            if (CheckSessions() != null)
            {
                ActiveSession user = CheckSessions();
                App.loggedUser = App.transBase.User.Where(x => x.Login == user.Login_User).FirstOrDefault();
                App.menuFrame.Navigate(new ProfilePage());
                App.mainButtons.RefreshButtons(App.loggedUser.Id_Role);
            }
            else
            {
                App.menuFrame.Navigate(new EnterPage());
            }

            ScrollMenu.ScrollToVerticalOffset(350);
        }
        public ActiveSession CheckSessions()
        {
            ActiveSession session = App.transBase.ActiveSession.FirstOrDefault(x => x.Computer_Number == 1 && x.Login_User != null);
            if (session != null) return session;
            return null;
        }
    }
}
