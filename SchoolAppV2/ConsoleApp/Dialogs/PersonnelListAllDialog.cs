using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.Data.SqlClient;
using Repository;
using Repository.Repos;
using Spectre.Console;

namespace ConsoleApp.Dialogs
{
    internal class PersonnelListAllDialog : DialogBase
    {
        private ISqlServerClient _dbClient;

        public PersonnelListAllDialog(ISqlServerClient dbClient)
        {
            _dbClient = dbClient;
        }
        
        public override DialogLocationData DisplayContent()
        {

            // configuring and filling table
            var table = new Table();
            string fg = "mediumpurple4";
            table.AddColumns($"[{fg}]First Name[/]", $"[{fg}]Last Name[/]");
            table.BorderColor(Color.RoyalBlue1);

            #region ADO.NET (1) hämta personal
            using SqlDataReader dataReader = _dbClient.Sql("SELECT * FROM Personnel");

            while(dataReader.Read())
            {
                table.AddRow(dataReader["FirstName"].ToString(), dataReader["LastName"].ToString());
            }
            #endregion

            // displaying table
            Menu.DisplayTitle("List Of All The Staff");
            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[dim][[Press any key...]][/]");
            Console.ReadKey();

            return ReturnBack();
        }
    }
}
