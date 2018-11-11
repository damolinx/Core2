using System;
using System.Collections.Generic;

namespace Core2.Arguments
{
    public class OptionDefinitionDictionary : Dictionary<string, OptionDefinition>
    {
        public OptionDefinitionDictionary()
         : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public OptionDefinitionDictionary(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public void Add(OptionDefinition definition)
        {
            this.Add(definition.Name, definition);
        }
    }
}
