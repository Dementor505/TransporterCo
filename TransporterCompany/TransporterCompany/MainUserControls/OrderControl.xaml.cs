﻿using System;
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
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public Order _order;
        public OrderControl(Order order)
        {
            InitializeComponent();
            _order = order;


            OrderStatus statusOrder = App.transBase.OrderStatus.FirstOrDefault(so => so.Order.Id_Order == _order.Id_Order);
            if (statusOrder?.Status != null)
            {
                // Получаем Status по идентификатору StatusOrder
                Status status = App.transBase.Status.FirstOrDefault(s => s.Id_Status == statusOrder.Status.Id_Status);

                // Устанавливаем текст статуса, если Status найден
                StatusTb.Text = status?.Name_Status ?? "Статус не найден";
            }
            else
            {
                // Устанавливаем текст статуса, если OrderStatus или Status не найдены
                StatusTb.Text = "Статус не найден";
            }

            NumberTb.Text = _order.Id_Order.ToString();
            DateTb.Text = _order.DateStart.ToString();
            DateTb.Text = _order.DateStart.ToString();
            NameTb.Text = _order.Name_Order.ToString();
            CostTb.Text = _order.Cost.ToString();
            ClientTb.Text = _order.Id_Client.ToString();
            DateFinishTb.Text = _order.DateEnd.ToString();
            ManagerTb.Text = _order.Id_Manager.ToString();
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
    }
}