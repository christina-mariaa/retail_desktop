using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SaleItemGet
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonIgnore]
        public decimal Total => (Product?.CurrentPrice ?? 0) * Amount;
    }
}
