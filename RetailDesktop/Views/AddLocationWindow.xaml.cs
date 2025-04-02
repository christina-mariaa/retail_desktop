using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public partial class AddLocationWindow : Window
    {
        public Location NewLocation { get; set; }
        public AddLocationWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_click(object sender, RoutedEventArgs e)
        {
            if (CodeBox.Text == "")
            {
                MessageBox.Show("Введите код магазина/склада", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewLocation = new Location
            {
                Code = CodeBox.Text,
                Address = AddressBox.Text,
                IsStore = IsStoreBox.IsChecked ?? false,
                IsMain = IsMainBox.IsChecked ?? false,
            };

            DialogResult = true;
            Close();
        }

        private void IsStore_Checked(object sender, RoutedEventArgs e)
        {
            IsMainBox.IsEnabled = false;
        }
        private void IsStore_Unchecked (object sender, RoutedEventArgs e)
        {
            IsMainBox.IsEnabled = true;
        }
        private void IsMain_Checked(object sender, RoutedEventArgs e)
        {
            IsStoreBox.IsEnabled = false;
        }
        private void IsMain_Unchecked(object sender, RoutedEventArgs e)
        {
            IsStoreBox.IsEnabled = true;
        }
    }
}
