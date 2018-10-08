using Core2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Commands.Menu
{
    public class MenuPageCommand : Command
    {
        public MenuPageCommand(string backLabel = "Back")
        {
            this.RequiresClearScreen = false;
            this.RequiresCursor = false;

            this.MenuEntries = new List<MenuPageEntry>(
                new[]
                {
                    new MenuPageEntry
                    {
                        Command = new BackMenuPageCommand(this),
                        Key = ConsoleKey.D0,
                        Text = backLabel,
                    }
                });
        }

        public IList<MenuPageEntry> MenuEntries { get; }

        public sealed override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            var menuEntries = GetMenuEntries(context);
            foreach (var menuEntry in menuEntries.OrderBy(m => m.Key))
            {
                Console.WriteLine(" {0}. {1}", ConsoleKeyUtilities.AsUserString(menuEntry.Key.Value), menuEntry.Text);
            }

            var entry = WaitForEntry(menuEntries);
            context.Program.Commands.Push(this);
            context.Program.Commands.Push(entry.Command);

            return Task.FromResult(CommandResult.Empty);
        }

        protected virtual IReadOnlyList<MenuPageEntry> GetMenuEntries(CommandContext context)
        {
            return new ReadOnlyCollection<MenuPageEntry>(this.MenuEntries);
        }

        #region Private

        private static MenuPageEntry WaitForEntry(IEnumerable<MenuPageEntry> entries)
        {
            MenuPageEntry entry;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                entry = entries.FirstOrDefault(e => e.Key == keyInfo.Key);
            }
            while (entry == null);
            return entry;
        }

        #endregion
    }
}
