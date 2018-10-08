using Core2.Commands.Prompt.Parsers;
using Newtonsoft.Json;

namespace Core2.Commands.Prompt
{
    public class PromptConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Gets or sets prompt parser
        /// </summary>
        /// <remarks>
        /// if <c>null</c>, default <see cref="SimplePromptInputParser"/> is used
        /// </remarks>
        [JsonIgnore]
        public PromptInputParser Parser { get; set; }

        /// <summary>
        /// Gets or sets whether available cmdlets list is dynamic.
        /// </summary>
        /// <remarks>
        /// When dynamic, <see cref="PromptCommand.CreateCmdlets(PromptCmdletContext)"/>
        /// is invoked for every cmdlet resolution instead of just at startup.
        /// </remarks>
        [JsonProperty]
        public bool UseDynamicCmdlets { get; set; }
    }
}
