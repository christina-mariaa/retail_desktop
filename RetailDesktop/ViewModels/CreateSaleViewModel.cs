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
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Diagnostics;


namespace RetailDesktop.ViewModels
{
    public class CreateSaleViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Stores { get; set; } = new ObservableCollection<Location>();
        public ObservableCollection<Counteragent> Clients { get; set; } = new ObservableCollection<Counteragent>();
        public ObservableCollection<Employee> Sellers { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Stock> StockProducts { get; set; } = new ObservableCollection<Stock>();
        public ObservableCollection<SaleItemModel> SaleItems { get; set; } = new ObservableCollection<SaleItemModel>();

        private Location _selectedStore;
        public Location SelectedStore
        {
            get => _selectedStore;
            set
            {
                if (SetProperty(ref _selectedStore, value))
                {
                    LoadSellersForStore();
                    LoadStockProducts();
                    OnPropertyChanged(nameof(IsStoreSelected));
                }
            }
        }

        public bool IsStoreSelected => _selectedStore != null;

        public Counteragent SelectedClient { get; set; }
        public Employee SelectedSeller { get; set; }

        private SaleItemModel _selectedSaleItem;
        public SaleItemModel SelectedSaleItem
        {
            get => _selectedSaleItem;
            set
            {
                _selectedSaleItem = value;
                OnPropertyChanged(nameof(SelectedSaleItem));
                
            }
        }

        public decimal TotalSum => SaleItems.Sum(i => i.Total);

        public bool CanPrint { get; set; }
        public SaleGet createdSale;

        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SubmitSaleCommand { get; }
        public ICommand PrintReceiptCommand { get; }

        private readonly ProductService productService = new ProductService();
        private readonly CounteragentService counteragentService = new CounteragentService();
        private readonly LocationService locationService = new LocationService();
        private readonly EmployeeService employeeService = new EmployeeService();
        private readonly SaleService saleService = new SaleService();
        private readonly StockService stockService = new StockService();

        public CreateSaleViewModel()
        {
            AddItemCommand = new RelayCommand(AddItem);
            SubmitSaleCommand = new RelayCommand(async () => await SubmitSale());
            PrintReceiptCommand = new RelayCommand(PrintReceipt);
        }

        public async Task Initialize()
        {
            var stores = await locationService.GetLocations();
            Stores = new ObservableCollection<Location>(stores.Where(l => l.IsStore));
            OnPropertyChanged(nameof(Stores));

            var clients = await counteragentService.GetCouteragent(true);
            Clients = new ObservableCollection<Counteragent>(clients);
            OnPropertyChanged(nameof(Clients));

            var employees = await employeeService.GetEmployees();

            foreach (var item in SaleItems)
            {
                item.PropertyChanged += SaleItem_PropertyChanged;
            }

        }

        private async void LoadStockProducts()
        {
            if (SelectedStore == null) return;

            var stocks = await stockService.GetStocksByStore(SelectedStore.Code);
            foreach (var stock in stocks)
            {
                if (string.IsNullOrWhiteSpace(stock.Product.Image))
                    Debug.WriteLine($"[DEBUG] У товара {stock.Product.Name} не указано изображение");
            }
            StockProducts = new ObservableCollection<Stock>(stocks);
            OnPropertyChanged(nameof(StockProducts));
        }


        private async void LoadSellersForStore()
        {
            if (SelectedStore == null) return;

            var employees = await employeeService.GetEmployees();
            var sellers = employees.Where(e => e.Location?.Code == SelectedStore.Code && e.Position.Name.ToLower() == "продавец").ToList();
            Sellers = new ObservableCollection<Employee>(sellers);

            OnPropertyChanged(nameof(Sellers));
        }

        private void AddItem()
        {
            var newItem = new SaleItemModel();
            newItem.PropertyChanged += SaleItem_PropertyChanged;
            SaleItems.Add(newItem);
            OnPropertyChanged(nameof(TotalSum));
        }

        private void SaleItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SaleItemModel.Quantity) ||
                e.PropertyName == nameof(SaleItemModel.Price) ||
                e.PropertyName == nameof(SaleItemModel.Product))
            {
                OnPropertyChanged(nameof(TotalSum));
            }
        }

        private async Task SubmitSale()
        {
            if (SelectedStore == null || SelectedClient == null || SaleItems.Count == 0)
            {
                MessageBox.Show("Заполните все обязательные поля и добавьте товары.");
                return;
            }

            if (SaleItems.Any(i => i.Product == null || i.Quantity <= 0))
            {
                MessageBox.Show("Убедитесь, что все товары выбраны и указано корректное количество.");
                return;
            }

            var salePost = new SalePost
            {
                StoreId = SelectedStore.Code,
                ClientId = SelectedClient.Code,
                SellerId = SelectedSeller?.Id,
                SaleItems = new ObservableCollection<SaleItemPost>(
                    SaleItems.Select(i => i.ToPost()))
            };

            var sale = await saleService.MakeSale(salePost);
            if (sale != null)
            {
                createdSale = sale;
                MessageBox.Show("Продажа успешно оформлена!");
                CanPrint = true;
                OnPropertyChanged(nameof(CanPrint));
            }
        }

        private void PrintReceipt()
        {
            if (createdSale == null)
            {
                MessageBox.Show("Нет данных для печати чека.");
                return;
            }

            var dialog = new SaveFileDialog
            {
                FileName = $"Чек_{createdSale.Id}.pdf",
                DefaultExt = ".pdf",
                Filter = "PDF документы (*.pdf)|*.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                var path = dialog.FileName;

                try
                {
                    var printer = new ReceiptPrinter();
                    printer.GenerateReceiptPdf(createdSale, path);
                    MessageBox.Show("Чек сохранён.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении чека: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
