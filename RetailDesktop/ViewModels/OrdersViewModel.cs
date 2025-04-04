using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using RetailDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailDesktop.ViewModels
{
    public class OrdersViewModel
    {
        public ObservableCollection<OrderGet> Orders { get; set; }
        public ICommand LoadOrdersCommand { get; set; }
        public ICommand OpenOrderCommand { get; set; }
        public ICommand OpenCreateOrderWindowCommand { get; set; }
        private OrderService orderService { get; set; }
        public OrderGet SelectedOrder { get; set; }

        public OrdersViewModel()
        {
            Orders = new ObservableCollection<OrderGet>();
            LoadOrdersCommand = new RelayCommand(LoadOrders);
            OpenOrderCommand = new RelayCommand(OpenOrderDetails);
            OpenCreateOrderWindowCommand = new RelayCommand(OpenCreateOrderWindow);
            orderService = new OrderService();
            LoadOrders();
        }

        private void LoadOrders()
        {
            List<OrderGet> orders = orderService.GetOrders();

            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }

        private void OpenOrderDetails()
        {
            if (SelectedOrder != null)
            {
                var window = new OrderDetailsWindow(SelectedOrder);
                window.ShowDialog();
            }
        }

        private void OpenCreateOrderWindow()
        {
            var window = new MakeOrderWindow();
            window.ShowDialog();
        }
    }
}
