using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Televisor
{
    /// <summary>
    /// Represents a TV channel.
    /// </summary>
    public interface ITvChannel
    {
        /// <summary>
        /// Gets the name of the TV channel.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the channel has a correct signal right now.
        /// </summary>
        public bool HasTVSignal { get; }

    }
}
