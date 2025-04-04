using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Transfer
    {
        [JsonProperty("from_location_id")]
        public int FromLocationId { get; set; }
        [JsonProperty("to_location_id")]
        public int ToLocationId { get; set; }
        [JsonProperty("items")]
        public List<TransferItem> Items { get; set; }
    }
}
