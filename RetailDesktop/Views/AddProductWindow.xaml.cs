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
    public partial class AddProductWindow : Window
    {
        public Product NewProduct { get; set; }
        public AddProductWindow()
        {
            InitializeComponent();
            LoadCategories();
        }
        private void LoadCategories()
        {
            var productService = new ProductService();
            List<ProductCategory> categories = productService.GetProductCategories();
            CategoryBox.ItemsSource = categories;
        }

        private void SaveButton_click(object sender, RoutedEventArgs e)
        {
            var selectedCategory = CategoryBox.SelectedItem as ProductCategory;
            if (CodeBox.Text == "")
            {
                MessageBox.Show("Введите код продукта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NameBox.Text == "")
            {
                MessageBox.Show("Введите название продукта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (decimal.TryParse(PriceBox.Text, out decimal price) && price > 0)
            {
                NewProduct = new Product
                {
                    Code = CodeBox.Text,
                    Name = NameBox.Text,
                    Price = price,
                    Brand = BrandBox.Text,
                    Category = selectedCategory?.Id,
                };

                DialogResult = true;
                Close();

            }
            else
            {
                MessageBox.Show("Введите корректное значение стоимости.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
