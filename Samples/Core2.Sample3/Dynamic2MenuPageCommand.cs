using Core2.Commands.Menu;
using System;

namespace Core2.Sample3
{
    public class Dynamic2MenuPageCommand : Dynamic1MenuPageCommand
    {
        public Dynamic2MenuPageCommand()
        {
            this.MenuEntries.Add(new MenuPageEntry
            {
                Command = new SaySomethingCommand("A!"),
                Key = ConsoleKey.A,
                Text = "Say 'A' (static)"
            });
            this.MenuEntries.Add(new MenuPageEntry
            {
                Command = new SaySomethingCommand("B!"),
                Key = ConsoleKey.B,
                Text = "Say 'B' (static)"
            });
            this.MenuEntries.Add(new MenuPageEntry
            {
                Command = new SaySomethingCommand("C!"),
                Key = ConsoleKey.C,
                Text = "Say 'C' (static)"
            });
        }
    }
}
