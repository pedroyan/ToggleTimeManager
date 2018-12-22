using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;
using TogglTimeManager.Views;

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

            var window = new MainWindow();
            window.Show();

            //Dashboard idea: https://social.msdn.microsoft.com/Forums/vstudio/en-US/c7edafe9-d4ac-4bd8-ac25-f4482cfdaa75/dockpanel-stackpanel-or-grid?forum=wpf
        }
    }
}
