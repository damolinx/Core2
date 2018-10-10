using Core2.Commands.Prompt;
using System;
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
            var paths = args.Any()
                ? args.SelectMany(arg => Directory.Exists(arg) ? Directory.EnumerateFileSystemEntries(arg) : new[] { arg })
                : new[] { Environment.CurrentDirectory };

            foreach (var path in paths)
            {
                var fileInfo = new FileInfo(path);
                Console.WriteLine(fileInfo.Name);
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
