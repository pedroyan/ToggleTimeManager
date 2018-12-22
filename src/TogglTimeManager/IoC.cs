using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TogglTimeManager.Services;
using TogglTimeManager.ViewModels;

namespace TogglTimeManager
{
    public static class IoC
    {
        //TODO: study services lifecycle with Autofac (https://autofac.readthedocs.io/en/latest/resolve/index.html)
        //From the docs: While it is possible to resolve components right from the root container, doing this through your application in some cases may result in a memory leak
        //https://autofac.readthedocs.io/en/latest/lifetime/index.html

        private static ILifetimeScope _scope;

        /// <summary>
        /// Register the core services and dependencies of the application
        /// </summary>
        public static void RegisterServices()
        {
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Namespace == typeof(IFilePicker).Namespace)
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Namespace == typeof(FileSelectionViewModel).Namespace);

            _scope = builder.Build();
        }

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <typeparam name="T">Type of the dependency being resolved</typeparam>
        /// <returns>The resolved dependency</returns>
        public static T Resolve<T>()
        {
            if (_scope == null)
            {
                throw new Exception("IoC hasn't been properly initialized");
            }

            return _scope.Resolve<T>();
        }

        /// <summary>
        /// Resolves a dependency
        /// </summary>
        /// <param name="parameters">Arguments provided to the dependency</param>
        /// <typeparam name="T">Type of the dependency being resolved</typeparam>
        /// <returns>The resolved dependency</returns>
        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_scope == null)
            {
                throw new Exception("IoC hasn't been properly initialized");
            }

            return _scope.Resolve<T>(parameters);
        }
    }
}
