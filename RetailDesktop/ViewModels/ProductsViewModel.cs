using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using RetailDesktop.Views;
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
        public ICommand AddProductCommand { get; private set; }
        private readonly ProductService productService;

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();
            productService = new ProductService();
            LoadProductsCommand = new RelayCommand(LoadProducts);
            AddProductCommand = new RelayCommand(AddProduct);
            LoadProducts();
        }

        private void LoadProducts()
        {
            List<Product> products = productService.GetProducts();
            products.Sort((a, b) => string.Compare(a.Code, b.Code));

            Products.Clear();

            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

        private void AddProduct()
        {
            var window = new AddProductWindow();
            bool? result = window.ShowDialog();

            if (result == true)
            {
                Product newProduct = window.NewProduct;
                bool success = productService.AddProduct(newProduct);

                if (success) 
                {
                    newProduct.CurrentPrice = newProduct.Price;
                    newProduct.Category = window.ProductCategory;
                    Products.Add(newProduct);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении товара на сервер.");
                }
            }
        }
    }
}
