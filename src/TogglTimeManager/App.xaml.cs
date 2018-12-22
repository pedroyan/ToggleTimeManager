using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.IsolatedStorage;
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
        private IUserRepository userRepository;
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.RegisterServices();
            userRepository = IoC.Resolve<IUserRepository>();
            var userInfo = await userRepository.GetUserInfo();

            if (userInfo == null)
            {
                InitialSetup();
            }
            else
            {
                new MainDashboard(new MainDashboardViewModel(userInfo.Summary)).Show();
            }
        }

        private void InitialSetup()
        {
            var vm = new NewSheetWindowViewModel();
            var window = new NewSheetWindow(vm);
            window.Show();

            vm.TimeSheetCompleted += (s, ea) =>
            {
                var workDayDuration = TimeSpan.FromHours(6);
                var userInfo = new UserInfo()
                {
                    Summary = TimeSummaryCalculator.CalculateHoursSummary(workDayDuration, ea)
                };

                userRepository.Persist(userInfo);

                new MainDashboard(new MainDashboardViewModel(userInfo.Summary)).Show();
                window.Close();
            };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
