using System.Collections.Generic;

namespace Core2.Arguments
{
    public class Argument
    {
        public Argument()
        {
            this.Values = new List<string>();
        }

        public OptionArgumentDefinition Option { get; set; }

        public IList<string> Values { get; }
    }
}
