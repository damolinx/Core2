using System;
using System.ComponentModel;
using System.Linq;

namespace Core2.Commands.Attributes
{
    /// <summary>
    /// Specifies a description for a command/cmdlet
    /// </summary>
    /// <remarks>
    /// <see cref="DescriptionAttribute"/> is equivalent for all cases.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CommandDescriptionAttribute : DescriptionAttribute
    {
        public CommandDescriptionAttribute()
        {
        }

        public CommandDescriptionAttribute(string description)
            : base(description)
        {
        }

        public static string GetDescription(Type cmdletType)
        {
            // Support DescriptionAttribute
            return cmdletType.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)
                .Cast<DescriptionAttribute>()
                .Select(attrib => attrib.Description)
                .FirstOrDefault();
        }
    }
}
