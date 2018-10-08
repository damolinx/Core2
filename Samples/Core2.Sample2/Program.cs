using Core2.Sample2.Commands;

namespace Core2.Samples.Sample2
{
    class Program : Core2.ProgramBase
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Commands.Push(new CmdPromptCommand());
            program.Execute(args);
        }
    }
}
