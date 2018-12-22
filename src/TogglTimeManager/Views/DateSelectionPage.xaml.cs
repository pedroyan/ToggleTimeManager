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
using Autofac;
using Autofac.Core;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager.Views
{
    /// <summary>
    /// Interaction logic for DateSelectionPage.xaml
    /// </summary>
    public partial class DateSelectionPage : Page
    {
        public DateSelectionPage(TimeSheet timeSheet, IPageNavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new DateSelectionViewModel(timeSheet, navigationService);
        }
    }
}
