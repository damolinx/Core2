using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Commands.Prompt.Cmdlets
{
    [Description("Gets help on registered cmdlets")]
    public class HelpCmdlet : PromptCmdlet
    {
        public override Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args)
        {
            var cmdlets = context.Prompt.GetCmdlets(context);
            var columnWidth = cmdlets.Keys.Max(k => k.Length) + 1;

            foreach (var cmdlet in cmdlets.OrderBy(kvp => kvp.Key, StringComparer.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("{0} {1}", cmdlet.Key.PadRight(columnWidth), GetDescription(cmdlet.Value.GetType()));
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }

        private static string GetDescription(Type cmdletType)
        {
            return cmdletType.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .Select(attrib => attrib.Description)
                .FirstOrDefault() ?? string.Empty;
        }
    }
}
