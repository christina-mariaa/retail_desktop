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
    public class StockReportViewModel : ViewModelBase
    {
        private readonly StockService stockService = new StockService();

        public ObservableCollection<Stock> StockItems { get; set; } = new ObservableCollection<Stock>();

        public ICommand LoadStockCommand { get; }

        public StockReportViewModel()
        {
            LoadStockCommand = new RelayCommand(async () => await LoadStock());
        }

        private async Task LoadStock()
        {
            try
            {
                var stocks = await stockService.GetAllStocks();
                StockItems.Clear();
                foreach (var stock in stocks)
                {
                    StockItems.Add(stock);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке остатков: " + ex.Message);
            }
        }
    }
}
