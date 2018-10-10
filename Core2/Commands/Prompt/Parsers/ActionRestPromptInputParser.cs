using Core2.Extensions;
using System;

namespace Core2.Commands.Prompt.Parsers
{
    /// <summary>
    /// Parses out an action name from input, rest of string becomes a single cmdlet argument
    /// </summary>
    public sealed class ActionRestPromptInputParser : PromptInputParser
    {
        protected override (string cmdletName, string[] cmdletArguments) Parse(PromptCmdletContext context, string input)
        {
            var breakIndex = input.IndexOf(Char.IsWhiteSpace);
            return (breakIndex > -1)
                ? (cmdletName: input.Substring(0, breakIndex), cmdletArguments: new[] { input.Substring(breakIndex + 1) })
                : (cmdletName: input, cmdletArguments: Array.Empty<string>());
        }
    }
}
