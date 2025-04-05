using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Order
    {
        [JsonProperty("delivery_address")]
        public string DeliveryAddress { get; set; }
        [JsonIgnore]
        public DateTime DeliveryDate { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("delivery_date")]
        public string DeliveryDateString
        {
            get => DeliveryDate.ToString("yyyy-MM-dd");
            set => DeliveryDate = DateTime.TryParse(value, out var date) ? date : DateTime.MinValue;
        }
    }
}
