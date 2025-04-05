using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SalesReportRow
    {
        public string Store { get; set; }
        public string Seller { get; set; }
        public string Product { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalSum { get; set; }
    }
}
