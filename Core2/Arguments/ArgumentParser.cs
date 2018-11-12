using System;
using System.Collections.Generic;
using System.Linq;

namespace Core2.Arguments
{
    public class ArgumentParser
    {
        public const string DefaultOptionSuffix = "-";

        public ArgumentParser()
            : this(DefaultOptionSuffix, StringComparer.Ordinal)
        {
        }

        public ArgumentParser(string optionSuffix)
            : this(optionSuffix, StringComparer.Ordinal)
        {
        }

        public ArgumentParser(StringComparer optionComparer)
            : this(DefaultOptionSuffix, optionComparer)
        {
        }

        public ArgumentParser(string optionSuffix, StringComparer optionComparer)
        {
            this.OptionsDefinitions = new OptionDefinitionDictionary(optionComparer);
            this.OptionSuffix = optionSuffix;
            this.OptionSuffixComparison = StringComparison.Ordinal;
        }

        /// <summary>
        /// Option argument definitions
        /// </summary>
        public OptionDefinitionDictionary OptionsDefinitions { get; }

        /// <summary>
        /// Option suffix (e.g. -, /)
        /// </summary>
        public string OptionSuffix { get; }

        /// <summary>
        /// Option suffix comparer
        /// </summary>
        public StringComparison OptionSuffixComparison { get; set; }

        private OptionDefinition GetOptionDefinition(string arg, bool requireDefinition)
        {
            var optionName = arg.Substring(this.OptionSuffix.Length);
            if (!this.OptionsDefinitions.TryGetValue(optionName, out var definition))
            {
                if (!requireDefinition)
                {
                    definition = new OptionDefinition(optionName);
                    this.OptionsDefinitions.Add(definition);
                }
                else
                {
                    throw new UnknownOptionException(optionName);
                }
            }

            return definition;
        }

        private IEnumerable<Argument> InnerParse(string[] args, bool requireDefinition)
        {
            Argument currentArgument = null;
            foreach (var arg in args)
            {
                if (arg.StartsWith(this.OptionSuffix, this.OptionSuffixComparison))
                {
                    if (currentArgument != null)
                    {
                        yield return currentArgument;
                        currentArgument = null;
                    }

                    currentArgument = new Argument(ArgumentKind.Option)
                    {
                        Definition = GetOptionDefinition(arg, requireDefinition)
                    };
                }
                else if (currentArgument?.Kind == ArgumentKind.Option)
                {
                    if (currentArgument.Values.Count < currentArgument.Definition.MaxArguments)
                    {
                        currentArgument.Values.Add(arg);
                    }
                    else
                    { 
                        yield return currentArgument;
                        currentArgument = null;
                        yield return new Argument(ArgumentKind.Literal, new[] { arg });
                    }
                }
                else
                {
                    yield return new Argument(ArgumentKind.Literal, new[] { arg });
                }
            }

            if (currentArgument != null)
            {
                yield return currentArgument;
                currentArgument = null;
            }
        }

        public IEnumerable<Argument> Parse(string[] args, bool requireDefinition)
        {
            // Ensure no unevaluated enumeration is returned
            return InnerParse(args, requireDefinition).ToList();
        }
    }
}
