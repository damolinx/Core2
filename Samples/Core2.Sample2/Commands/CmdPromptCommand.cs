using Core2.Commands.Prompt;
using Core2.Commands.Prompt.Cmdlets;
using Core2.Sample2.Cmdlets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core2.Sample2.Commands
{
    public class CmdPromptCommand : PromptCommand<CmdPromptContext>
    {
        protected override Task<TCmdletContext> CreateCmdletContextAsync<TCmdletContext>(CommandContext context)
        {
            var cmdletContext = (TCmdletContext)new CmdPromptContext(context, this);
            return Task.FromResult(cmdletContext);
        }

        protected override IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(CmdPromptContext context)
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

        protected override string GetPromptText(CmdPromptContext context)
        {
            return Environment.CurrentDirectory + ">";
        }
    }
}
