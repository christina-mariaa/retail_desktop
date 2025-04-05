using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Position
    {
        [JsonProperty("id")]
        public int Id {  get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
