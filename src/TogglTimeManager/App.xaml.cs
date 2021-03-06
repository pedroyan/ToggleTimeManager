﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Core;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;
using TogglTimeManager.Views;
using TogglTimeManager.Views.MainDashboard;
using TogglTimeManager.Views.NewSheet;
using TogglTimeManager.Views.TimeOff;

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

            try
            {
                UserInfo userInfo = await _userRepository.GetUserInfo();
                var vm = new MainDashboardViewModel(userInfo.CalculateSummary(), _userRepository,
                    IoC.Resolve<IWindowService>());
                new MainDashboard(vm).Show();
            }
            catch (FileNotFoundException)
            {
                InitialSetup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application could not start due to a fatal error:\n\n{ex.Message}", "Fatal Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                Current.Shutdown();
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
                    TimeSheet = ea.TimeSheet,
                    WorkContract = ea.WorkContract
                };

                //If this step fails, application should crash
                _userRepository.Persist(userInfo);

                var mdvm = new MainDashboardViewModel(userInfo.CalculateSummary(), _userRepository, IoC.Resolve<IWindowService>());
                new MainDashboard(mdvm).Show();

                window.Close();
            };
        }
    }
}
