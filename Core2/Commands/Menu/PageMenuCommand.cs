using System;
using System.Threading.Tasks;

namespace Core2.Commands.Menu
{
    public class PageMenuCommand : MenuCommand
    {
        public PageMenuCommand(string title, string backLabel = "Back")
            : base(title, backLabel)
        {
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            Console.Clear();
            return base.ExecuteAsync(context);
        }
    }
}
