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
    public class OrderService
    {
        private HttpClient httpClient { get; set; }
        public OrderService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<OrderGet>> GetOrders()
        {
            string url = Constants.BaseUrl + "orders/";

            try
            {
                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<OrderGet> orders = JsonConvert.DeserializeObject<List<OrderGet>>(json);
                    return orders;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка заказов:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<OrderGet>();
        }

        public async Task<bool> MakeOrder(Order order)
        {
            string url = Constants.BaseUrl + "orders/";

            try
            {
                string json = JsonConvert.SerializeObject(order);
                
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResponse);
                        if (errorObj != null && errorObj.ContainsKey("error"))
                        {
                            MessageBox.Show("Ошибка создания заказа:\n" + errorObj["error"], "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка создания заказа:\n" + errorResponse, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка от сервера:\n" + errorResponse, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при попытке создать заказ:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

    }
}
