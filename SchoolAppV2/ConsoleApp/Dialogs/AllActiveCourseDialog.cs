using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Repository.Repos;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class AllActiveCourseDialog : DialogBase
    {
        private ICourseRepository _courseRepo;

        public AllActiveCourseDialog(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public override DialogLocationData DisplayContent()
        {
            // querying list
            var courses = _courseRepo.GetActiveCourses().AsEnumerable();

            Menu.DisplayTitle("List Of All active Courses");

            if (courses.Any())
            {

                // configuring and filling table
                var table = new Table();
                string fg = "mediumpurple4";
                table.AddColumns($"[{fg}]Course Name[/]");
                table.BorderColor(Color.RoyalBlue1);

                foreach (var course in courses)
                {
                    table.AddRow(course.CourseName);
                }

                // displaying table
                AnsiConsole.Write(table);


            }
            else
                AnsiConsole.MarkupLine("\n[yellow rapidblink]Empty list! No active course has been found[/]");


            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();
            return ReturnBack();
        }
    }
}