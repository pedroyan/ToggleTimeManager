﻿using System.Windows.Controls;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views.NewSheet
{
    /// <summary>
    /// Interaction logic for DateSelectionPage.xaml
    /// </summary>
    public partial class DateSelectionPage : Page
    {
        public DateSelectionPage(CompleteInformationViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}