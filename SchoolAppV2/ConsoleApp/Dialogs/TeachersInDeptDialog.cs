using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Repository.Repos;
using Spectre.Console;
using System.Runtime.Intrinsics.Arm;

namespace ConsoleApp.Dialogs
{
    public class TeachersInDeptDialog : DialogBase
    {
        private IDepartmentRepository _departmentRepo;

        public TeachersInDeptDialog(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("The number of teachers in every departments");

            // fetching data
            var departments = _departmentRepo.GetAll().Select(
                    d => new
                    {
                        DepartmentName = d.DepartmentName,
                        CountTeachers = d.Personnel.Count(p => p.OccupationTitles.Any(o => o.Title == "Lärare"))
                    }
                ).AsEnumerable();

            // configuring and filling table
            var table = new Table();
            string fg = "mediumpurple4";
            table.AddColumns($"[{fg}]Department[/]", $"[{fg}]Teachers[/]");
            table.BorderColor(Color.RoyalBlue1);

            foreach (var dep in departments)
            {
                table.AddRow(dep.DepartmentName, dep.CountTeachers.ToString());
            }

            // displaying table
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();

            return ReturnBack();
        }
    }
}