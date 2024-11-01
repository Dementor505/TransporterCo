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
using TransporterCompany.Windows;

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для OrderManager.xaml
    /// </summary>
    public partial class OrderManager : UserControl
    {
        Order _order;
        public OrderManager(Order order)
        {
            InitializeComponent();
            _order = order;


            OrderStatus statusOrder = App.transBase.OrderStatus.OrderByDescending(x => x.Date_Change).FirstOrDefault(so => so.Order.Id_Order == _order.Id_Order);
            if (statusOrder?.Status != null)
            {
                // Получаем Status по идентификатору StatusOrder
                Status status = App.transBase.Status.FirstOrDefault(s => s.Id_Status == statusOrder.Status.Id_Status);

                // Устанавливаем текст статуса, если Status найден
                //StatusTb.Text = status?.Name_Status ?? "Статус не найден";
                StatusTb2.Text = status?.Name_Status ?? "Статус не найден";
            }
            else
            {
                // Устанавливаем текст статуса, если OrderStatus или Status не найдены
                //StatusTb.Text = "Статус не найден";
                StatusTb2.Text = "Статус не найден";
            }

            //if (StatusTb2.Text == "Новый" || StatusTb2.Text == "Отменён" || StatusTb2.Text == "Составление спецификации" || StatusTb2.Text == "Подтверждение")
            //{
            //    ReturnBtn.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    ReturnBtn.Visibility = Visibility.Hidden;
            //}

            if (order.Id_Manager != null)
            {
                AddMyBtn.Visibility = Visibility.Hidden;
                ReturnBtn.Visibility = Visibility.Hidden;
            }
            if (statusOrder.Id_Status == 2)
            {
                ReturnBtn.Visibility = Visibility.Hidden;
                AddMyBtn.Visibility = Visibility.Hidden;
            }

            NumberTb.Text = _order.Id_Order.ToString();
            DateTb.Text = _order.DateStart.ToString();
            DateTb.Text = _order.DateStart.ToString();
            NameTb.Text = _order.Name_Order.ToString();
            CostTb.Text = _order.Cost.ToString();
            ClientTb.Text = _order.Id_Client.ToString();
            DateFinishTb.Text = _order.DateEnd.ToString();
            if (_order.Id_Manager != null) ManagerTb.Text = _order.Id_Manager.ToString();
            _order = order;
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            DeclayWindow declayWindow = new DeclayWindow();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = declayWindow.Width;
            double windowHeight = declayWindow.Height;
            declayWindow.Left = (screenWidth / 2) - (windowWidth / 2);
            declayWindow.Top = (screenHeight / 2) - (windowHeight / 2);
            declayWindow.ShowDialog();
            OrderStatus oldStatus = App.transBase.OrderStatus
                .Where(x => x.Id_Order == _order.Id_Order)
                .OrderByDescending(x => x.Id_Order)
                .FirstOrDefault();
            OrderStatus newOrderStatus = new OrderStatus()
            {
                Id_Status = 2,
                Date_Change = DateTime.Now,
                Time_Change = null,
                Id_Order = _order.Id_Order,
                Id_OldStatus = oldStatus.Id_Status,
                Description = App.declayText,
            };
            App.transBase.OrderStatus.Add(newOrderStatus);
            App.transBase.SaveChanges();
            ChangeStatus();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            FirstLayer.Visibility = Visibility.Hidden;
            SecondLayer.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            FirstLayer.Visibility = Visibility.Visible;
            SecondLayer.Visibility = Visibility.Hidden;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.loggedUser.Id_Role == 1 && StatusTb2.Text == "Новый")
            {
                MessageBox.Show("Удалён");
            }
            else
            {
                MessageBox.Show("Удалять можно только новые заказы");
            }
        }

        private void AddMyBtn_Click(object sender, RoutedEventArgs e)
        {
            _order.Id_Manager = App.loggedUser.Login;
            App.transBase.SaveChanges();

            List<Order> managerOrders = new List<Order>();
            foreach (OrderStatus orderStatus in App.transBase.OrderStatus)
            {
                if (orderStatus.Id_Status == 1) managerOrders.Add(orderStatus.Order);
            }
            foreach (Order order in App.transBase.Order)
            {
                if (order.Id_Manager == App.loggedUser.Login && !managerOrders.Contains(order)) managerOrders.Add(order);
            }

            App.orderPanel.Children.Clear();
            foreach (Order order in managerOrders)
            {
                App.orderPanel.Children.Add(new OrderManager(order));
            }

            OrderStatus oldStatus = App.transBase.OrderStatus
                .Where(x => x.Id_Order == _order.Id_Order)
                .OrderByDescending(x => x.Id_Order)
                .FirstOrDefault();
            OrderStatus newOrderStatus = new OrderStatus()
            {
                Id_Status = 3,
                Date_Change = DateTime.Now,
                Time_Change = null,
                Id_Order = _order.Id_Order,
                Id_OldStatus = oldStatus.Id_Status,
            };
            App.transBase.OrderStatus.Add(newOrderStatus);
            App.transBase.SaveChanges();
            ChangeStatus();
        }
        public void ChangeStatus()
        {
            OrderStatus statusOrder = App.transBase.OrderStatus.OrderByDescending(x => x.Date_Change).FirstOrDefault(so => so.Order.Id_Order == _order.Id_Order);
            Status status = App.transBase.Status.FirstOrDefault(x => x.Id_Status == statusOrder.Id_Status);
            //MessageBox.Show(statusOrder.Id_Status.ToString() + "   " + status.Name_Status);
            List<Order> managerOrders = new List<Order>();
            foreach (OrderStatus orderStatus in App.transBase.OrderStatus)
            {
                if (orderStatus.Id_Status == 1) managerOrders.Add(orderStatus.Order);
            }
            foreach (Order order in App.transBase.Order)
            {
                if (order.Id_Manager == App.loggedUser.Login && !managerOrders.Contains(order)) managerOrders.Add(order);
            }
            App.orderPanel.Children.Clear();
            foreach (Order order in managerOrders)
            {
                App.orderPanel.Children.Add(new OrderManager(order));
            }
        }
    }
}
