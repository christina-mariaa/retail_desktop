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

namespace RetailDesktop.ViewModels
{
    public class LocationsViewModel
    {
        public ObservableCollection<Location> Locations { get; private set; }
        public ICommand LoadLocationsCommand {  get; private set; }
        private readonly LocationService locationService;

        public LocationsViewModel ()
        {
            Locations = new ObservableCollection<Location> ();
            locationService = new LocationService ();
            LoadLocationsCommand = new RelayCommand(LoadLocations);
        }

        public void LoadLocations()
        {
            List<Location> locations = locationService.GetLocations ();

            Locations.Clear ();

            foreach (Location location in locations)
            {
                Locations.Add(location);
            }
        }
    }
}
