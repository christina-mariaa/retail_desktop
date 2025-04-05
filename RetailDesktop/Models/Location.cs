using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RetailDesktop.Models
{
    public class Location
    {
        [JsonProperty ("code")]
        public string Code { get; set; }
        [JsonProperty ("address")] 
        public string Address { get; set; }
        [JsonProperty("is_store")]
        public bool IsStore { get; set; }
        [JsonProperty("is_main")]
        public bool IsMain { get; set; }

        [JsonIgnore]
        public string FullName => $"{Code} / {Address}";
    }
}
