using System;
using System.Collections.Generic;

namespace Core2.Common
{
    /// <summary>
    /// Base context
    /// </summary>
    public abstract class ContextBase
    {
        protected ContextBase(ContextBase parent = null)
        {
            this.Annotations = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            this.Parent = parent;
        }

        /// <summary>
        /// Context property bag
        /// </summary>
        public IDictionary<string, object> Annotations { get; }

        /// <summary>
        /// Parent context
        /// </summary>
        protected ContextBase Parent { get; }
    }
}
