using Core2.Commands.Menu;
using System;

namespace Core2.Sample3
{
    class Program : Core2.ProgramBase
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Commands.Push(CreateMainPage());
            program.Execute(args);
        }

        private static MenuPageCommand CreateMainPage()
        {
            var main = new MenuPageCommand(backLabel: "Exit");

            main.MenuEntries.Add(new MenuPageEntry
            {
                Command = new Dynamic1MenuPageCommand(),
                Key = ConsoleKey.D1,
                Text = "Dynamic (full)"
            });

            main.MenuEntries.Add(new MenuPageEntry
            {
                Command = new Dynamic2MenuPageCommand(),
                Key = ConsoleKey.D2,
                Text = "Dynamic (with defaults)"
            });

            main.MenuEntries.Add(new MenuPageEntry
            {
                Command = new SaySomethingCommand("Hello World!"),
                Key = ConsoleKey.D3,
                Text = "Say 'Hello World!'"
            });

            return main;
        }
    }
}
