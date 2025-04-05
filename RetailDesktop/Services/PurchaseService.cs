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
    public class PurchaseService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<bool> SendPurchase(PurchaseModel purchase)
        {
            string url = Constants.BaseUrl + "purchases/";

            try
            {
                string json = JsonConvert.SerializeObject(purchase);
                MessageBox.Show(json);
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
                            MessageBox.Show("Ошибка создания поставки:\n" + errorObj["error"], "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка создания поставки:\n" + errorResponse, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Ошибка при попытке создать поставку:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
