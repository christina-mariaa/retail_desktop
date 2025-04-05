using RetailDesktop.Helpers;
using RetailDesktop.Services;
using RetailDesktop.Models;
using RetailDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailDesktop.ViewModels
{
    public class SalesViewModel : ViewModelBase
    {
        private readonly SaleService saleService;

        //public ObservableCollection<SalePost> Sales_for_post { get; set; }
        public ObservableCollection<SaleGet> Sales { get; set; }

        public ICommand LoadSalesCommand { get; set; }
        public ICommand CreateSaleCommand { get; set; }
        public ICommand OpenSaleDetailsCommand { get; set; }

        private SalePost _selectedSale;
        //public Sale SelectedSale
        //{
        //    get => _selectedSale;
        //    set
        //    {
        //        if (SetProperty(ref _selectedSale, value) && value != null)
        //        {
        //            OpenSaleDetails(value);
        //        }
        //    }
        //}

        public SalesViewModel()
        {
            saleService = new SaleService();
            Sales = new ObservableCollection<SaleGet>();

            LoadSalesCommand = new RelayCommand(async () => await LoadSales());
            CreateSaleCommand = new RelayCommand(OpenCreateSaleWindow);
        }

        public async Task LoadSales()
        {
            var sales = await saleService.GetSales();
            Sales.Clear();
            foreach (var sale in sales)
                Sales.Add(sale);
        }

        private void OpenCreateSaleWindow()
        {
            var window = new CreateSaleWindow();
            if (window.ShowDialog() == true)
            {
                LoadSalesCommand.Execute(null);
            }
        }

        //private void OpenSaleDetails(Sale sale)
        //{
        //    var window = new SaleDetailsWindow(sale);
        //    window.ShowDialog();
        //}
    }
}
