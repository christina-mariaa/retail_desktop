using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SalesComparisonRow
    {
        public string Store { get; set; }
        public string Seller { get; set; }
        public string Product { get; set; }

        public int QuantityPeriod1 { get; set; }
        public int QuantityPeriod2 { get; set; }

        public int Difference => QuantityPeriod2 - QuantityPeriod1;

        public Brush RowColor
        {
            get
            {
                if (Difference > 0) return Brushes.LightGreen;
                if (Difference < 0) return Brushes.LightCoral;
                return Brushes.Transparent;
            }
        }
    }

}
