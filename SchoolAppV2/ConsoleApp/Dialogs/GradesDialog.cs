using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Dialogs
{
    internal class GradesDialog:DialogBase
    {
        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Grades");

            var menu = Menu.MenuOptionsBuilder()
                .AddMenuOption("Grades since last month", SwitchTo<GradedSinceLastMonthDialog>())
                .AddMenuOption("Show average, max and min grades of every Courses", SwitchTo<GradesCourseListDialog>())
                .AddMenuOption("return Back", ReturnBack())
                .Build();



            return AnsiConsole.Prompt(menu).Value as DialogLocationData ?? Refresh();
        }
    }
}
