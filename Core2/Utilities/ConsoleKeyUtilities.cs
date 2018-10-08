using System;

namespace Core2.Utilities
{
    internal static class ConsoleKeyUtilities
    {
        /// <summary>
        /// Converts <see cref="ConsoleKey"/> into a UI-friendly string
        /// </summary>
        public static string AsUserString(ConsoleKey key)
        {
            var keyText = key.ToString();
            return (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
                ? keyText.Substring(1)
                : keyText;
        }
    }
}
