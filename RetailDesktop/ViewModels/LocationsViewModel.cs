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
    public class LocationsViewModel
    {
        public ObservableCollection<Location> Locations { get; private set; }
        public ICommand LoadLocationsCommand {  get; private set; }
        public ICommand AddLocationCommand {  get; private set; }
        private readonly LocationService locationService;

        public LocationsViewModel ()
        {
            Locations = new ObservableCollection<Location> ();
            locationService = new LocationService ();
            LoadLocationsCommand = new RelayCommand(LoadLocations);
            AddLocationCommand = new RelayCommand(AddLocation);
            LoadLocations();
        }

        public async void LoadLocations()
        {
            List<Location> locations = await locationService.GetLocations ();

            Locations.Clear ();

            foreach (Location location in locations)
            {
                Locations.Add(location);
            }
        }

        public async void AddLocation()
        {
            var window = new AddLocationWindow();

            bool? result = window.ShowDialog();

            if (result == true)
            {
                Location newLocation = window.NewLocation;
                bool success = await locationService.AddLocation(newLocation);

                if (success)
                {
                    Locations.Add(newLocation);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении магазина/склада на сервер.");
                }

            }
        }
    }
}
