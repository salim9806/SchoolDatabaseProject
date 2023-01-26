using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class DialogRouter
    {
        Stack<DialogLocationData> _history;

        public DialogRouter()
        {
            _history = new Stack<DialogLocationData>();
        }

        public void StartRouterAt<T>(IServiceProvider serviceProvider) where T:DialogBase
        {
            IServiceScope scope = serviceProvider.CreateScope();
            DialogBase dialog = scope.ServiceProvider.GetRequiredService<T>();
            DialogLocationData location;
            bool running = true;

            while (running)
            {
                Console.Clear();
                location = dialog.DisplayContent();


                switch (location.Action)
                {
                    case SourceRequest.Switch:
                        scope.Dispose();
                        scope = serviceProvider.CreateScope();

                        dialog = GetDialog(scope, location.Destination);
                        _history.Push(location);
                        
                        continue;
                    case SourceRequest.Return:
                        if (_history.TryPop(out DialogLocationData? previous))
                        {
                            scope.Dispose();
                            scope = serviceProvider.CreateScope();

                            dialog = GetDialog(scope, previous.Source); 
                        }
                        continue;
                    case SourceRequest.Refresh:
                        scope.Dispose();
                        scope = serviceProvider.CreateScope();
                        dialog = GetDialog(scope, location.Source);
                        continue;
                    case SourceRequest.Rerender:
                        continue;
                    case SourceRequest.Exit:
                        running = false;
                        break;
                }
            }

            scope.Dispose();
            Console.Clear();
        } 

        private DialogBase GetDialog(IServiceScope scope, Type type)
        {
            return scope.ServiceProvider.GetRequiredService(type) as DialogBase;
        }


    }
}
