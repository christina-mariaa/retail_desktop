﻿using RetailDesktop.Models;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RetailDesktop.Views
{
    public partial class MakeOrderWindow : Window
    {
        private MakeOrderViewModel viewModel;
        public MakeOrderWindow()
        {
            InitializeComponent();
            viewModel = new MakeOrderViewModel();
            DataContext = viewModel;
            Loaded += async (s, e) => await viewModel.InitializeAsync();
            viewModel.CloseAction = new Action(this.Close);
        }
    }
}
