using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class OrderItemGet
    {
        public Product Product { get; set; }
        public int Amount { get; set; }

        public decimal? Total => Product.CurrentPrice * Amount;
    }
}
