using System;
using System.Threading.Tasks;

namespace Core2.Samples.Sample1
{
    public class HelloWorldCommand : Command
    {
        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();

            return Task.FromResult(CommandResult.Empty);
        }
    }
}
