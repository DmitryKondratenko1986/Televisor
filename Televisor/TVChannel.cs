using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Televisor
{
    /// <summary>
    /// Describes a TV channel.
    /// </summary>
    public class TVChannel : ITvChannel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TVChannel"/> class.
        /// </summary>
        /// <param name="name">The name of TV channel.</param>
        /// <param name="hasTVsignal">A value indicating whether the channel has a correct signal right now 
        /// (not a white noise).
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when channel name is null.</exception>
        public TVChannel(string name, bool hasTVsignal)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Channel name cannot be null or empty or whitespace.", nameof(name));
            }

            this.Name = name;
            this.HasTVSignal = hasTVsignal;
        }

        /// <summary>
        /// Gets the name of the TV channel.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the channel has a correct signal right now (not a white noise).
        /// </summary>
        public bool HasTVSignal { get; set; }
    }
}
