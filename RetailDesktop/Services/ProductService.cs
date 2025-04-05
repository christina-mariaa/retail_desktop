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
using System.IO;


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

        public async Task<bool> AddProduct(Product product, string imageFilePath)
        {
            string url = Constants.BaseUrl + "products/";

            try
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(product.Code), "code");
                formData.Add(new StringContent(product.Name), "name");
                formData.Add(new StringContent(product.Brand ?? ""), "brand");
                formData.Add(new StringContent(product.Price?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "0"), "price");
                formData.Add(new StringContent(product.CategoryId?.ToString() ?? ""), "category_id");

                if (!string.IsNullOrEmpty(imageFilePath) && File.Exists(imageFilePath))
                {
                    var fileStream = File.OpenRead(imageFilePath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                    formData.Add(fileContent, "image", Path.GetFileName(imageFilePath));
                }

                var response = await httpClient.PostAsync(url, formData);
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

        public async Task<bool> PatchProductPriceAsync(string productCode, decimal newPrice)
        {
            string url = $"{Constants.BaseUrl}products/{productCode}/";

            try
            {
                var payload = new
                {
                    price = newPrice
                };

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                {
                    Content = content
                };

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка изменения цены: " + error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке запроса PATCH: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
