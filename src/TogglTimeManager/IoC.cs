using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using TogglTimeManager.Services;

namespace TogglTimeManager
{
    public static class IoC
    {
        //TODO: study services lifecycle with Autofac (https://autofac.readthedocs.io/en/latest/resolve/index.html)
        //From the docs: While it is possible to resolve components right from the root container, doing this through your application in some cases may result in a memory leak
        //https://autofac.readthedocs.io/en/latest/lifetime/index.html

        private static ILifetimeScope _scope;

        /// <summary>
        /// Sets the IoC root scope
        /// </summary>
        public static void SetScope(ILifetimeScope scope)
        {
            _scope = scope;
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
