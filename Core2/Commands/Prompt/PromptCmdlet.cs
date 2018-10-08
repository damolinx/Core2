using System.Threading.Tasks;

namespace Core2.Commands.Prompt
{
    public abstract class PromptCmdlet
    {
        protected PromptCmdlet()
        {
        }

        public abstract Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args);
    }

    public abstract class PromptCmdlet<TContext> : PromptCmdlet
        where TContext : PromptCmdletContext
    {
        protected PromptCmdlet()
        {
        }

        public abstract Task<PromptCmdletResult> ExecuteAsync(TContext context, params string[] args);

        public sealed override Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args)
        {
            return ExecuteAsync((TContext)context, args);
        }
    }
}
