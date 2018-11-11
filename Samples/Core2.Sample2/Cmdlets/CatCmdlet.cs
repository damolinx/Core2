using Core2.Commands.Attributes;
using Core2.Commands.Prompt;
using Core2.Sample2.Commands;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [CommandArgument(LongOptionName = "cat")]
    [CommandDescription("Display file contents")]
    public class CatCmdlet : PromptCmdlet<CmdPromptCmdletContext>
    {
        public override async Task<PromptCmdletResult> ExecuteAsync(CmdPromptCmdletContext context, params string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("Missing File Name");
                    break;

                default:
                    foreach (var arg in args)
                    {
                        try
                        {
                            await ProcessFileAsync(arg)
                                .ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.Message);
                        }
                    }
                    break;
            }

            return PromptCmdletResult.Empty;
        }

        private async Task ProcessFileAsync(string arg)
        {
            using (var fs = File.OpenRead(arg))
            {
                await fs.CopyToAsync(Console.OpenStandardOutput())
                    .ConfigureAwait(false);
            }
        }
    }
}
