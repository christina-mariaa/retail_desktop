using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class OrderPost : Order
    {
        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("order_items")]
        public List<OrderItemPost> OrderItems { get; set; }

        [JsonProperty("delivery_driver_id")]
        public int? DeliveryDriverId { get; set; }

        [JsonProperty("order_picker_id")]
        public int? OrderPickerId { get; set; }
    }
}
