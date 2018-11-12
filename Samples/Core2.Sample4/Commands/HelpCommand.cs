using Core2.Commands;
using System;
using System.Threading.Tasks;

namespace Core2.Samples.Sample4.Commands
{
    public class HelpCommand : Command
    {
        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            Console.WriteLine("Displays a list of currently running processes on local machine.");
            Console.WriteLine();
            return Task.FromResult(CommandResult.Empty);
        }
    }
}
