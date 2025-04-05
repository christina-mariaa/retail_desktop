using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailDesktop.ViewModels
{
    public class SalesComparisonViewModel : ViewModelBase
    {
        private readonly SaleService saleService = new SaleService();

        public ObservableCollection<SalesComparisonRow> ComparisonRows { get; set; } = new ObservableCollection<SalesComparisonRow>();

        public DateTime Period1Start { get; set; } = DateTime.Today.AddDays(-14);
        public DateTime Period1End { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime Period2Start { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime Period2End { get; set; } = DateTime.Today;

        public ICommand LoadComparisonCommand { get; }

        public SalesComparisonViewModel()
        {
            LoadComparisonCommand = new RelayCommand(async () => await LoadComparison());
        }

        private async Task LoadComparison()
        {
            var sales1 = await saleService.GetSales(Period1Start, Period1End);
            var sales2 = await saleService.GetSales(Period2Start, Period2End);

            var grouped1 = sales1
                .SelectMany(s => s.SaleItems.Select(i => new
                {
                    s.Store.Address,
                    Seller = $"{s.Seller.Surname} {s.Seller.Name}",
                    i.Product.Name,
                    i.Amount
                }))
                .GroupBy(x => new { x.Address, x.Seller, x.Name })
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

            var grouped2 = sales2
                .SelectMany(s => s.SaleItems.Select(i => new
                {
                    s.Store.Address,
                    Seller = $"{s.Seller.Surname} {s.Seller.Name}",
                    i.Product.Name,
                    i.Amount
                }))
                .GroupBy(x => new { x.Address, x.Seller, x.Name })
                .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));

            var allKeys = grouped1.Keys.Union(grouped2.Keys).ToList();

            ComparisonRows.Clear();

            foreach (var key in allKeys)
            {
                grouped1.TryGetValue(key, out var q1);
                grouped2.TryGetValue(key, out var q2);

                ComparisonRows.Add(new SalesComparisonRow
                {
                    Store = key.Address,
                    Seller = key.Seller,
                    Product = key.Name,
                    QuantityPeriod1 = q1,
                    QuantityPeriod2 = q2
                });
            }

            OnPropertyChanged(nameof(ComparisonRows));
        }
    }

}
