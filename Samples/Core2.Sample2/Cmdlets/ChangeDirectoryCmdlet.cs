using Core2.Commands.Prompt;
using Core2.Sample2.Commands;
using Core2.Utilities;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [Description("Change current directory")]
    public class ChangeDirectoryCmdlet : PromptCmdlet<CmdPromptCmdletContext>
    {
        public override Task<PromptCmdletResult> ExecuteAsync(CmdPromptCmdletContext context, params string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine(context.CurrentDirectory);
                    break;

                case 1:
                    context.CurrentDirectory = PathUtilities.GetFullPath(args[0], true);
                    break;

                default:
                    Console.Error.WriteLine("Too Many Arguments");
                    break;
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
