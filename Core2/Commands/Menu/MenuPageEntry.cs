using System;

namespace Core2.Commands.Menu
{
    public class MenuPageEntry
    {
        public Command Command { get; set; }

        public ConsoleKey? Key { get; set; }

        public string Text { get; set; }
    }
}