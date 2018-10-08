namespace Core2.Commands.Prompt
{
    public class PromptCmdletResult
    {
        public static readonly PromptCmdletResult Empty = new ImmutablePromptCmdletResult();


        private sealed class ImmutablePromptCmdletResult : PromptCmdletResult
        {
        }
    }
}
