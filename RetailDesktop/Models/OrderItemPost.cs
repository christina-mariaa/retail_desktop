using Newtonsoft.Json;
using RetailDesktop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailDesktop.Models
{
    public class OrderItemPost : ViewModelBase
    {
        [JsonIgnore]
        public Product Product
        {
            get => _product;
            set
            {
                if (SetProperty(ref _product, value))
                {
                    ProductId = value?.Code;
                }
            }
        }
        private Product _product;

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonIgnore]
        public decimal? Total => Product != null ? Product.CurrentPrice * Amount : 0;
    }
}
