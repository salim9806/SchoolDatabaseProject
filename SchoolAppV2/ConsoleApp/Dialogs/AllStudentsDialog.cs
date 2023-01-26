using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Repository.Repos;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class AllStudentsDialog : DialogBase
    {
        private IStudentRepository _studentRepo;

        public AllStudentsDialog(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public override DialogLocationData DisplayContent()
        {
            // querying list
            var studentList = _studentRepo.GetAll();

            Menu.DisplayTitle("List Of All Students");

            if (studentList.Any())
            {

                // configuring and filling table
                var table = new Table();
                string fg = "mediumpurple4";
                table.AddColumns($"[{fg}]First Name[/]", $"[{fg}]Last Name[/]", $"[{fg}]Date of Birth[/]", $"[{fg}]Class[/]");
                table.BorderColor(Color.RoyalBlue1);

                foreach (var student in studentList)
                {
                    table.AddRow(student.FirstName, student.LastName, student.DateOfBirth.ToString("d"), student.BelongToClass ?? "ingen class");
                }

                // displaying table
                AnsiConsole.Write(table);

                
            }else
                AnsiConsole.MarkupLine("\n[yellow rapidblink]Empty list! No students has been found[/]");


            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();
            return ReturnBack();
        }
    }
}