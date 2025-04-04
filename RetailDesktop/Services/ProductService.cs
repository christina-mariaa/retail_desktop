using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailDesktop.Services
{
    public class ProductService
    {
        private readonly HttpClient httpClient;

        public ProductService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Product>> GetProducts()
        {
            string url = Constants.BaseUrl + "products/";

            try
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
                    return products;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка товаров:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<Product>();
        }

        public async Task<bool> AddProduct(Product product)
        {
            string url = Constants.BaseUrl + "products/";

            try
            {
                string json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            string url = Constants.BaseUrl + "product-categories/";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<ProductCategory> productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(json);
                    return productCategories;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий товаров:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return new List<ProductCategory>();
        }
    }
}
