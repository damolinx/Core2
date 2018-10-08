namespace Core2
{
    public class CommandResult
    {
        public static readonly CommandResult Empty = new ImmutableCommandResult();

        private sealed class ImmutableCommandResult : CommandResult
        {
        }
    }
}