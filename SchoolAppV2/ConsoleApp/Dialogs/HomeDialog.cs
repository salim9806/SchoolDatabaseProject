using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp.Dialogs
{
    internal class HomeDialog : DialogBase
    {

        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Home");

            var menu = Menu.MenuOptionsBuilder()
                .AddMenuOption("Students", SwitchTo<StudentsDialog>())
                .AddMenuOption("Personnel", SwitchTo<PersonnelDialog>())
                .AddMenuOption("Grades", SwitchTo<GradesDialog>())
                .AddMenuOption("\"How many teachers works in those different departments?\"", SwitchTo<TeachersInDeptDialog>())
                .AddMenuOption("\"Show information of all students\"", SwitchTo<AllStudentsDialog>())
                .AddMenuOption("\"Which courses are in active state?\"", SwitchTo<AllActiveCourseDialog>())
                .AddMenuOption("Shut down", Exit())
                .Build();

            var choice = AnsiConsole.Prompt(menu);

            return choice.Value as DialogLocationData ?? Refresh();
        }
    }
}
