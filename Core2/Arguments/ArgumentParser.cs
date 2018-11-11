using System;
using System.Collections.Generic;

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
            this.RequiresOptionDefinition = true;
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
        /// Every parsed options must be defined in <see cref="OptionsDefinitions"/>
        /// </summary>
        /// <remarks>Defaults to <c>true</c></remarks>
        public bool RequiresOptionDefinition { get; set; }

        /// <summary>
        /// Option suffix comparer
        /// </summary>
        public StringComparison OptionSuffixComparison { get; set; }

        private OptionDefinition GetOptionDefinition(string arg)
        {
            var optionName = arg.Substring(this.OptionSuffix.Length);
            if (!this.OptionsDefinitions.TryGetValue(optionName, out var definition))
            {
                if (!this.RequiresOptionDefinition)
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

        public IEnumerable<Argument> Parse(params string[] args)
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
                        Definition = GetOptionDefinition(arg)
                    };
                }
                else if (currentArgument?.Kind == ArgumentKind.Option)
                {
                    currentArgument.Values.Add(arg);
                    if (currentArgument.Values.Count >= currentArgument.Definition.MaxArguments)
                    {
                        yield return currentArgument;
                        currentArgument = null;
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
    }
}
