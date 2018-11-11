using System;

namespace Core2.Arguments
{
    public class OptionDefinition
    {
        public OptionDefinition(string name, int maxArguments = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Cannot be empty", nameof(name));
            }

            this.MaxArguments = maxArguments;
            this.Name = name;
        }

        public int MaxArguments { get;}

        public string Name { get; }
    }
}
