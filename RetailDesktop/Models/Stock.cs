using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Stock
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public string DisplayName => $"{Product.FullName} (Цена: {Product.CurrentPrice}) (Остаток: {Quantity})";
    }
}
