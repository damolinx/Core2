using System;
using System.Threading.Tasks;

namespace Core2.Commands.Menu
{
    public class BackMenuCommand : Command
    {
        private readonly MenuCommand _hostMenu;

        public BackMenuCommand(MenuCommand menu)
        {
            _hostMenu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            if (!context.Program.Commands.TryPop(out var command))
            {
                throw new InvalidOperationException("Failed to update command stack");
            }

            if (command != _hostMenu)
            {
                throw new InvalidOperationException("Unexpected command stack state");
            }

            return Task.FromResult(CommandResult.Empty);
        }
    }
}