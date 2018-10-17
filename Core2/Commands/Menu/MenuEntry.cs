using System;
using System.Threading;

namespace Core2.Commands.Menu
{
    public class MenuEntry
    {
        private Command _command;

        public Command Command => LazyInitializer.EnsureInitialized(ref _command, CommandFactory);

        public Func<Command> CommandFactory { get; set; }

        public ConsoleKey Key { get; set; }

        public string Text { get; set; }
    }
}