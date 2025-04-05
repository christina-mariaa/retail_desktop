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

namespace RetailDesktop.ViewModels
{
    public class ProfitableProductsReportViewModel : ViewModelBase
    {
        private readonly ReportService reportService = new ReportService();

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public ObservableCollection<ProfitableProductReportItem> ProfitableProducts { get; set; } = new ObservableCollection<ProfitableProductReportItem>();

        public ICommand LoadProfitableProductsCommand { get; }

        public ProfitableProductsReportViewModel()
        {
            LoadProfitableProductsCommand = new RelayCommand(async () => await LoadProfitableProducts());
        }

        private async Task LoadProfitableProducts()
        {
            if (StartDate == null || EndDate == null)
            {
                MessageBox.Show("Выберите обе даты.");
                return;
            }

            try
            {
                var result = await reportService.GetProfitableProducts(StartDate.Value, EndDate.Value);
                ProfitableProducts.Clear();
                foreach (var item in result)
                    ProfitableProducts.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке отчета: " + ex.Message);
            }
        }
    }
}
