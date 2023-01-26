using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public enum SourceRequest
    {
        Switch,
        Return,
        Refresh,
        Exit,
        Rerender
    }

    public class DialogLocationData
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
        public SourceRequest Action { get; set; }
    }
}
