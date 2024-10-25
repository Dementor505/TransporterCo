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
using TransporterCompany.Pages;

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для WorkerPage.xaml
    /// </summary>
    public partial class WorkerPage : Page
    {
        public WorkerPage()
        {
            InitializeComponent();

            foreach (var worker in App.transBase.Worker)
            {
                workerWp.Children.Add(new WorkerControl(worker));
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AddEditWorker(new Worker()));
        }
    }
}
