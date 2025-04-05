using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Counteragent
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contact_info")]
        public string ContactInfo { get; set; }

        [JsonProperty("is_supplier")]
        public bool IsSupplier { get; set; }
    }
}
