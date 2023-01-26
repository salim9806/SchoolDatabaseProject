using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.Data.SqlClient;
using Repository;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class PersonnelByOccupationDialog : DialogBase
    {
        private ISqlServerClient _dbClient;

        public PersonnelByOccupationDialog(ISqlServerClient dbClient)
        {
            _dbClient = dbClient;
        }
        public override DialogLocationData DisplayContent()
        {
            var occupations = new List<string>();

            #region ADO.NET Hämtar alla kategorierna
            
            using SqlDataReader dataOccupationReader = _dbClient.Sql("SELECT Title FROM Occupation");
            while (dataOccupationReader.Read())
                occupations.Add(dataOccupationReader["Title"].ToString());

            _dbClient.CloseConnection();
            #endregion


            var pickedOccupation = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Choose a [green]position[/] job to filter by?")
                .AddChoices(occupations)
            );

            Menu.DisplayTitle($"Staffs That Work As \"{pickedOccupation}\"");

            // configuring and filling table
            var table = new Table();
            string fg = "mediumpurple4";
            table.AddColumns($"[{fg}]First Name[/]", $"[{fg}]Last Name[/]");
            table.BorderColor(Color.RoyalBlue1);

            #region ADO.NET Hämta alla personal genom en av kategorierna t.ex 'lärare'

            using SqlDataReader dataPersonnelReader = _dbClient.Sql("SELECT FirstName, LastName FROM Personnel p " +
                                                                    "join PersonnelOccupation po ON po.PersonnelId = p.Id " +
                                                                    $"WHERE OccupationTitle = '{pickedOccupation}'");
            while (dataPersonnelReader.Read())
            {
                table.AddRow(dataPersonnelReader["FirstName"].ToString(), dataPersonnelReader["LastName"].ToString());
            }

            #endregion

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


            AnsiConsole.MarkupLine("\n[yellow rapidblink]Empty list! No staff has been found[/]");
            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();
            return ReturnBack();
        }
    }
}
