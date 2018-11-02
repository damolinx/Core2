namespace Core2.Commands
{
    public class CommandResult<TData> : CommandResult
    {
        public CommandResult(TData data)
        {
            this.Data = data;
        }

        public TData Data { get; }
    }
}