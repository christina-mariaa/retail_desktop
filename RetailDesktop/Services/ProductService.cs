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

        public ProductService() {
            httpClient = new HttpClient();
        }

        public List<Product> GetProducts()
        {
            string url = Constants.BaseUrl + "products/";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(url).Result;
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
        }
    }
}
