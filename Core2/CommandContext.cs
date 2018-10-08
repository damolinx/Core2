using System;

namespace Core2
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