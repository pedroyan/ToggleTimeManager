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

            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Namespace == typeof(IFilePicker).Namespace)
                .AsImplementedInterfaces();

            builder.RegisterType<FileSelectionViewModel>();

            var window = new MainWindow();
            window.Loaded += (s, args) =>
            {
                builder.Register(c => new PageNavigationService(window.MainFrame.NavigationService))
                    .As<IPageNavigationService>();
            };
            window.Show();

            var scope = builder.Build();

            IoC.SetScope(scope);

            //TODO: Configure IOC here with AUTOFAC
            //TODO: After IOC is configured, set the navigation service.
        }
    }
}
