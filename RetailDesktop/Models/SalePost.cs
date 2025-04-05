using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class SalePost
    {
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("seller_id")]
        public int? SellerId { get; set; }

        [JsonProperty("sale_items")]
        public ObservableCollection<SaleItemPost> SaleItems { get; set; } = new ObservableCollection<SaleItemPost>();
    }
}
