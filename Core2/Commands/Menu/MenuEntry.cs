using System;
using System.Threading;

namespace Core2.Commands.Menu
{
    public class MenuEntry
    {
        private Command _command;

        public MenuEntry(ConsoleKey key, string text, Command command = null)
        {
            _command = command;
            this.Key = (key != default) ? key : throw new ArgumentException($"Invalid key. Key:{key}", nameof(key));
            this.Text = !string.IsNullOrWhiteSpace(text) ? text : throw new ArgumentException("Cannot be empty", nameof(text));
        }

        public Command GetCommand(CommandContext context)
        {
            if (_command == null && this.CommandFactory == null)
            {
                throw new InvalidOperationException($"Cannot create command, {nameof(Command)} and {nameof(CommandFactory)} are both null");
            }

            return LazyInitializer.EnsureInitialized(ref _command, () => this.CommandFactory(context));
        }

        public Func<CommandContext, Command> CommandFactory { get; set; }

        public ConsoleKey Key { get; }

        public string Text { get; set; }
    }
}