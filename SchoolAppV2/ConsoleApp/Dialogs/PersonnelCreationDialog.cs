using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.EntityFrameworkCore;
using Repository.Repos;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class PersonnelCreationDialog : DialogBase
    {
        private IPersonnelRepository _personnelRepository;
        private IOccupationRepository _occupationRepository;
        private IDepartmentRepository _departmentRepository;

        public PersonnelCreationDialog(IPersonnelRepository personnelRepository, IOccupationRepository occupationRepository, IDepartmentRepository departmentRepository)
        {
            _personnelRepository = personnelRepository;
            _occupationRepository = occupationRepository;
            _departmentRepository = departmentRepository;
        }
        public override DialogLocationData DisplayContent()
        {
            // displaying title
            Menu.DisplayTitle("Adding new staff");

            var firstname = AnsiConsole.Ask<string>("What would be the [green]first name[/]?");
            var lastname = AnsiConsole.Ask<string>("What would be the [green]last name[/]?");

            var pickedDepartment = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"In Which [green]deparment[/] should [green]{firstname} {lastname}[/] work in?")
                        .AddChoices(_departmentRepository.GetAll().AsNoTracking().Select(d => d.DepartmentName).ToList()));

            var pickedOccupations = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title($"What are the positions of [green]{firstname} {lastname}[/]?")
                        .Required()
                        .PageSize(10)
                        .AddChoices(_occupationRepository.GetAll().AsNoTracking().Select(o => o.Title).ToList()));
            

            AnsiConsole.MarkupLine($"\nFirst & Last name -> [yellow]{firstname} {lastname}[/]");
            AnsiConsole.MarkupLine($"Will work in department -> [yellow]{pickedDepartment}[/]");
            AnsiConsole.MarkupLine($"Will Work as -> [yellow]{pickedOccupations.Aggregate((p1, p2) => { return p1 + ", " + p2; })}[/]\n");

            // saving data
            if (AnsiConsole.Confirm("Ensured to save?"))
            {
                try
                {

                    AnsiConsole.Status()
                        .Start("Saving to database...", ctx =>
                        {

                            _personnelRepository.AddNewPersonnel(firstname, lastname, pickedDepartment, pickedOccupations, DateTime.Today);
                            _personnelRepository.Commit();
                            Thread.Sleep(1500);

                        });

                    AnsiConsole.MarkupLine("\nData has been saved [green rapidBlink]Successfully[/]!");
                }
                catch (Exception)
                {
                    AnsiConsole.MarkupLine("\n[yellow rapidBlink]Something went wrong while saving.[/]");

                }

                
            }else
            {
                Console.WriteLine();
                var menu = Menu.MenuOptionsBuilder()
                    .AddMenuOption("Try again", Rerender())
                    .AddMenuOption("Cancel", ReturnBack())
                    .Build();

                var result = AnsiConsole.Prompt(menu);
                return result.Value as DialogLocationData ?? ReturnBack();
            }

            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();

            return ReturnBack();
        }
    }
}