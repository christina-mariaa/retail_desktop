using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.Services
{
    public class ReportService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private SaleService saleService = new SaleService();

        public async Task<List<PopularProductReportItem>> GetPopularProducts(DateTime startDate, DateTime endDate)
        {
            string url = $"{Constants.BaseUrl}popular-products/?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PopularProductReportItem>>(json);
        }

        public async Task<List<SaleGet>> GetSales(DateTime start, DateTime end)
        {
            var url = $"{Constants.BaseUrl}sales/?start_date={start:yyyy-MM-dd}&end_date={end:yyyy-MM-dd}";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SaleGet>>(json);
        }

        public async Task<List<ProfitableProductReportItem>> GetProfitableProducts(DateTime startDate, DateTime endDate)
        {
            string url = $"{Constants.BaseUrl}profitable-products?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProfitableProductReportItem>>(json);
        }

        public async Task<List<SalesReportRow>> GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            var sales = await saleService.GetSales(startDate, endDate);

            var report = sales
                .SelectMany(sale => sale.SaleItems.Select(item => new
                {
                    Store = sale.Store.Address,
                    Seller = $"{sale.Seller.Surname} {sale.Seller.Name} {sale.Seller.MiddleName}",
                    Product = item.Product.Name,
                    Quantity = item.Amount,
                    Total = item.Amount * (item.Product.CurrentPrice ?? 0)
                }))
                .GroupBy(x => new { x.Store, x.Seller, x.Product })
                .Select(g => new SalesReportRow
                {
                    Store = g.Key.Store,
                    Seller = g.Key.Seller,
                    Product = g.Key.Product,
                    QuantitySold = g.Sum(x => x.Quantity),
                    TotalSum = g.Sum(x => x.Total)
                })
                .ToList();

            return report;
        }

    }
}
