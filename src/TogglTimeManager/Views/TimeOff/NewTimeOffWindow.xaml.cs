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

namespace TogglTimeManager.Views.TimeOff
{
    /// <summary>
    /// Interaction logic for NewTimeOff.xaml
    /// </summary>
    public partial class NewTimeOffWindow : Window
    {
        public NewTimeOffWindow(NewTimeOffViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}