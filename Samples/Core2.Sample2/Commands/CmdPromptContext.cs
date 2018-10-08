using Core2.Commands.Prompt;
using System;

namespace Core2.Sample2.Commands
{
    public class CmdPromptContext : PromptCmdletContext
    {
        public CmdPromptContext(CommandContext context, PromptCommand prompt)
            : base(context, prompt)
        {
        }

        public string CurrentDirectory
        {
            get { return Environment.CurrentDirectory; }
            set { Environment.CurrentDirectory = value; }
        }
    }
}
