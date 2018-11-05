using System;
using System.Collections.Generic;
using System.Linq;

namespace Core2.Arguments
{
    public class ArgumentParser
    {
        public const char DefaultOptionSuffix = '-';

        private readonly char _optionSuffix;
        private readonly IDictionary<string, OptionArgumentDefinition> _optionsDefinitions;

        public ArgumentParser()
            : this(DefaultOptionSuffix, StringComparer.Ordinal)
        {
        }

        public ArgumentParser(char optionSuffix)
            : this(optionSuffix, StringComparer.Ordinal)
        {
        }

        public ArgumentParser(StringComparer optionComparer)
            : this(DefaultOptionSuffix, optionComparer)
        {
        }

        public ArgumentParser(char optionSuffix, StringComparer optionComparer)
        {
            _optionsDefinitions = new Dictionary<string, OptionArgumentDefinition>(optionComparer);
            _optionSuffix = optionSuffix;
        }

        public bool AllowUnregisteredOptions { get; set; }

        public IEnumerable<Argument> Parse(params string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.FirstOrDefault() == _optionSuffix)
                {
                    yield return ParseOption(args, ref i);
                }
                else
                {
                    yield return ParseLiteral(arg);
                }
            }
        }

        private Argument ParseLiteral(string value)
        {
            var arg = new Argument();
            arg.Values.Add(value);
            return arg;
        }

        private Argument ParseOption(string[] args, ref int argIndex)
        {
            var optionName = args[argIndex].Substring(1);

            if (!_optionsDefinitions.TryGetValue(optionName, out var optionArgDef))
            {
                if (this.AllowUnregisteredOptions)
                {
                    optionArgDef = new OptionArgumentDefinition(optionName);
                    this.RegisterOption(optionArgDef);
                }
                else
                {
                    throw new UnknownArgumentOptionException(optionName);
                }
            }

            var arg = new Argument
            {
                Option = optionArgDef
            };

            for (var i = 0; arg.Values.Count < optionArgDef.MaxArguments && argIndex < args.Length; i++)
            {
                arg.Values.Add(args[++argIndex]);
            }

            return arg;
        }

        public void RegisterOption(OptionArgumentDefinition optionArgDefinition)
        {
            if (optionArgDefinition == null)
            {
                throw new ArgumentNullException(nameof(optionArgDefinition));
            }
            _optionsDefinitions[optionArgDefinition.Name] = optionArgDefinition;
        }

        public void UnregisterOption(OptionArgumentDefinition option)
        {
            if (option == null)
            {
                throw new ArgumentNullException(nameof(option));
            }
            UnregisterOption(option.Name);
        }

        public void UnregisterOption(string optionName)
        {
            if (string.IsNullOrWhiteSpace(optionName))
            {
                throw new ArgumentException("Cannot be empty", nameof(optionName));
            }
            _optionsDefinitions.Remove(optionName);
        }
    }
}
