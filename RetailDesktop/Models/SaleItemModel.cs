using Newtonsoft.Json;
using RetailDesktop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RetailDesktop.Models
{
    public class SaleItemModel : ViewModelBase
    {
        private Product product;
        private int quantity;
        private decimal price;
        private ImageSource imageSource;

        public Product Product
        {
            get => product;
            set
            {
                if (SetProperty(ref product, value))
                {
                    Price = product?.CurrentPrice ?? 0;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (SetProperty(ref quantity, value))
                {
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public ImageSource SelectedImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }
        public decimal Price
        {
            get => price;
            set
            {
                if (SetProperty(ref price, value))
                {
                    OnPropertyChanged(nameof(Total));
                }
            }
        }
        [JsonIgnore]
        public decimal Total => Price * Quantity;

        public SaleItemPost ToPost()
        {
            return new SaleItemPost
            {
                ProductId = Product?.Code,
                Amount = Quantity
            };
        }
    }
}
