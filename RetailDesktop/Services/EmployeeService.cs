using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetailDesktop.Services
{
    public class EmployeeService
    {
        private HttpClient httpClient {  get; set; }
        public EmployeeService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            string url = Constants.BaseUrl + "employees/";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(json);
                    return employees;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка сотрудников:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<Employee>();
        }

        public async Task<List<Position>> GetPositions()
        {
            string url = Constants.BaseUrl + "employees-positions/";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<Position> positions = JsonConvert.DeserializeObject<List<Position>>(json);
                    return positions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки списка должностей:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new List<Position>();
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            string url = Constants.BaseUrl + "employees/";

            try
            {
                string json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления сотрудника:" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
