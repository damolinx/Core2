using Core2.Common;
using System;

namespace Core2.Commands.Prompt
{
    public class PromptCmdletContext : ContextBase
    {
        public PromptCmdletContext(CommandContext parent, PromptCommand prompt)
            : base(parent)
        {
            this.Prompt = prompt ?? throw new ArgumentNullException(nameof(prompt));
        }

        public bool Exit { get; set; }

        public PromptCommand Prompt { get; }
    }
}
