using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RetailDesktop.ViewModels 
{
    public class MakeOrderViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Counteragent> Clients { get; set; }
        public ObservableCollection<Employee> OrderPickers { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Employee> DeliveryDrivers { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<OrderItemPost> OrderItems { get; set; }

        private Location selectedStore;
        public Location SelectedStore
        {
            get { return selectedStore; }
            set
            {
                if (SetProperty(ref selectedStore, value))
                {
                    LoadDeliveryDriversForStore();
                    LoadOrderPickersForStore();
                    OnPropertyChanged(nameof(IsStoreSelected));
                }
            }
        }
    
        public Counteragent SelectedClient { get; set; }
        public Employee SelectedDriver { get; set; }
        public Employee SelectedPicker { get; set; }

        public string DeliveryAddress { get; set; }
        public string Comment { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.Today;

        public ICommand AddProductCommand { get; }
        public ICommand SubmitOrderCommand { get; }

        public Action CloseAction { get; set; }


        private readonly OrderService orderService;
        private readonly LocationService locationService;
        private readonly EmployeeService employeeService;
        private readonly ProductService productService;
        private readonly CounteragentService counteragentService;

        public bool IsStoreSelected => SelectedStore != null;


        public MakeOrderViewModel()
        {            
            Locations = new ObservableCollection<Location>();
            Clients = new ObservableCollection<Counteragent>();
            Employees = new ObservableCollection<Employee>();
            OrderPickers = new ObservableCollection<Employee>();
            DeliveryDrivers = new ObservableCollection<Employee>();
            Products = new ObservableCollection<Product>();

            OrderItems = new ObservableCollection<OrderItemPost>();

            AddProductCommand = new RelayCommand(AddProduct);
            SubmitOrderCommand = new RelayCommand(async () => await SubmitOrder());

            orderService = new OrderService();
            locationService = new LocationService();
            employeeService = new EmployeeService();
            productService = new ProductService();
            counteragentService = new CounteragentService();
            
        }

        public async Task InitializeAsync()
        {
            var loadedLocations = await locationService.GetLocations();
            Locations.Clear();
            foreach (var loc in loadedLocations)
            {
                Locations.Add(loc);
            }

            var loadedEmployees = await employeeService.GetEmployees();
            Employees.Clear();
            foreach (var emp in loadedEmployees)
            {
                Employees.Add(emp);
            }

            var loadedProducts = await productService.GetProducts();
            Products.Clear();
            foreach (var product in loadedProducts)
            {
                Products.Add(product);
            }

            var loadedClients = await counteragentService.GetCouteragent(true);

            Clients.Clear();
            foreach (var client in loadedClients)
            {
                Clients.Add(client);
            }

            OnPropertyChanged(nameof(Locations));
            OnPropertyChanged(nameof(Employees));
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(Clients));
        }

        private void AddProduct()
        {
            OrderItems.Add(new OrderItemPost());
        }

        private void LoadDeliveryDriversForStore()
        {
            if (SelectedStore == null)
                return;

            var drivers = Employees
                .Where(x => x.Position?.Name?.ToLower() == "курьер" &&
                            x.Location.Code == SelectedStore.Code)
                .ToList();


            DeliveryDrivers.Clear();
            foreach (var driver in drivers)
            {
                DeliveryDrivers.Add(driver);
            }
        }

        private void LoadOrderPickersForStore()
        {
            if (SelectedStore == null)
                return;

            var oderPickers = Employees
                .Where(x => x.Position?.Name?.ToLower() == "сборщик" &&
                            x.Location.Code == SelectedStore.Code)
                .ToList();

            OrderPickers.Clear();
            foreach (var oderPicker in oderPickers)
            {
                OrderPickers.Add(oderPicker);
            }
        }

        private async Task SubmitOrder()
        {
            if (SelectedStore == null)
            {
                MessageBox.Show("Выберите магазин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedClient == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(DeliveryAddress))
            {
                MessageBox.Show("Введите адрес доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DeliveryDate < DateTime.Today)
            {
                MessageBox.Show("Дата доставки не может быть в прошлом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newOrder = new OrderPost
            {
                StoreId = SelectedStore.Code,
                ClientId = SelectedClient.Code,
                DeliveryAddress = DeliveryAddress,
                DeliveryDate = DeliveryDate.Date,
                Comment = Comment,
                DeliveryDriverId = SelectedDriver?.Id,
                OrderPickerId = SelectedPicker?.Id,
                OrderItems = OrderItems.ToList()
            };

            bool success = await orderService.MakeOrder(newOrder);

            if (success)
            {
                MessageBox.Show("Заказ успешно создан", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
