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

        public List<Product> GetProducts()
        {
            string url = Constants.BaseUrl + "products/";

            try
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
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

        public bool AddProduct(Product product)
        {
            string url = Constants.BaseUrl + "products/";

            try
            {
                string json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(url, content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления товара:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public List<ProductCategory> GetProductCategories()
        {
            string url = Constants.BaseUrl + "product-categories/";

            try
            {
                var response = httpClient.GetAsync(url).Result;

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
