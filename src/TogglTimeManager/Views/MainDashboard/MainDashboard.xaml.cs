﻿using System;
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
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views.MainDashboard
{
    /// <summary>
    /// Interaction logic for MainDashboard.xaml
    /// </summary>
    public partial class MainDashboard : Window
    {
        public MainDashboard(MainDashboardViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
