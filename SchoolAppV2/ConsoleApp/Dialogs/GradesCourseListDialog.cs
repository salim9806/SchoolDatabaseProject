using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.Data.SqlClient;
using Repository;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class GradesCourseListDialog : DialogBase
    {
        private ISqlServerClient _dbClient;

        public GradesCourseListDialog(ISqlServerClient dbClient)
        {
            _dbClient = dbClient;
        }
        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Show average, max and min grades of every Courses");

            // configuring and filling table
            var table = new Table();
            string fg = "mediumpurple4";
            table.AddColumns($"[{fg}]Course[/]", $"[{fg}]Avg. grade[/]", $"[{fg}]Highest grade[/]", $"[{fg}]Lowest grade[/]");
            table.BorderColor(Color.RoyalBlue1);

            using SqlDataReader dataReader = _dbClient.Sql("SELECT CourseName, dbo.GradeAsLetter(ROUND(AVG(GLN.Number),1)) AS avgGrade, dbo.GradeAsLetter(MAX(GLN.Number)) as maxGrade, dbo.GradeAsLetter(MIN(GLN.Number)) as minGrade FROM Course C " +
                                                           "join Grade G on C.CourseName = G.TakenCourse " +
                                                           "join Student S on S.Id = G.StudentId " +
                                                           "inner join GradeLetterNumberMap GLN on GLN.Letter = G.Rating " +
                                                           "GROUP BY CourseName");

            while(dataReader.Read())
            {
                table.AddRow(dataReader["CourseName"].ToString(), dataReader["avgGrade"].ToString(), dataReader["maxGrade"].ToString(), dataReader["minGrade"].ToString());
            }

            // displaying table
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();

            return ReturnBack();
        }
    }
}