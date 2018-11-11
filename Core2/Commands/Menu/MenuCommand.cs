using Core2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Commands.Menu
{
    public abstract class MenuCommand : Command
    {
        protected MenuCommand(string title, string backLabel = "Back")
        {
            this.MenuEntries = new List<MenuEntry>();
            this.MenuEntries.Add(new MenuEntry(ConsoleKey.D0, backLabel, new BackMenuCommand(this)));
            this.RequiresClearScreen = false;
            this.RequiresCursor = false;
            this.Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException("Cannot be empty", nameof(title));
        }

        public IList<MenuEntry> MenuEntries { get; }

        public string Title { get; set; }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            var menuEntries = GetMenuEntries(context);
            this.RenderHeader(context);
            this.RenderMenu(context, menuEntries);

            var entry = WaitForEntry(menuEntries);
            var entryCommand = entry.GetCommand(context);
            context.Program.Commands.Push(this);
            context.Program.Commands.Push(entryCommand);
            return Task.FromResult(CommandResult.Empty);
        }

        protected virtual void RenderHeader(CommandContext context)
        {
            Console.WriteLine(this.Title);
            Console.WriteLine(new string('-', this.Title.Length));
            Console.WriteLine();
        }

        protected virtual void RenderMenu(CommandContext context, IReadOnlyList<MenuEntry> menuEntries)
        {
            foreach (var menuEntry in menuEntries.OrderBy(m => m.Key))
            {
                Console.WriteLine(" {0}. {1}", ConsoleKeyUtilities.AsUserString(menuEntry.Key), menuEntry.Text);
            }
            Console.WriteLine();
        }

        protected virtual IReadOnlyList<MenuEntry> GetMenuEntries(CommandContext context)
        {
            return new ReadOnlyCollection<MenuEntry>(this.MenuEntries);
        }

        private static MenuEntry WaitForEntry(IEnumerable<MenuEntry> entries)
        {
            MenuEntry entry;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                entry = entries.FirstOrDefault(e => e.Key == keyInfo.Key);
            }
            while (entry == null);
            return entry;
        }
    }
}
