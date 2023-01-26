using ConsoleApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddDialogs(this IServiceCollection services)
        {
            var dialogs = Assembly.GetCallingAssembly().GetTypes().Where(
                t => t.IsSubclassOf(typeof(DialogBase)));
            


            foreach(var dialog in dialogs)
            {
                services.AddTransient(dialog);
            }

            return services;
        }
    }
}
