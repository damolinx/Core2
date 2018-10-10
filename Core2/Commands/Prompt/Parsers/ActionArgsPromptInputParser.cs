using System;
using System.Collections.Generic;
using System.Linq;

namespace Core2.Commands.Prompt.Parsers
{
    /// <summary>
    /// Parses out an action name from input. Arguments are parsed using whitespaces as separators.
    /// </summary>
    public sealed class ActionArgsPromptInputParser : PromptInputParser
    {
        protected override (string cmdletName, string[] cmdletArguments) Parse(PromptCmdletContext context, string input)
        {
            var tokens = Parse(input).ToArray();
            return (cmdletName: tokens.FirstOrDefault(), cmdletArguments: tokens.Skip(1).ToArray());
        }

        private static IEnumerable<string> Parse(string input)
        {
            var start = 0;
            var end = 0;

            while (end < input.Length)
            {
                if (Char.IsWhiteSpace(input[end]))
                {
                    yield return input.Substring(start, end - start);
                    start = end + 1;
                }
                end++;
            }

            yield return input.Substring(start);
        }
    }
}
