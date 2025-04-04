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
    public class TransferService
    {
        private HttpClient _httpClient;
        public TransferService() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> MakeTransfer(Transfer transfer)
        {
            string url = Constants.BaseUrl + "transfers/";

            try
            {
                string json = JsonConvert.SerializeObject(transfer);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(errorResponse);
                        if (errorObj != null && errorObj.ContainsKey("error"))
                        {
                            MessageBox.Show("Ошибка при создании перемещения:\n" + errorObj["error"], "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при создании перемещения:\n" + errorResponse, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Сетевая ошибка при попытке сделать перемещение:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
