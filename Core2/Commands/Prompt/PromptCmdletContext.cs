using System;
using System.Collections.Generic;

namespace Core2.Commands.Prompt
{
    public class PromptCmdletContext
    {
        private readonly CommandContext _commandContext;

        public PromptCmdletContext(CommandContext context, PromptCommand prompt)
        {
            _commandContext = context ?? throw new ArgumentNullException(nameof(context));
            this.Annotations = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            this.Prompt = prompt ?? throw new ArgumentNullException(nameof(prompt));
        }

        public IDictionary<string, object> Annotations { get; }

        public bool Exit { get; internal set; }

        public PromptCommand Prompt { get; }
    }
}
