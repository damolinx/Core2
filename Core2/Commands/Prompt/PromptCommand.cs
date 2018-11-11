using Core2.Commands.Prompt.Parsers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core2.Commands.Prompt
{
    public class PromptCommand : Command
    {
        public PromptCommand()
            : this(new ActionRestPromptInputParser())
        {
        }

        public PromptCommand(PromptInputParser parser)
        {
            this.Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            this.RequiresClearScreen = false;
            this.RequiresCursor = true;
        }

        /// <summary>
        /// Gets or sets prompt parser
        /// </summary>
        /// <remarks>
        /// if <c>null</c>, default <see cref="ActionRestPromptInputParser"/> is used
        /// </remarks>
        public PromptInputParser Parser { get; set; }

        /// <summary>
        /// Gets or sets whether available cmdlets list is dynamic.
        /// </summary>
        /// <remarks>
        /// When dynamic, <see cref="CreateCmdlets(PromptCmdletContext)"/>
        /// is invoked for every cmdlet resolution instead of just at startup.
        /// </remarks>
        public bool UseDynamicCmdlets { get; set; }

        protected virtual PromptCmdletContext CreateCmdletContext(CommandContext context)
        {
            return new PromptCmdletContext(context, this);
        }

        protected virtual IReadOnlyDictionary<string, PromptCmdlet> CreateCmdlets(PromptCmdletContext context)
        {
            return new Dictionary<string, PromptCmdlet>(StringComparer.OrdinalIgnoreCase);
        }

        public sealed override async Task<CommandResult> ExecuteAsync(CommandContext context)
        {
            var cmdletContext = CreateCmdletContext(context);

            while (!cmdletContext.Exit)
            {
                var promptText = GetPromptText(cmdletContext);

                Console.Write("{0} ", promptText);
                var input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(input))
                {
                    if (this.Parser.TryParseInput(cmdletContext, input, out var parsedInput))
                    {
                        // Map input to cmdlet
                        if (this.GetCmdlets(cmdletContext).TryGetValue(parsedInput.cmdletName, out var cmdlet))
                        {
                            try
                            {
                                // Execute cmdlet
                                var cmdletResult = await cmdlet.ExecuteAsync(cmdletContext, parsedInput.cmdletArguments)
                                    .ConfigureAwait(false);

                                //TODO: config
                                // Write cmdlet result
                                //Console.WriteLine(cmdletResult.ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            Console.WriteLine("'{0}' is not defined", parsedInput.cmdletName);
                        }
                    }
                    //TODO: config
                    Console.WriteLine();
                }
            }

            return CommandResult.Empty;
        }

        protected internal IReadOnlyDictionary<string, PromptCmdlet> GetCmdlets(PromptCmdletContext context)
        {
            const string CmdletsKey = "Prompt.Cmdlets";
            IReadOnlyDictionary<string, PromptCmdlet> cmdlets;

            if (!this.UseDynamicCmdlets)
            {
                if (!context.Annotations.TryGetValue(CmdletsKey, out var storedCmdlets))
                {
                    storedCmdlets = CreateCmdlets(context);
                    context.Annotations[CmdletsKey] = storedCmdlets;
                }

                cmdlets = (IReadOnlyDictionary<string, PromptCmdlet>)storedCmdlets;
            }
            else
            {
                cmdlets = CreateCmdlets(context);
            }

            return cmdlets;
        }

        protected virtual string GetPromptText(PromptCmdletContext context)
        {
            return ">";
        }
    }
}
