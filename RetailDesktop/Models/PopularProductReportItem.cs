using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class PopularProductReportItem
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("total_qty_sold")]
        public int TotalQtySold { get; set; }
    }
}
