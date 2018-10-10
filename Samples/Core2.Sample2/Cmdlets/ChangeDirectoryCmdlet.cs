using Core2.Commands.Prompt;
using Core2.Utilities;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [Description("Change current directory")]
    public class ChangeDirectoryCmdlet : PromptCmdlet
    {
        public override Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine(Environment.CurrentDirectory);
                    break;

                case 1:
                    Environment.CurrentDirectory = PathUtilities.GetFullPath(args[0], true);
                    break;

                default:
                    Console.WriteLine("Too many arguments");
                    break;
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
