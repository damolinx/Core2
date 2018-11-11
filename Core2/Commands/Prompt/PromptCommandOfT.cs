using System.Collections.Generic;
using System.ComponentModel;

namespace Core2.Commands.Prompt
{
    public abstract class PromptCommand<TContext> : PromptCommand
        where TContext : PromptCmdletContext
    {
        protected abstract TCmdletContext CreateCmdletContext<TCmdletContext>(CommandContext context)
            where TCmdletContext : TContext;

        protected virtual IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(TContext context)
        {
            return base.CreateCmdlets(context);
        }

        protected virtual string GetPromptText(TContext context)
        {
            return base.GetPromptText(context);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override PromptCmdletContext CreateCmdletContext(CommandContext context)
        {
            var cmdletContext = CreateCmdletContext<TContext>(context);
            return cmdletContext;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override string GetPromptText(PromptCmdletContext context)
        {
            return GetPromptText((TContext)context);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected sealed override IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(PromptCmdletContext context)
        {
            return CreateCmdlets((TContext)context);
        }
    }
}
