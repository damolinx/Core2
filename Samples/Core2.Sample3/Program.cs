using Core2.Sample3.Menus;

namespace Core2.Sample3
{
    class Program : Core2.ProgramBase
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Commands.Push(new DrivesMenu());
            program.Execute(args);
        }
    }
}
