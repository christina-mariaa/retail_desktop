using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailDesktop.ViewModels
{
    public class TransferViewModel : ViewModelBase
    {
        public ObservableCollection<Location> Locations { get; set; }
        public ObservableCollection<Location> FromLocations { get; set; }
        public ObservableCollection<Location> ToLocations { get; set; }

        private Location _selectedFromLocation;
        public Location SelectedFromLocation { 
            get => _selectedFromLocation; 
            set
            {
                if (SetProperty(ref _selectedFromLocation, value)) 
                {
                    OnPropertyChanged(nameof(IsFromLocationSelected));
                }
            } 
        }

        public bool IsFromLocationSelected => _selectedFromLocation != null;
        private readonly LocationService locationService;

        public TransferViewModel() 
        { 
            Locations = new ObservableCollection<Location>();
            FromLocations = new ObservableCollection<Location>();
            ToLocations = new ObservableCollection<Location>();
            locationService = new LocationService();
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

            OnPropertyChanged(nameof(Locations));
            OnPropertyChanged(nameof(FromLocations));
            OnPropertyChanged(nameof(ToLocations));
        }
    }
}
