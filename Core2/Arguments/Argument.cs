using System;
using System.Collections.Generic;

namespace Core2.Arguments
{
    public class Argument
    {
        public Argument(ArgumentKind kind)
            : this(kind, new List<string>())
        {
        }

        public Argument(ArgumentKind kind, IList<string> values)
        {
            this.Kind = kind;
            this.Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public ArgumentKind Kind { get; }

        public OptionDefinition Definition { get; set; }

        public IList<string> Values { get; }
    }
}
