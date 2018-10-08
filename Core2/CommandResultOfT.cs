namespace Core2
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