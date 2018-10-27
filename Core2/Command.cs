using System.Threading.Tasks;

namespace Core2
{
    public abstract class Command
    {
        public Command(CommandSettings settings = null)
        {
            this.Settings = new CommandSettings();
        }

        public CommandSettings Settings { get; }

        public abstract Task<CommandResult> ExecuteAsync(CommandContext context);
    }
}