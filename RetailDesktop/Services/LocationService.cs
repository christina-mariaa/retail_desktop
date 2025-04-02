using System;
using RetailDesktop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RetailDesktop.Helpers;
using Newtonsoft.Json;
using System.Windows;

namespace RetailDesktop.Services
{
    public class LocationService
    {
        public HttpClient httpClient { get; set; }

        public LocationService() 
        { 
            httpClient = new HttpClient();
        }

        public List<Location> GetLocations()
        {
            string url = Constants.BaseUrl + "locations/";

            try
            {
                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(json);
                    return locations;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка магазинов и складов:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<Location>();
        }

        public bool AddLocation(Location location)
        {
            string url = Constants.BaseUrl + "locations/";

            try
            {
                string json = JsonConvert.SerializeObject(location);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(url, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления магазина/склада:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
