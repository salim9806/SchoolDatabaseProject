//using AsciiTableFormatter;
using ConsoleApp.Infrastructure;
//using ConsoleApp.Services;
using ConsoleApp.ViewComponents.MenuComponent;
//using DataSource.Entities;
using Repository.Repos;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class StudentListDialog:DialogBase
    {
        private IStudentRepository _studentRepository;

        public StudentListDialog(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public override DialogLocationData DisplayContent()
        {

            // prompting for orderBy
            string by = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Order by student [green]First name[/] or by [green]Last name[/]?")
                        .AddChoices(new[] { "First name", "Last name"} ));

            // prompting for descending or ascending
            string descendOrAscend = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Descending[/] or [green]Ascending[/]?")
                        .AddChoices(new[] { "Ascending", "Descending" }));



            // querying list
            var studentList = _studentRepository.GetAllStudents(
                orderBy: t => by == "First name" ? t.FirstName : t.LastName,
                orderByDescending: descendOrAscend == "Ascending" ? false : true
                );

            Menu.DisplayTitle("List Of All Students");

            if (studentList.Any())
            {

                // configuring and filling table
                var table = new Table();
                string fg = "mediumpurple4";
                table.AddColumns($"[{fg}]First Name[/]", $"[{fg}]Last Name[/]", $"[{fg}]Birthday date[/]", $"[{fg}]Class[/]");
                table.BorderColor(Color.RoyalBlue1);

                foreach (var student in studentList)
                {
                    table.AddRow(student.FirstName, student.LastName, student.DateOfBirth.ToString("d"), student.BelongToClass ?? "ingen class");
                }

                // displaying table
                AnsiConsole.Write(table);

                // display menu to query again or return back
                Console.WriteLine();
                var menu = Menu.MenuOptionsBuilder()
                    .AddMenuOption("Query again", Refresh())
                    .AddMenuOption("Return back", ReturnBack())
                    .Build();

                var choice = AnsiConsole.Prompt(menu);

                return choice.Value as DialogLocationData ?? Refresh();
            }

            AnsiConsole.MarkupLine("\n[yellow rapidblink]Empty list! No students has been found[/]");
            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();
            return ReturnBack();


        }
    }
}
