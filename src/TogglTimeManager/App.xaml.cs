using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using TogglTimeManager.Core;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;
using TogglTimeManager.Views;
using TogglTimeManager.Views.MainDashboard;
using TogglTimeManager.Views.NewSheet;

namespace TogglTimeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.RegisterServices();

            var vm = new NewSheetWindowViewModel();
            var window = new NewSheetWindow(vm);
            window.Show();

            vm.TimeSheetCompleted += (s, ea) =>
            {
                var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), ea);
                new MainDashboard(new MainDashboardViewModel(summary)).Show();
                window.Close();
            };
        }
    }
}
