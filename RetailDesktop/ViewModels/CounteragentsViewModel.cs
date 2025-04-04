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
            LoadClientsCommand = new RelayCommand(LoadClients);
            LoadSuppliersCommand = new RelayCommand(LoadSuppliers);
            AddClientCommand = new RelayCommand(AddClient);
            AddSupplierCommand = new RelayCommand(AddSupplier);
            counteragentService = new CounteragentService();
        }

        private void LoadClients()
        {
            List<Counteragent> clients = counteragentService.GetCouteragent(IsClient: true);

            Counteragents.Clear();

            foreach (var client in clients)
            {
                Counteragents.Add(client);
            }
        }

        private void LoadSuppliers()
        {
            List<Counteragent> suppliers = counteragentService.GetCouteragent(IsClient: false);

            Counteragents.Clear();

            foreach (var supplier in suppliers)
            {
                Counteragents.Add(supplier);
            }
        }

        private void AddClient()
        {
            var window = new AddCounteragentWindow();

            bool? result = window.ShowDialog();

            if (result == true) 
            {
                Counteragent newCounteragent = window.NewCounteragent;
                newCounteragent.IsSupplier = false;
                bool success = counteragentService.AddCounteragent(IsClient: true, newCounteragent);

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

        private void AddSupplier()
        {
            var window = new AddCounteragentWindow();

            bool? result = window.ShowDialog();

            if (result == true)
            {
                Counteragent newCounteragent = window.NewCounteragent;
                newCounteragent.IsSupplier = true;
                bool success = counteragentService.AddCounteragent(IsClient: false, newCounteragent);

                if (success == true)
                {
                    Counteragents.Add(newCounteragent);
                }
            }
        }
    }
}
