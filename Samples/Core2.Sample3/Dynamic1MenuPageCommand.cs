using Core2.Commands.Menu;
using System;
using System.Collections.Generic;

namespace Core2.Sample3
{
    public class Dynamic1MenuPageCommand : MenuPageCommand
    {
        protected override IReadOnlyList<MenuPageEntry> GetMenuEntries(CommandContext context)
        {
            var entries = new List<MenuPageEntry>(base.GetMenuEntries(context));
            var random = new Random();
            var dynamicCount = random.Next(1, 5);

            for (var i = 0; i < dynamicCount; i++)
            {
                var r = random.Next();
                entries.Add(new MenuPageEntry
                {
                    Command = new SaySomethingCommand($"{r}!"),
                    Key = (ConsoleKey)(i + (int)ConsoleKey.D1),
                    Text = $"Say '{r}' (dynamic)"
                });
            }

            return entries;
        }
    }
}
