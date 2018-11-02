namespace Core2.Commands
{
    public class CommandResult
    {
        public static readonly CommandResult Empty = new ImmutableCommandResult();

        private sealed class ImmutableCommandResult : CommandResult
        {
        }
    }
}