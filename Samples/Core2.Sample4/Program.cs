using Core2.Arguments;
using Core2.Commands;
using Core2.Samples.Sample4.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core2.Samples.Sample4
{
    class Program : Core2.ProgramBase
    {
        public const string HelpOption = "?";

        static void Main(string[] args)
        {
            var program = new Program
            {
                Arguments = CreateParser().Parse(args, requireDefinition: true)
            };

            var command = CreateCommand(program.Arguments);

            program.Commands.Push(command);
            program.Execute(args);
            Console.ReadKey();
        }

        public IEnumerable<Argument> Arguments { get; set; }



        private static Command CreateCommand(IEnumerable<Argument> arguments)
        {
            Command command;

            if (arguments.Any(arg => arg.Definition.Name == HelpOption))
            {
                command = new HelpCommand();
            }
            else
            {
                var listCommand = new ListCommand();
                foreach (var arg in arguments)
                {
                    switch (arg.Definition.Name)
                    {
                        case "m":
                            listCommand.Modules = true;
                            break;

                        case "pid":
                            listCommand.ProcessIds = arg.Values.Select(int.Parse).ToArray();
                            break;

                        case "v":
                            listCommand.Verbose = true;
                            break;
                    }
                }
                command = listCommand;
            }
            return command;
        }

        private static ArgumentParser CreateParser()
        {
            var parser = new ArgumentParser("/");
            parser.OptionsDefinitions.Add(new OptionDefinition("?"));
            parser.OptionsDefinitions.Add(new OptionDefinition("m"));
            parser.OptionsDefinitions.Add(new OptionDefinition("pid", Int32.MaxValue));
            parser.OptionsDefinitions.Add(new OptionDefinition("v"));
            return parser;
        }
    }
}
