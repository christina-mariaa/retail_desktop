using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Transfer
    {
        [JsonProperty("from_location_id")]
        public string FromLocationId { get; set; }
        [JsonProperty("to_location_id")]
        public string ToLocationId { get; set; }
        [JsonProperty("items")]
        public ObservableCollection<TransferItem> Items { get; set; } = new ObservableCollection<TransferItem>();
    }
}
