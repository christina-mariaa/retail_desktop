using RetailDesktop.Helpers;
using RetailDesktop.Services;
using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http;

namespace RetailDesktop.ViewModels
{
    public class PopularProductsReportViewModel : ViewModelBase
    {
        private readonly ReportService reportService = new ReportService();

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public ObservableCollection<PopularProductReportItem> PopularProducts { get; set; } = new ObservableCollection<PopularProductReportItem>();

        public ICommand LoadDataCommand { get; }

        public PopularProductsReportViewModel()
        {
            LoadDataCommand = new RelayCommand(async () => await LoadData());
        }

        private async Task LoadData()
        {
            if (StartDate == null || EndDate == null)
            {
                MessageBox.Show("Выберите обе даты.");
                return;
            }

            try
            {
                var result = await reportService.GetPopularProducts(StartDate.Value, EndDate.Value);
                if (result == null)
                {
                    MessageBox.Show("Сервер вернул пустой результат.");
                    return;
                }

                PopularProducts.Clear();

                foreach (var item in result.Take(10))
                {
                    if (item?.Product == null || string.IsNullOrWhiteSpace(item.Product.Name))
                    {
                        MessageBox.Show("Один из товаров не содержит имя. Проверьте данные.");
                        continue;
                    }

                    PopularProducts.Add(item);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show("Ошибка HTTP: " + httpEx.Message);
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show("Ошибка при разборе ответа: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка: " + ex.Message);
            }
        }

    }
}
