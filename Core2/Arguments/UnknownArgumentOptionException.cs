using System;
using System.Runtime.Serialization;

namespace Core2.Arguments
{
    [Serializable]
    public class UnknownArgumentOptionException : Exception
    {
        public UnknownArgumentOptionException()
        {
        }

        public UnknownArgumentOptionException(string optionName)
            : this(optionName, $"Uknoww option argument.  Name: {optionName}")
        {
        }

        public UnknownArgumentOptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnknownArgumentOptionException(string optionName, string message)
            : base(message)
        {
            this.OptionName = optionName ?? throw new ArgumentNullException(nameof(optionName));
        }

        protected UnknownArgumentOptionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string OptionName { get; }
    }
}
