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

namespace TransporterCompany.MainUserControls
{
    /// <summary>
    /// Логика взаимодействия для OrderDirector.xaml
    /// </summary>
    public partial class OrderDirector : UserControl
    {
        Order _order;
        public OrderDirector(Order order)
        {
            InitializeComponent();

            _order = order;


            OrderStatus statusOrder = App.transBase.OrderStatus.OrderByDescending(x => x.Date_Change).FirstOrDefault(so => so.Order.Id_Order == _order.Id_Order);
            if (statusOrder?.Status != null)
            {
                Status status = App.transBase.Status.FirstOrDefault(s => s.Id_Status == statusOrder.Status.Id_Status);

                StatusTb2.Text = status?.Name_Status ?? "Статус не найден";
            }
            else
            {
                StatusTb2.Text = "Статус не найден";
            }

            //if (order.Id_Manager != null)
            //{
            //    AddMyBtn.Visibility = Visibility.Hidden;
            //    ReturnBtn.Visibility = Visibility.Hidden;
            //}
            //if (statusOrder.Id_Status == 2)
            //{
            //    ReturnBtn.Visibility = Visibility.Hidden;
            //    AddMyBtn.Visibility = Visibility.Hidden;
            //}

            NumberTb.Text = _order.Id_Order.ToString();
            DateTb.Text = _order.DateStart.ToString();
            DateTb.Text = _order.DateStart.ToString();
            NameTb.Text = _order.Name_Order.ToString();
            CostTb.Text = _order.Cost.ToString();
            ClientTb.Text = _order.Id_Client.ToString();
            DateFinishTb.Text = _order.DateEnd.ToString();
            if (_order.Id_Manager != null) ManagerTb.Text = _order.Id_Manager.ToString();
            _order = order;
            _order = order;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
