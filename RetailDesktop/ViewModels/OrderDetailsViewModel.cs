using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderGet Order { get; set; }
        public ObservableCollection<OrderItemGet> OrderItems { get; set; }

        public OrderDetailsViewModel(OrderGet order)
        {
            Order = order;
        }
    }
}
