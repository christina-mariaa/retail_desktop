using Newtonsoft.Json;
using RetailDesktop.Helpers;
using RetailDesktop.Models;
using RetailDesktop.Services;
using RetailDesktop.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RetailDesktop.ViewModels
{
    public class EmployeesViewModel
    {
        public ObservableCollection<Employee> Employees {  get; private set; }  
        public ICommand LoadEmployeesCommand { get; private set; }
        public ICommand AddEmployeeCommand { get; private set; }
        private EmployeeService employeeService;

        public EmployeesViewModel()
        {
            Employees = new ObservableCollection<Employee>();
            employeeService = new EmployeeService();
            LoadEmployeesCommand = new RelayCommand(LoadEmployees);
            AddEmployeeCommand = new RelayCommand(AddEmployee);
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            List<Employee> employees = employeeService.GetEmployees();

            Employees.Clear();

            foreach (Employee employee in employees)
            {
                Employees.Add(employee);
            }
        }

        private void AddEmployee()
        {
            var window = new AddEmployeeWindow();
            bool? result = window.ShowDialog();

            if (result == true) 
            {
                Employee newEmployee = window.NewEmployee;
                bool success = employeeService.AddEmployee(newEmployee);

                if (success == true)
                {
                    Employees.Add(newEmployee);
                }

                else
                {
                    MessageBox.Show("Ошибка при добавлении сотрудника");
                }
            }
        }
    }
}
