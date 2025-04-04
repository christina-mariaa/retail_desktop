using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class PurchaseModel
    {
        [JsonProperty("supplier")]
        public int Supplier { get; set; }

        [JsonProperty("warehouse")]
        public int Warehouse { get; set; }

        [JsonProperty("purchase_products")]
        public ObservableCollection<PurchaseItemModel> PurchaseProducts { get; set; } = new ObservableCollection<PurchaseItemModel>();
    }
}
