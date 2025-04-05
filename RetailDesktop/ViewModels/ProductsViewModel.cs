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
        public ICommand ExportProductsCommand { get; private set; }
        public ICommand ChangePriceCommand { get; private set; }

        private readonly ProductService productService;
        private readonly ProductExportService productExportService;

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();

            productService = new ProductService();
            productExportService = new ProductExportService();

            LoadProductsCommand = new RelayCommand(async () => await LoadProducts());
            AddProductCommand = new RelayCommand(async () => await AddProduct());
            ExportProductsCommand = new RelayCommand(async () => await ExportProducts());
            ChangePriceCommand = new AsyncRelayCommand<Product>(async (product) =>
            {
                await ChangePrice(product);
            });
        }

        public async Task LoadProducts()
        {
            List<Product> products = await productService.GetProducts();

            Products.Clear();

            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

        private async Task AddProduct()
        {
            var window = new AddProductWindow();
            bool? result = window.ShowDialog();

            if (result == true)
            {
                Product newProduct = window.NewProduct;
                string image = window.ImageFilePath;
                bool success = await productService.AddProduct(newProduct, image);

                if (success) 
                {
                    newProduct.CurrentPrice = newProduct.Price;
                    newProduct.Category = window.ProductCategory;
                    Products.Add(newProduct);
                    MessageBox.Show("Товар добавлен", "Успешное добавление", button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении товара на сервер.");
                }
            }
        }

        private async Task ExportProducts()
        {
            await productExportService.ExportProductsToFolder();

        }

        private async Task ChangePrice(Product product)
        {
            if (product == null) return;

            var window = new EditPriceWindow(product.Code);
            window.Owner = Application.Current.MainWindow;

            if (window.ShowDialog() == true)
            {
                await LoadProducts();
            }
        }
    }
}
