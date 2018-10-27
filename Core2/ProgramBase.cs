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
        }

        public ConcurrentStack<Command> Commands { get; }

        protected virtual Task<CommandContext> CreateCommandContextAsync(Command command)
        {
            return Task.FromResult(new CommandContext(this));
        }

        public void Execute(params string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
                ExecuteAsync(args).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                //TODO: log
            }
        }

        public virtual async Task ExecuteAsync(params string[] args)
        {
            while (this.Commands.TryPop(out var command))
            {
                SetupConsole(command);

                var commandContext = await CreateCommandContextAsync(command)
                    .ConfigureAwait(false);

                var commandResult = await command.ExecuteAsync(commandContext)
                    .ConfigureAwait(false);
            }
        }

        private static void SetupConsole(Command command)
        {
            Console.CursorVisible = command.Settings.RequiresCursor;

            if (command.Settings.RequiresClearScreen)
            {
                Console.Clear();
            }
        }
    }
}
