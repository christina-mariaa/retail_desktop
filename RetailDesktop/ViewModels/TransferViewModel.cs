using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RetailDesktop.ViewModels
{
    public class TransferViewModel : ViewModelBase
    {
        public Transfer Transfer { get; set; }
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Location> FromLocations { get; set; }
        public ObservableCollection<Location> ToLocations { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        private Location _selectedFromLocation;
        public Location SelectedFromLocation { 
            get => _selectedFromLocation; 
            set
            {
                if (SetProperty(ref _selectedFromLocation, value)) 
                {
                    OnPropertyChanged(nameof(IsFromLocationSelected));
                    UpdateToLocations();
                }
            } 
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand MakeTransferCommand { get; set; }

        public Location SelectedToLocation { get; set; }
        public ObservableCollection<TransferItem> TransferItems { get; set; }

        public bool IsFromLocationSelected => _selectedFromLocation != null;

        private readonly LocationService locationService;
        private readonly ProductService productService;
        private readonly TransferService transferService;

        public TransferViewModel() 
        { 
            Locations = new ObservableCollection<Location>();
            FromLocations = new ObservableCollection<Location>();
            ToLocations = new ObservableCollection<Location>();
            Transfer = new Transfer();
            TransferItems = Transfer.Items;
            Products = new ObservableCollection<Product>();

            AddItemCommand = new RelayCommand(AddItem);
            MakeTransferCommand = new RelayCommand(async () => await MakeTransfer());

            locationService = new LocationService();
            productService = new ProductService();
            transferService = new TransferService();
        }

        private void AddItem()
        {
            TransferItems.Add(new TransferItem());
        }

        private async Task MakeTransfer()
        {
            if (!ValidateFields()) return;

            Transfer.FromLocationId = SelectedFromLocation?.Code;
            Transfer.ToLocationId = SelectedToLocation?.Code;

            bool result = await transferService.MakeTransfer(Transfer);
            if (result)
            {
                MessageBox.Show("Товары успешно перемещены!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Transfer.Items.Clear();
            }
        }
        private bool ValidateFields()
        {
            if (SelectedFromLocation == null)
            {
                MessageBox.Show("Выберите склад/магазин отправитель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (SelectedToLocation == null)
            {
                MessageBox.Show("Выберите склад/магазин получатель", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (SelectedFromLocation.Code == SelectedToLocation.Code)
            {
                MessageBox.Show("Отправитель и получатель не могут быть одинаковыми", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TransferItems.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (var item in TransferItems)
            {
                if (item.ProductId <= 0)
                {
                    MessageBox.Show("У одного из товаров не выбран продукт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (item.Quantity <= 0)
                {
                    MessageBox.Show("Укажите количество больше 0 для товара с ID: " + item.ProductId, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }

        private void UpdateToLocations()
        {
            ToLocations.Clear();

            foreach (var location in Locations)
            {
                if (SelectedFromLocation == null || location.Code != SelectedFromLocation.Code)
                {
                    ToLocations.Add(location);
                }
            }

            SelectedToLocation = null;
            OnPropertyChanged(nameof(SelectedToLocation));
        }

        public async Task InitializeAsync()
        {
            var loadedLocations = await locationService.GetLocations();
            Locations = new ObservableCollection<Location>(loadedLocations);

            FromLocations.Clear();
            ToLocations.Clear();

            foreach (var loc in Locations)
            {
                FromLocations.Add(loc);
                ToLocations.Add(loc);
            }

            var loadedProducts = await productService.GetProducts();

            Products.Clear();

            foreach(var product in loadedProducts)
            { 
                Products.Add(product); 
            }

            OnPropertyChanged(nameof(Locations));
            OnPropertyChanged(nameof(FromLocations));
            OnPropertyChanged(nameof(ToLocations));
            OnPropertyChanged(nameof(Products));
        }
    }
}
