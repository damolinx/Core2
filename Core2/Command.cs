using System.Threading.Tasks;

namespace Core2
{
    public abstract class Command
    {
        public bool RequiresCursor { get; set; }

        public bool RequiresClearScreen { get; set; }

        public abstract Task<CommandResult> ExecuteAsync(CommandContext context);
    }
}