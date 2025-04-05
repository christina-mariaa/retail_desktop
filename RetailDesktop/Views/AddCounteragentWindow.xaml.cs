using RetailDesktop.Models;
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
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddCounteragentWindow : Window
    {
        public Counteragent NewCounteragent { get; set; }
        public AddCounteragentWindow()
        {
            InitializeComponent();
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CodeBox.Text))
            {
                MessageBox.Show("Введите код", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(ContactInfoBox.Text))
            {
                MessageBox.Show("Введите контактную информацию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewCounteragent = new Counteragent
            {
                Code = CodeBox.Text,
                Name = NameBox.Text,
                ContactInfo = ContactInfoBox.Text,
            };

            DialogResult = true;
            Close();
        }
    }
}
