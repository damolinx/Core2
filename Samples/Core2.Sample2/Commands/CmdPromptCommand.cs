using Core2.Commands.Prompt;
using Core2.Commands.Prompt.Cmdlets;
using Core2.Sample2.Cmdlets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core2.Sample2.Commands
{
    public class CmdPromptCommand : PromptCommand
    {
        protected override Task<PromptCmdletContext> CreateCmdletContextAsync(CommandContext context)
        {
            return Task.FromResult(new PromptCmdletContext(context, this));
        }

        protected override IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(PromptCmdletContext context)
        {
            var dictionary = new Dictionary<string, PromptCmdlet>(StringComparer.OrdinalIgnoreCase)
            {
                { "cd", new ChangeDirectoryCmdlet() },
                { "dir", new DirectoryCmdlet() },
                { "exit",  new ExitCmdlet() },
                { "help",  new HelpCmdlet() }
            };

            return dictionary;
        }

        protected override string GetPromptText(PromptCmdletContext context)
        {
            return Environment.CurrentDirectory + ">";
        }
    }
}
