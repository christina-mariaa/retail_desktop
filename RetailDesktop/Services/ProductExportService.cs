using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using System.Windows;

namespace RetailDesktop.Services
{
    public class ProductExportService
    {
        private readonly ProductService productService = new ProductService();
        public async Task ExportProductsToFolder()
        {
            List<Product> products = await productService.GetProducts();
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Выберите папку для выгрузки"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string selectedPath = dialog.FileName;
                foreach (var product in products)
                {
                    string categoryName = product.Category?.Name ?? "Без категории";
                    string categoryPath = Path.Combine(selectedPath, SanitizeFileName(categoryName));
                    Directory.CreateDirectory(categoryPath);

                    string productFolderName = product.FullName;
                    string productPath = Path.Combine(categoryPath, SanitizeFileName(productFolderName));
                    Directory.CreateDirectory(productPath);

                    string infoFile = Path.Combine(productPath, "info.txt");

                    File.WriteAllText(infoFile,
                        $"Код: {product.Code}{Environment.NewLine}" +
                        $"Наименование: {product.Name}{Environment.NewLine}" +
                        $"Бренд: {product.Brand}"
                    );
                }

                MessageBox.Show("Выгрузка товаров завершена", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
