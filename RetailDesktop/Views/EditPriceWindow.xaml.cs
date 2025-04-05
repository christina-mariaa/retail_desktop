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
    public partial class EditPriceWindow : Window
    {
        public decimal NewPrice { get; private set; }
        private readonly string productCode;

        public EditPriceWindow(string productCode)
        {
            InitializeComponent();
            this.productCode = productCode;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PriceBox.Text, out decimal price))
            {
                NewPrice = price;

                var service = new ProductService();
                var success = await service.PatchProductPriceAsync(productCode, price);

                if (success)
                {
                    MessageBox.Show("Цена успешно обновлена");
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка обновления цены");
                }
            }
            else
            {
                MessageBox.Show("Введите корректную цену");
            }
        }
    }
}
