using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class PersonnelDialog : DialogBase
    {
        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Staffs");

            var menu = Menu.MenuOptionsBuilder()
                .AddMenuOption("List All Personnel", SwitchTo<PersonnelListAllDialog>())
                .AddMenuOption("Filter Personnel By Category", SwitchTo<PersonnelByOccupationDialog>())
                .AddMenuOption("Add New Staff", SwitchTo<PersonnelCreationDialog>())
                .AddMenuOption("return Back", ReturnBack())
                .Build();


            return AnsiConsole.Prompt(menu).Value as DialogLocationData ?? Refresh();
        }
    }
}
