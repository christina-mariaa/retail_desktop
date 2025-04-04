using RetailDesktop.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetailDesktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ProductsView();
        }

        private void ShowLocations(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new LocationsView();
        }

        private void ShowEmployees(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new EmployeesView();
        }

        private void ShowClients(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ClientsView();
        }

        private void ShowSuppliers(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new SuppliersView();
        }

        private void ShowOrders(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new OrdersListView();
        }
        private void ShowPurchases(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new PurchaseView();
        }

        private void ShowTransfers(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new TransferView();
        }
    }
}
