using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ViewComponents.MenuComponent
{
    public class MenuOption
    {
        public int OptionNumber { get; set; }
        public string Text { get; set; }
        public object? Value { get; set; }
        public override string ToString()
        {
            return $"{Text}";
        }
    }
    public class MenuOptionsBuilder
    {
        short idCounter = 0;
        private Dictionary<short, MenuOption> _menuOptions;

        public MenuOptionsBuilder()
        {
            _menuOptions = new Dictionary<short, MenuOption>();
        }

        public MenuOptionsBuilder AddMenuOption(string text, object? value = null)
        {
            short id = ++idCounter;
            _menuOptions.Add(id, new MenuOption { OptionNumber = id, Text = text, Value = value });
            return this;
        }

        public SelectionPrompt<MenuOption> Build()
        {
            return new SelectionPrompt<MenuOption>()
                .AddChoices(_menuOptions.Values);
        }
    }

    public static class Menu
    {
        public static MenuOptionsBuilder MenuOptionsBuilder()
        {
            return new MenuOptionsBuilder();
        }

        internal static void DisplayTitle(string titleText)
        {
            var rule = new Rule($"[lightgoldenrod2_1]{titleText}[/]\n");
            rule.LeftAligned();
            var titleComponent = rule;
            AnsiConsole.Write(titleComponent);
            Console.WriteLine();
        }
    }

}
