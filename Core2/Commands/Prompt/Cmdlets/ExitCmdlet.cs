using Core2.Commands.Attributes;
using System.Threading.Tasks;

namespace Core2.Commands.Prompt.Cmdlets
{
    [CommandDescription("Exits interactive prompt")]
    public class ExitCmdlet<TContext> : PromptCmdlet<TContext>
        where TContext : PromptCmdletContext
    {
        public override Task<PromptCmdletResult> ExecuteAsync(TContext context, params string[] args)
        {
            context.Exit = true;
            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
