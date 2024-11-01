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

namespace TransporterCompany.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Order> clientOrders = new List<Order>();
        List<Order> managerOrders = new List<Order>();
        public OrderPage()
        {
            InitializeComponent();

            App.orderPanel = orderWp;

            clientOrders = App.transBase.Order.Where(x => x.Id_Client == App.loggedUser.Login).ToList();

            //var managerOrders = App.transBase.Order
            //    .Join(
            //        App.transBase.OrderStatus,
            //        order => order.Id_Order,
            //        os => os.Id_Order,
            //        (order, os) => new
            //        {
            //            OrderId = order.Id_Order,
            //            StatusId = os.Id_Status,
            //        }
            //    ).Where(x => x.StatusId == 1)
            //    .ToList();
            //MessageBox.Show(managerOrders.Count.ToString());

            foreach (OrderStatus orderStatus in App.transBase.OrderStatus)
            {
                if (orderStatus.Id_Status == 1) managerOrders.Add(orderStatus.Order);
            }
            foreach (Order order in App.transBase.Order)
            {
                if (order.Id_Manager == App.loggedUser.Login && !managerOrders.Contains(order)) managerOrders.Add(order);
            }


            if (App.loggedUser.Id_Role == 1) Client();
            if (App.loggedUser.Id_Role == 2) Director();
            if (App.loggedUser.Id_Role == 3) Manager();
            if (App.loggedUser.Id_Role == 4) Constructor();
            if (App.loggedUser.Id_Role == 5) Master();
        }
        public void Client()
        {
            foreach (Order order in clientOrders)
            {
                orderWp.Children.Add(new OrderClient(order));
            }
        }
        public void Director()
        {

        }
        public void Manager()
        {
            foreach (Order order in managerOrders)
            {
                orderWp.Children.Add(new OrderManager(order));
            }
        }
        public void Constructor()
        {

        }
        public void Master()
        {

        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            App.menuFrame.Navigate(new AddEditOrder(new Order()));
        }
    }
}
