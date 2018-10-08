using System.ComponentModel;
using System.Threading.Tasks;

namespace Core2.Commands.Prompt.Cmdlets
{
    [Description("Exits interactive prompt")]
    public class ExitCmdlet : PromptCmdlet
    {
        public ExitCmdlet()
        {
        }

        public override Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args)
        {
            context.Exit = true;
            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
