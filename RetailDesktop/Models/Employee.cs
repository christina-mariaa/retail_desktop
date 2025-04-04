using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("position_data")]
        public Position Position { get; set; }

        [JsonProperty("position")]
        public int PositionId { get; set; }

        [JsonProperty("location_data")]
        public Location Location { get; set; }

        [JsonProperty("location")]
        public string LocationCode { get; set; }
        [JsonIgnore]
        public string FullName => $"{Surname} {Name} {MiddleName}";
    }
}
