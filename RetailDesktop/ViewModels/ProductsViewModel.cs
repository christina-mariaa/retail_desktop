using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace RetailDesktop.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; private set; }
        public ICommand LoadProductsCommand { get; private set; }
        private readonly ProductService productService;

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();
            productService = new ProductService();
            LoadProductsCommand = new RelayCommand(LoadProducts);

        }

        private void LoadProducts()
        {
            List<Product> products = productService.GetProducts();

            Products.Clear();

            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

    }
}
