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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views
{
    /// <summary>
    /// Interaction logic for FileSelectionPage.xaml
    /// </summary>
    public partial class FileSelectionPage : Page
    {
        public FileSelectionPage()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //TODO: Resolve Navigation service from IOC instead of using this OnLoaded method
            DataContext = new FileSelectionViewModel(new FilePicker(), new PageNavigationService(NavigationService));
        }
    }
}
