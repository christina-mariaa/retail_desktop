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
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace RetailDesktop.ViewModels
{
    public class SalesReportViewModel : ViewModelBase
    {
        private readonly SaleService saleService = new SaleService();

        public ObservableCollection<SalesReportRow> ReportRows { get; set; } = new ObservableCollection<SalesReportRow>();

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime _endDate = DateTime.Today;
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        private ICollectionView _groupedReport;
        public ICollectionView GroupedReport
        {
            get => _groupedReport;
            set => SetProperty(ref _groupedReport, value);
        }

       



        public ICommand LoadReportCommand { get; }

        public SalesReportViewModel()
        {
            LoadReportCommand = new RelayCommand(async () => await LoadReport());
        }

        private async Task LoadReport()
        {
            try
            {
                var sales = await saleService.GetSales(StartDate, EndDate);
                var report = sales
                    .SelectMany(sale => sale.SaleItems.Select(item => new
                    {
                        Store = sale.Store.Address,
                        Seller = $"{sale.Seller.Surname} {sale.Seller.Name}",
                        Product = item.Product.Name,
                        Quantity = item.Amount,
                        Sum = item.Amount * (decimal)item.Product.CurrentPrice
                    }))
                    .GroupBy(x => new { x.Store, x.Seller, x.Product })
                    .Select(g => new SalesReportRow
                    {
                        Store = g.Key.Store,
                        Seller = g.Key.Seller,
                        Product = g.Key.Product,
                        QuantitySold = g.Sum(x => x.Quantity),
                        TotalSum = g.Sum(x => x.Sum)
                    })
                    .ToList();

                ReportRows.Clear();
                foreach (var row in report)
                {
                    ReportRows.Add(row);
                }

                GroupedReport = CollectionViewSource.GetDefaultView(ReportRows);
                GroupedReport.GroupDescriptions.Clear();
                GroupedReport.GroupDescriptions.Add(new PropertyGroupDescription("Store"));
                GroupedReport.GroupDescriptions.Add(new PropertyGroupDescription("Seller"));
                OnPropertyChanged(nameof(GroupedReport));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки отчета:\n" + ex.Message);
            }
        }

    }
}
