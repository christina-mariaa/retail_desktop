using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class OrderGet : Order
    {
        public int Id { get; set; }

        public Location Store { get; set; }
        public Counteragent Client { get; set; }

        public string State { get; set; }
        [JsonProperty("total_price")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("delivery_driver")]
        public Employee DeliveryDriver { get; set; }

        [JsonProperty("order_picker")]
        public Employee OrderPicker { get; set; }

        [JsonProperty("order_items")]
        public List<OrderItemGet> OrderItems { get; set; }
    }
}
