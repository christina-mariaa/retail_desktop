using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Product
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }

        [JsonProperty("current_price")]
        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

    }
}
