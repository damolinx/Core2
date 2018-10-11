using Core2.Commands.Prompt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [Description("List directory contents")]
    public class DirectoryCmdlet : PromptCmdlet
    {
        public override Task<PromptCmdletResult> ExecuteAsync(PromptCmdletContext context, params string[] args)
        {
            IEnumerable<string> paths;

            if (args.Any())
            {
                paths = args.SelectMany(arg => Directory.Exists(arg) ? Directory.EnumerateFileSystemEntries(arg) : new[] { arg });
            }
            else
            {
                paths = Directory.EnumerateFileSystemEntries(Environment.CurrentDirectory);
            }

            foreach (var path in paths)
            {
                var fileInfo = new FileInfo(path);
                Console.WriteLine(fileInfo.Exists ? fileInfo.Name : "File Not Found");
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
