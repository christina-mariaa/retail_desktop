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
    public class PurchaseViewModel
    {
        public PurchaseModel Purchase { get; set; }
        public ObservableCollection<PurchaseItemModel> PurchaseItems { get; set; }

        public ObservableCollection<Counteragent> Suppliers { get; set; }
        public ObservableCollection<Location> Warehouses { get; set; }

        public ICommand AddItemCommand { get; set; }
        public ICommand SavePurchaseCommand { get; set; }

        private PurchaseService purchaseService;
        private CounteragentService counteragentService;
        private LocationService locationService;

        public PurchaseItemModel SelectedItem { get; set; }

        public PurchaseViewModel()
        {
            Purchase = new PurchaseModel();
            PurchaseItems = Purchase.PurchaseProducts;

            AddItemCommand = new RelayCommand(AddItem);
            SavePurchaseCommand = new RelayCommand(SavePurchase);

            purchaseService = new PurchaseService();
            counteragentService = new CounteragentService();
            locationService = new LocationService();

            Suppliers = new ObservableCollection<Counteragent>(counteragentService.GetCouteragent(false));
            Warehouses = new ObservableCollection<Location>(locationService.GetLocations().Where(l => !l.IsStore));
        }

        private void AddItem()
        {
            PurchaseItems.Add(new PurchaseItemModel());
        }

        private void SavePurchase()
        {
            if (!ValidateFields()) return;

            bool result = purchaseService.SendPurchase(Purchase);
            if (result)
            {
                MessageBox.Show("Поставка успешно добавлена!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Purchase.PurchaseProducts.Clear();
            }
        }

        private bool ValidateFields()
        {
            if (Purchase.Supplier <= 0)
            {
                MessageBox.Show("Укажите поставщика", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Purchase.Warehouse <= 0)
            {
                MessageBox.Show("Укажите склад", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (PurchaseItems.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            foreach (var item in PurchaseItems)
            {
                if (string.IsNullOrWhiteSpace(item.ProductCode))
                {
                    MessageBox.Show("У одного из товаров не заполнен код", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(item.ProductName))
                {
                    MessageBox.Show("У одного из товаров не заполнено название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (item.Amount <= 0)
                {
                    MessageBox.Show("Укажите количество больше 0 для товара '" + item.ProductCode + "'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                if (item.PriceForAnItem <= 0)
                {
                    MessageBox.Show("Укажите цену больше 0 для товара '" + item.ProductCode + "'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            return true;
        }
    }
}
