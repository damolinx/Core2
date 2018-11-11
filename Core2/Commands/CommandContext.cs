using Core2.Common;
using System;

namespace Core2.Commands
{
    public class CommandContext : ContextBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandContext(ProgramBase program, CommandContext parent = null)
            : base(parent)
        {
            this.Program = program ?? throw new ArgumentNullException(nameof(program));
        }

        /// <summary>
        /// Program
        /// </summary>
        public ProgramBase Program { get; }
    }
}