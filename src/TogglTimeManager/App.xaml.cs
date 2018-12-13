﻿using System;
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
            IoC.RegisterServices(builder);

            var window = new MainWindow();
            window.Loaded += (s, args) =>
            {
                //This can be done on a better way that does not leak infrastructure details into other parts
                //of the code but no need to over-complicate this simple software for now
                builder.Register(c => new PageNavigationService(window.MainFrame.NavigationService))
                    .As<IPageNavigationService>();
            };
            window.Show();

            var scope = builder.Build();
            IoC.SetScope(scope);
        }
    }
}
