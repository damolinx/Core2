using System;
using System.Threading.Tasks;

namespace Core2.Commands
{
    public abstract class Command
    {
        public Command(CommandSettings settings = null)
        {
            this.Settings = new CommandSettings();
        }

        public CommandSettings Settings { get; }

        public abstract Task<CommandResult> ExecuteAsync(CommandContext context);

        /// <summary>
        /// Handle exception thrown by <see cref="ExecuteAsync(CommandContext)"/>
        /// </summary>
        /// <param name="context">Command Context</param>
        /// <param name="ex">Exception</param>
        /// <returns>
        /// <c>true</c> if exception was handled, <c>false</c> otherwise.
        /// </returns>
        /// <remarks>
        /// Command execution will be terminated regardless of return value, but a
        /// handled exception won't cause the program to exit.
        /// </remarks>
        public virtual bool HandleException(CommandContext context, Exception ex)
        {
            return false;
        }
    }
}