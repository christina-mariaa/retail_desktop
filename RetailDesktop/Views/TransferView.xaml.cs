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
    public partial class TransferView : UserControl
    {
        private readonly TransferViewModel viewModel;

        public TransferView()
        {
            InitializeComponent();

            viewModel = new TransferViewModel();
            DataContext = viewModel;

            Loaded += async (s, e) => await viewModel.InitializeAsync();
        }
    }
}
