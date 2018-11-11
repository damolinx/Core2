using Core2.Commands;
using Core2.Commands.Prompt;
using Core2.Commands.Prompt.Cmdlets;
using Core2.Sample2.Cmdlets;
using System;
using System.Collections.Generic;

namespace Core2.Sample2.Commands
{
    public class CmdPromptCommand : PromptCommand<CmdPromptCmdletContext>
    {
        public CmdPromptCommand()
        {
        }

        protected override TCmdletContext CreateCmdletContext<TCmdletContext>(CommandContext context)
        {
            var promptContext = new CmdPromptCmdletContext(context, this);
            return (TCmdletContext)promptContext;
        }

        protected override IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(CmdPromptCmdletContext context)
        {
            var dictionary = new Dictionary<string, PromptCmdlet>(StringComparer.OrdinalIgnoreCase)
            {
                { "cat", new CatCmdlet() },
                { "cd", new ChangeDirectoryCmdlet() },
                { "dir", new DirectoryCmdlet() },
                { "exit",  new ExitCmdlet<CmdPromptCmdletContext>() },
                { "help",  new HelpCmdlet<CmdPromptCmdletContext>() },
            };

            return dictionary;
        }

        protected override string GetPromptText(CmdPromptCmdletContext context)
        {
            return context.CurrentDirectory + ">";
        }
    }
}
