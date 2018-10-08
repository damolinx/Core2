using System;
using System.IO;
using System.Linq;

namespace Core2.Utilities
{
    public static class PathUtilities
    {
        public static string GetFullPath(string path, bool expandEnvironmentVariables = true)
        {
            var expandedPath = expandEnvironmentVariables
                ? Environment.ExpandEnvironmentVariables(path)
                : path;
            return Path.GetFullPath(expandedPath);
        }

        public static string EnsureDirectorySeparatorChar(string currentDirectory)
        {
            return (currentDirectory.Last() == Path.DirectorySeparatorChar)
                ? currentDirectory
                : currentDirectory + Path.DirectorySeparatorChar;
        }
    }
}
