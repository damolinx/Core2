namespace Core2.Commands.Prompt.Parsers
{
    public abstract class PromptInputParser
    {
        protected PromptInputParser()
        {
        }

        protected abstract (string cmdletName, string[] cmdletArguments) Parse(PromptCmdletContext context, string input);

        public bool TryParseInput(PromptCmdletContext context, string input, out (string cmdletName, string[] cmdletArguments) parsedInput)
        {
            try
            {
                parsedInput = Parse(context, input);
                return true;
            }
            catch
            {
                //TODO: Trace
                parsedInput = default((string, string[]));
                return false;
            }
        }
    }
}