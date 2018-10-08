using Core2.Extensions;
using System;

namespace Core2.Commands.Prompt.Parsers
{
    public class SimplePromptInputParser : PromptInputParser
    {
        protected override (string cmdletName, string[] cmdletArguments) Parse(PromptCmdletContext context, string input)
        {
            //TODO: simple " match
            var breakIndex = input.IndexOf(Char.IsWhiteSpace);
            return (breakIndex > -1)
                ? (cmdletName: input.Substring(0, breakIndex), cmdletArguments: new[] { input.Substring(breakIndex + 1) })
                : (cmdletName: input, cmdletArguments: Array.Empty<string>());
        }
    }
}
