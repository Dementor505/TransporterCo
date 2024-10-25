using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TransporterCompany.MainDataBase;

namespace TransporterCompany
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Frame menuFrame;
        public static TransporterBaseEntities4 transBase = new TransporterBaseEntities4();
        public static User loggedUser;
        public static MainButtons mainButtons;
    }
}
