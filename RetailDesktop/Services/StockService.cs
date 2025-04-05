using Newtonsoft.Json;
using RetailDesktop.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RetailDesktop.Helpers;

namespace RetailDesktop.Services
{
    public class StockService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private const string baseUrl = Constants.BaseUrl + "product-leftovers/get_product_stocks/";

        public async Task<List<Stock>> GetAllStocks()
        {
            var response = await httpClient.GetStringAsync(baseUrl);
            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        public async Task<List<Stock>> GetStocksByStore(string storeCode)
        {
            var uri = $"{baseUrl}?store_code={WebUtility.UrlEncode(storeCode)}";
            var response = await httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        public async Task<List<Stock>> GetStocksByProduct(string productCode)
        {
            var uri = $"{baseUrl}?product_code={WebUtility.UrlEncode(productCode)}";
            var response = await httpClient.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Stock>>(response);
        }

        public async Task<Stock> GetStockForProductInStore(string productCode, string storeCode)
        {
            var uri = $"{baseUrl}?product_code={WebUtility.UrlEncode(productCode)}&store_code={WebUtility.UrlEncode(storeCode)}";
            var response = await httpClient.GetStringAsync(uri);
            var result = JsonConvert.DeserializeObject<List<Stock>>(response);
            return result.Count > 0 ? result[0] : new Stock();
        }
    }
}
