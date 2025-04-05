using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SaleGet
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("store")]
        public Location Store {  get; set; }
        [JsonProperty("client")]
        public Counteragent Client { get; set; }
        [JsonProperty("date_of_sale")]
        public DateTime DateOfSale { get; set; }
        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }
        [JsonProperty("seller")]
        public Employee Seller { get; set; }
        [JsonProperty("sale_items")]
        public List<SaleItemGet> SaleItems { get; set; }
    }
}
