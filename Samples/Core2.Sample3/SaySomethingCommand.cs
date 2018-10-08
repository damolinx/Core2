using System;
using System.Threading.Tasks;

namespace Core2.Sample3
{
    public class SaySomethingCommand : Command
    {
        public SaySomethingCommand(string text)
        {
            this.Text = text ?? "Nothing to say";
        }

        public string Text { get; }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            Console.WriteLine(this.Text);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue ... ");
            Console.ReadKey(intercept: true);
            return Task.FromResult(CommandResult.Empty);
        }
    }
}
