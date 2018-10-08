using Core2.Commands.Prompt;
using Core2.Sample2.Commands;
using Core2.Utilities;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Sample2.Cmdlets
{
    [Description("List directory contents")]
    public class DirectoryCmdlet : PromptCmdlet<CmdPromptContext>
    {
        public DirectoryCmdlet()
        {
        }

        public override Task<PromptCmdletResult> ExecuteAsync(CmdPromptContext context, params string[] args)
        {
            var paths = args.Any() ? args : new[] { context.CurrentDirectory };
            var formatter = GetFormatter(context);

            foreach (var path in paths)
            {
                foreach (var fsInfo in Directory.EnumerateFileSystemEntries(path).Select(p => new FileInfo(p)))
                {
                    Console.WriteLine(formatter(fsInfo));
                }
            }

            return Task.FromResult(PromptCmdletResult.Empty);
        }

        private static Func<FileSystemInfo, string> GetFormatter(CmdPromptContext context)
        {
            return (fsInfo) =>
            {
                return (fsInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory
                ? PathUtilities.EnsureDirectorySeparatorChar(fsInfo.Name)
                : fsInfo.Name;
            };
        }
    }
}
