using RetailDesktop.ViewModels;
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

namespace RetailDesktop.Views
{

    public partial class EmployeesView : UserControl
    {
        private EmployeesViewModel viewModel;
        public EmployeesView()
        {
            InitializeComponent();
            viewModel = new EmployeesViewModel();
            DataContext = viewModel;

            Loaded += async (s, e) => await viewModel.LoadEmployees();
        }
    }
}
