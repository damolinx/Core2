using Core2.Commands;
using Core2.Commands.Prompt;
using System;

namespace Core2.Sample2.Commands
{
    public class CmdPromptCmdletContext : PromptCmdletContext
    {
        public CmdPromptCmdletContext(CommandContext context, PromptCommand prompt)
            : base(context, prompt)
        {
        }

        /// <summary>
        /// Current environment
        /// </summary>
        /// <remarks>
        /// Used to wrap <see cref="Environment.CurrentDirectory"/> to keep sample simple
        /// </remarks>
        public string CurrentDirectory
        {
            get { return Environment.CurrentDirectory; }
            set { Environment.CurrentDirectory = value; }
        }
    }
}