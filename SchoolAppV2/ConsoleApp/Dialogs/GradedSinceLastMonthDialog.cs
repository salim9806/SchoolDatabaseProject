using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.Data.SqlClient;
using Repository;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class GradedSinceLastMonthDialog : DialogBase
    {
        private ISqlServerClient _dbClient;

        public GradedSinceLastMonthDialog(ISqlServerClient dbClient)
        {
            _dbClient = dbClient;
        }

        public override DialogLocationData DisplayContent()
        {
            Menu.DisplayTitle("Last Of Grades From Last Month");
            

            // configuring and filling table
            var table = new Table();
            string fg = "mediumpurple4";
            table.AddColumns($"[{fg}]Student name[/]", $"[{fg}]Course name[/]", $"[{fg}]Grade[/]", $"[{fg}]Date[/]");
            table.BorderColor(Color.RoyalBlue1);

            #region ADO.NET Hämta alla betyg som satts den senaste månaden.
            using SqlDataReader dataReader = _dbClient.Sql("SELECT CONCAT(FirstName, ' ', LastName) as StudentName, TakenCourse, Rating, TimeStamp FROM Grade g " +
                                                           "join Student s ON s.Id = g.StudentId " +
                                                           "join Course c ON c.CourseName = g.TakenCourse " +
                                                           "WHERE TimeStamp > DATEADD(MONTH,-1,getdate()) " +
                                                           "ORDER BY TimeStamp DESC");

            while(dataReader.Read())
            {
                table.AddRow(dataReader["StudentName"].ToString(), dataReader["TakenCourse"].ToString(), dataReader["Rating"].ToString(),dataReader["TimeStamp"].ToString());
            }

            #endregion
            // displaying table
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();

            return ReturnBack();
        }
    }
}