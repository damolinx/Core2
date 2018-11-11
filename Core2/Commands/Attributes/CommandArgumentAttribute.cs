using System;
using System.Collections.Generic;

namespace Core2.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CommandArgumentAttribute : Attribute
    {
        public string LongOptionName { get; set; }

        public string ShortOptionName { get; set; }

        public static IEnumerable<string> GetOptionNames(Type cmdletType)
        {
            foreach (CommandArgumentAttribute arg in cmdletType.GetCustomAttributes(typeof(CommandArgumentAttribute), inherit: false))
            {
                if (!string.IsNullOrWhiteSpace(arg.LongOptionName))
                {
                    yield return arg.LongOptionName;
                }

                if (!string.IsNullOrWhiteSpace(arg.ShortOptionName))
                {
                    yield return arg.ShortOptionName;
                }
            }
        }
    }
}
