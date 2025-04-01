using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Product
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("current_price")]
        public decimal? CurrentPrice { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("category")]
        public int? Category { get; set; }

    }
}
