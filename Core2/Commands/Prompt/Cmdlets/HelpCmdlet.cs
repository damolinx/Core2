using Core2.Commands.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Commands.Prompt.Cmdlets
{
    [CommandDescription("Gets help on registered cmdlets")]
    public class HelpCmdlet<TContext> : PromptCmdlet<TContext>
        where TContext : PromptCmdletContext
    {
        public override Task<PromptCmdletResult> ExecuteAsync(TContext context, params string[] args)
        {
            var cmdlets = context.Prompt.GetCmdlets(context);
            var columnWidth = cmdlets.Keys.Max(k => k.Length) + 1;

            foreach (var cmdlet in cmdlets.OrderBy(kvp => kvp.Key, StringComparer.CurrentCultureIgnoreCase))
            {
                var description = CommandDescriptionAttribute.GetDescription(cmdlet.Value.GetType()) ?? string.Empty;
                Console.WriteLine("{0} {1}", cmdlet.Key.PadRight(columnWidth), description);
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
