using Core2.Commands;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Core2
{
    public abstract class ProgramBase
    {
        protected ProgramBase()
        {
            this.Commands = new ConcurrentStack<Command>();
            this.Encoding = Encoding.UTF8;
        }

        public ConcurrentStack<Command> Commands { get; }

        public Encoding Encoding { get; }

        protected virtual CommandContext CreateCommandContext(Command command)
        {
            return new CommandContext(this);
        }

        public virtual void Execute(params string[] args)
        {
            try
            {
                ExecuteAsync(args).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                //TODO: log
                throw;
            }
        }

        public virtual async Task ExecuteAsync(params string[] args)
        {
            while (this.Commands.TryPop(out var command))
            {
                this.SetupConsole(command);
                var commandContext = CreateCommandContext(command);
                try
                {
                    var commandResult = await command.ExecuteAsync(commandContext)
                        .ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //TODO: log
                    if (!command.HandleException(commandContext, ex))
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Set up console for program
        /// </summary>
        /// <remarks>
        /// Runs once during program initialization.
        /// </remarks>
        protected virtual void SetupConsole()
        {
            Console.InputEncoding = this.Encoding;
            Console.OutputEncoding = this.Encoding;
        }

        /// <summary>
        /// Set up console for command
        /// </summary>
        /// <remarks>
        /// Runs once before executing <paramref name="command"/>.
        /// </remarks>
        protected virtual void SetupConsole(Command command)
        {
            Console.CursorVisible = command.RequiresCursor;

            if (command.RequiresClearScreen)
            {
                Console.Clear();
            }
        }
    }
}
