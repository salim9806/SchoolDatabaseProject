using ConsoleApp.Infrastructure;
using ConsoleApp.ViewComponents.MenuComponent;
using Microsoft.EntityFrameworkCore.Query;
using Repository;
using Repository.Repos;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp.Dialogs
{
    internal class StudentCreationDialog : DialogBase
    {
        private ISqlServerClient _dbClient;
        private IClassRepository _classRepo;

        public StudentCreationDialog(ISqlServerClient dbClient, IClassRepository classRepo)
        {
            _dbClient = dbClient;
            _classRepo = classRepo;
        }
        public override DialogLocationData DisplayContent()
        {


            // displaying title
            Menu.DisplayTitle("Adding new staff");

            var firstname = AnsiConsole.Ask<string>("What is student's [green]first name[/]?");
            var lastname = AnsiConsole.Ask<string>("What is student's [green]last name[/]?");
            
            DateTime birthDayDate = DateTime.Now;
            decimal securityNumber = 0;

            AnsiConsole.Prompt(
                new TextPrompt<string>(" [green]Social number[/]? (YYYYMMDD-XXXX) ")
                    .PromptStyle("yellow")
                    .ValidationErrorMessage("[red]That's not a valid social number[/]")
                    .Validate(input =>
                    {
                        Regex reg = new Regex(@"^(\d{4})(\d{2})(\d{2})-(\d{4})$");
                        var regMatch = reg.Match(input);
                        if (!regMatch.Success)
                            return ValidationResult.Error("you typed in wrong format!");

                        var g = regMatch.Groups;
                        if (!DateTime.TryParse($"{g[1].Value}-{g[2].Value}-{g[3].Value}", out birthDayDate))
                            return ValidationResult.Error("invalid birthday date, try again!");

                        if (birthDayDate > DateTime.UtcNow.AddYears(-16))
                            return ValidationResult.Error("Must be older than 16 years old!");

                        securityNumber = decimal.Parse(regMatch.Groups[4].Value);
                        return ValidationResult.Success();
                    }));
            
            var chosenClass = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What [green]class[/] this student belong to?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more classes)[/]")
                    .AddChoices(_classRepo.GetAll().Select(c => c.ClassName).ToList()));

            AnsiConsole.MarkupLine($"\n{firstname} was born in {birthDayDate.ToString("d")} and will go to class: {chosenClass}\n");


            // saving data
            if (AnsiConsole.Confirm("Ensured to save?"))
            {
                try
                {
                    AnsiConsole.Status()
                        .Start("Saving to database...", ctx =>
                        {
                            _dbClient.NonQuery($"INSERT INTO Student VALUES('{firstname}', '{lastname}', '{chosenClass}', '{birthDayDate.ToString("d")}', '{securityNumber}')");
                            Thread.Sleep(1500);
                        });
                    AnsiConsole.MarkupLine("\nData has been saved [green rapidBlink]Successfully[/]!");
                }
                catch (Exception)
                {
                    AnsiConsole.MarkupLine("\n[yellow rapidBlink]Something went wrong while saving.[/]");
                    throw;
                }

            }
            else
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
