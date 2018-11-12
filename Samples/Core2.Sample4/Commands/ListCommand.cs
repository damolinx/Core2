using Core2.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core2.Samples.Sample4.Commands
{
    public class ListCommand : Command
    {
        public IEnumerable<int> ProcessIds { get; set; }

        public bool Modules { get; set; }

        public bool Verbose { get; set; }

        public override Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            var processes = (this.ProcessIds?.Any() == true)
                ? this.ProcessIds.Select(id => TryGetGetProcessById(id, out var p) ? p : null).Where(p => p != null).ToArray()
                : Process.GetProcesses();

            var rowFormat = "{0, -26} {1, 8} {2, 12}";
            var fields = new List<(string title, Func<Process, object> value)>
            {
                (title: "Name",   value: new Func<Process, object>(p => ShortProcessName(p, 26))),
                (title: "PID",    value: new Func<Process, object>(p => p.Id)),
                (title: "Memory", value: new Func<Process, object>(p => (p.WorkingSet64 / 1024).ToString() + " K")),
            };

            // Verbose
            if (this.Verbose)
            {
                rowFormat += " {3, 3} {4, 7} {5, 7}";
                fields.Add((title: "SID", value: new Func<Process, object>(p => p.SessionId)));
                fields.Add((title: "Handles", value: new Func<Process, object>(p => p.HandleCount)));
                fields.Add((title: "Threads", value: new Func<Process, object>(p => p.Threads.Count)));
            }

            // Print header
            var header = string.Format(rowFormat, fields.Select(f => f.title).ToArray());
            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));

            // Print values
            foreach (var process in processes)
            {
                Console.WriteLine(rowFormat, fields.Select(f => f.value(process)).ToArray());
                if (this.Modules)
                {
                    Console.Write(" Modules: ");
                    try
                    {
                        Console.WriteLine(string.Join(';', process.Modules.Cast<ProcessModule>().Select(m => m.ModuleName)));
                    }
                    catch
                    {
                        Console.WriteLine("N/A");
                    }
                }
            }

            return Task.FromResult(CommandResult.Empty);
        }

        private static bool TryGetGetProcessById(int id, out Process process)
        {
            try
            {
                process = Process.GetProcessById(id);
                return true;
            }
            catch
            {
                process = null;
                return false;
            }
        }

        private static string ShortProcessName(Process p, int maxLength)
        {
            return (p.ProcessName.Length > maxLength)
                ? (p.ProcessName.Substring(0, maxLength - 3) + "...")
                : p.ProcessName;
        }
    }
}
