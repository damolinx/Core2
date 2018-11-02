using System;

namespace Core2.Commands
{
    public class CommandContext
    {
        public CommandContext(ProgramBase program)
        {
            this.Program = program ?? throw new ArgumentNullException(nameof(program));
        }

        public ProgramBase Program { get; }
    }
}