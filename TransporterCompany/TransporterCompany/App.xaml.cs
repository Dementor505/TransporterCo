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
        public static TransporterBaseEntities8 transBase = new TransporterBaseEntities8();
        public static User loggedUser;
        public static MainButtons mainButtons;
        public static WrapPanel iconsPanel;
        public static WrapPanel materialPanel;
        public static WrapPanel componentPanel;
        public static WrapPanel workerPanel;
    }
}
