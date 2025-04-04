using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RetailDesktop.Models
{
    public class PurchaseItemModel
    {
        [JsonProperty("product_code")]
        public string ProductCode { get; set; } = string.Empty;

        [JsonProperty("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("price_for_an_item")]
        public decimal PriceForAnItem { get; set; }

        [JsonProperty("increase_percent")]
        public decimal? IncreasePercent { get; set; }

        public decimal RetailPrice => PriceForAnItem * (1 + (IncreasePercent ?? 0) / 100);
    }
}
