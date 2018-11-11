using System;
using System.Runtime.Serialization;

namespace Core2.Arguments
{
    [Serializable]
    public class UnknownOptionException : Exception
    {
        public UnknownOptionException()
        {
        }

        public UnknownOptionException(string optionName)
            : this(optionName, $"Uknown option argument.  Name: {optionName}")
        {
        }

        public UnknownOptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UnknownOptionException(string optionName, string message)
            : base(message)
        {
            this.OptionName = optionName ?? throw new ArgumentNullException(nameof(optionName));
        }

        protected UnknownOptionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public string OptionName { get; }
    }
}
