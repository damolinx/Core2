using Core2.Commands.Prompt;

namespace Core2.Samples.Sample1
{
    class Program : Core2.ProgramBase
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Commands.Push(new PromptCommand());
            program.Execute(args);
        }
    }
}
