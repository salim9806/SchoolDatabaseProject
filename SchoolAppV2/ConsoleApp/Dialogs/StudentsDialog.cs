using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class StudentsDialog : DialogBase
    {
        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Students");

            var menu = Menu.MenuOptionsBuilder()
                .AddMenuOption("List All Students", SwitchTo<StudentListDialog>())
                .AddMenuOption("List Students by Class", SwitchTo<StudentListByClassDialog>())
                .AddMenuOption("Add New Student", SwitchTo<StudentCreationDialog>())
                .AddMenuOption("Return Back", ReturnBack())
                .Build();

            return AnsiConsole.Prompt(menu).Value as DialogLocationData ?? Refresh();

        }
    }
}
