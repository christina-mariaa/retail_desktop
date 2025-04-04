using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using RetailDesktop.Views;
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
    public class CounteragentsViewModel
    {
        public ObservableCollection<Counteragent> Counteragents { get; set; }
        private CounteragentService counteragentService { get; set; }
        public ICommand LoadClientsCommand { get; set; }
        public ICommand LoadSuppliersCommand { get; set; }
        public ICommand AddClientCommand { get; set; }
        public ICommand AddSupplierCommand { get; set; }

        public CounteragentsViewModel() 
        { 
            Counteragents = new ObservableCollection<Counteragent>();
            LoadClientsCommand = new RelayCommand(async () => await LoadClients());
            LoadSuppliersCommand = new RelayCommand(async () => await LoadSuppliers());
            AddClientCommand = new RelayCommand(async () => await AddClient());
            AddSupplierCommand = new RelayCommand(async () => await AddSupplier());
            counteragentService = new CounteragentService();
        }

        public async Task LoadClients()
        {
            List<Counteragent> clients = await counteragentService.GetCouteragent(IsClient: true);

            Counteragents.Clear();

            foreach (var client in clients)
            {
                Counteragents.Add(client);
            }
        }

        public async Task LoadSuppliers()
        {
            List<Counteragent> suppliers = await counteragentService.GetCouteragent(IsClient: false);

            Counteragents.Clear();

            foreach (var supplier in suppliers)
            {
                Counteragents.Add(supplier);
            }
        }

        private async Task AddClient()
        {
            var window = new AddCounteragentWindow();

            bool? result = window.ShowDialog();

            if (result == true) 
            {
                Counteragent newCounteragent = window.NewCounteragent;
                newCounteragent.IsSupplier = false;
                bool success = await counteragentService.AddCounteragent(IsClient: true, newCounteragent);

                if (success == true)
                {
                    Counteragents.Add(newCounteragent);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении клиента");
                }
            }
        }

        private async Task AddSupplier()
        {
            var window = new AddCounteragentWindow();

            bool? result = window.ShowDialog();

            if (result == true)
            {
                Counteragent newCounteragent = window.NewCounteragent;
                newCounteragent.IsSupplier = true;
                bool success = await counteragentService.AddCounteragent(IsClient: false, newCounteragent);

                if (success == true)
                {
                    Counteragents.Add(newCounteragent);
                }
            }
        }
    }
}
