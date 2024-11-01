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
        public static TransporterBaseEntities9 transBase = new TransporterBaseEntities9();
        public static User loggedUser;
        public static MainButtons mainButtons;
        public static WrapPanel iconsPanel;
        public static WrapPanel materialPanel;
        public static WrapPanel componentPanel;
        public static WrapPanel workerPanel;
        public static WrapPanel orderPanel;
        public static String declayText;
        public static WrapPanel docsPanel;
        public static List<byte[]> documentsList = new List<byte[]>();
        public static List<String> docNameList = new List<String>();
        public static List<String> docTypeList = new List<String>();

        public static List<SizeObject> sizeObjectsList = new List<SizeObject>();
    }
}
