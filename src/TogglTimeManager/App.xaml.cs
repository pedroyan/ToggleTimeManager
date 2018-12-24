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
using Autofac.Core;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;
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
        private IUserRepository _userRepository;
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.RegisterServices();
            _userRepository = IoC.Resolve<IUserRepository>();
            var userInfo = await _userRepository.GetUserInfo();

            if (userInfo == null)
            {
                InitialSetup();
            }
            else
            {
                var vm = new MainDashboardViewModel(userInfo.Summary, _userRepository, IoC.Resolve<IWindowService>());
                new MainDashboard(vm).Show();
            }
        }

        private void InitialSetup()
        {
            var vm = new NewSheetWindowViewModel();
            var window = new NewSheetWindow(vm);
            window.Show();

            vm.TimeSheetCreated += (s, ea) =>
            {
                var userInfo = new UserInfo()
                {
                    Summary = TimeSummaryCalculator.CalculateHoursSummary(ea.WorkContract, ea.TimeSheet, null),
                    WorkContract = ea.WorkContract
                };

                //If this step fails, application should crash
                _userRepository.Persist(userInfo);

                var mdvm = new MainDashboardViewModel(userInfo.Summary, _userRepository, IoC.Resolve<IWindowService>());
                new MainDashboard(mdvm).Show();

                window.Close();
            };
        }
    }
}
