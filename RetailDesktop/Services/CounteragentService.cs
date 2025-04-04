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
    public class CounteragentService
    {
        private HttpClient httpClient {  get; set; }
        public CounteragentService() 
        { 
            httpClient = new HttpClient();
        }

        public List<Counteragent> GetCouteragent(bool IsClient)
        {
            string url = Constants.BaseUrl + (IsClient? "clients-info/" : "suppliers/");

            try
            {
                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<Counteragent> counteragents = JsonConvert.DeserializeObject<List<Counteragent>>(json);
                    return counteragents;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка {(IsClient ? "клиентов" : "поставщиков")}:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<Counteragent>();
        }

        public bool AddCounteragent(bool IsClient, Counteragent counteragent)
        {
            string url = Constants.BaseUrl + (IsClient ? "clients-info/" : "suppliers/");

            try
            {
                string json = JsonConvert.SerializeObject(counteragent);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(url, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Ошибка сервера:\n" + errorDetails);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления {(IsClient ? "клиента" : "поставщика")}:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
    }
}
