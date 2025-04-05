using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SaleItemPost
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
