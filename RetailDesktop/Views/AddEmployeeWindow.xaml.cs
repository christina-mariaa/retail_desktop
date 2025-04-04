using RetailDesktop.Models;
using RetailDesktop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RetailDesktop.Views
{
    public partial class AddEmployeeWindow : Window
    {
        public Employee NewEmployee { get; private set; }

        public AddEmployeeWindow()
        {
            InitializeComponent();
            LoadPositions();
            Loaded += async (s, e) =>
            {
                await LoadLocations();
                LoadPositions();
            };
        }

        private async Task LoadLocations()
        {
            var locationService = new LocationService();
            List<Location> locations = await locationService.GetLocations();
            LocationBox.ItemsSource = locations;
        }

        private void LoadPositions()
        {
            var employeeService = new EmployeeService();
            List<Position> positions = employeeService.GetPositions();
            PositionBox.ItemsSource = positions;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPosition = PositionBox.SelectedItem as Position;
            var selectedLocation = LocationBox.SelectedItem as Location;

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(SurnameBox.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedPosition == null)
            {
                MessageBox.Show("Выберите должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedLocation == null)
            {
                MessageBox.Show("Выберите магазин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewEmployee = new Employee
            {
                Name = NameBox.Text.Trim(),
                Surname = SurnameBox.Text.Trim(),
                MiddleName = string.IsNullOrWhiteSpace(MiddleNameBox.Text) ? null : MiddleNameBox.Text.Trim(),
                PositionId = selectedPosition.Id,
                LocationCode = selectedLocation.Code,
                Position = new Position
                {
                    Id = selectedPosition.Id,
                    Name = selectedPosition.Name,
                },
                Location = new Location
                {
                    Code = selectedLocation.Code,
                    Address = selectedLocation.Address,
                },
            };

            DialogResult = true;
            Close();
        }

    }
}
