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
    public class SaleService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<List<SaleGet>> GetSales()
        {
            string url = Constants.BaseUrl + "sales/";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var sales = JsonConvert.DeserializeObject<List<SaleGet>>(json);
                    return sales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке продаж:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<SaleGet>();
        }

        public async Task<List<SaleGet>> GetSales(DateTime startDate, DateTime endDate)
        {
            string url = $"{Constants.BaseUrl}sales-by-period/?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var sales = JsonConvert.DeserializeObject<List<SaleGet>>(json);
                    return sales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке продаж:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<SaleGet>();
        }


        public async Task<SaleGet> MakeSale(SalePost sale)
        {
            string url = Constants.BaseUrl + "sales/";

            try
            {
                string json = JsonConvert.SerializeObject(sale);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SaleGet>(responseString);
                }
                else
                {
                    string errorText = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка при создании продажи:\n" + errorText, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении запроса:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}
