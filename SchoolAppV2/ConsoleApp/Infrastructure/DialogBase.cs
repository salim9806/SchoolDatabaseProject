using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public abstract class DialogBase
    {
        public abstract DialogLocationData DisplayContent();

        protected DialogLocationData SwitchTo<T>()where T : class
        {
            return new DialogLocationData 
            { 
                Source =  this.GetType(),
                Destination= typeof(T), 
                Action = SourceRequest.Switch 
            };
        }

        protected DialogLocationData ReturnBack()
        {
            return new DialogLocationData 
            { 
                Source = this.GetType(), 
                Destination = this.GetType(), 
                Action = SourceRequest.Return 
            };
        }

        protected DialogLocationData Refresh()
        {
            return new DialogLocationData
            {
                Source = this.GetType(),
                Destination = this.GetType(),
                Action = SourceRequest.Refresh
            };
        }

        protected DialogLocationData Exit()
        {
            return new DialogLocationData
            {
                Source = this.GetType(),
                Destination = this.GetType(),
                Action = SourceRequest.Exit
            };
        }

        protected DialogLocationData Rerender()
        {
            return new DialogLocationData
            {
                Source = this.GetType(),
                Destination = this.GetType(),
                Action = SourceRequest.Rerender
            };
        }


    }
}
