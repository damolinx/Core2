using System;
using System.Threading.Tasks;

namespace Core2.Commands.Menu
{
    /// <summary>
    /// 'Back' command for menu pages
    /// </summary>
    public class BackMenuPageCommand : Command
    {
        private readonly MenuPageCommand _hostMenuPageCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="menuPageCommand">Host menu page command</param>
        public BackMenuPageCommand(MenuPageCommand menuPageCommand)
        {
            _hostMenuPageCommand = menuPageCommand ?? throw new ArgumentNullException(nameof(menuPageCommand));
        }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            if (!context.Program.Commands.TryPop(out var command))
            {
                throw new InvalidOperationException("Failed to update command stack");
            }

            if (command != _hostMenuPageCommand)
            {
                throw new InvalidOperationException("Unexpected command stack state");
            }

            return Task.FromResult(CommandResult.Empty);
        }
    }
}