using Core2.Commands.Prompt;
using Core2.Sample2.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [Description("List directory contents")]
    public class DirectoryCmdlet : PromptCmdlet<CmdPromptCmdletContext>
    {
        public override Task<PromptCmdletResult> ExecuteAsync(CmdPromptCmdletContext context, params string[] args)
        {
            IEnumerable<string> paths;

            if (args.Any())
            {
                paths = args.SelectMany(arg => Directory.Exists(arg) ? Directory.EnumerateFileSystemEntries(arg) : new[] { arg });
            }
            else
            {
                paths = Directory.EnumerateFileSystemEntries(context.CurrentDirectory);
            }

            foreach (var path in paths)
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    Console.WriteLine(fileInfo.Name);
                }
                else
                {
                    Console.Error.WriteLine("File Not Found");
                }
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }
    }
}
