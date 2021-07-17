using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Televisor
{
    /// <summary>
    /// Represents the absence of a TV channel ("null-object pattern").
    /// </summary>
    public class NoChannel : ITvChannel
    {
        private const string NoChannelName = "No channel";
        private static NoChannel instance;

        private NoChannel()
        {
        }

        /// <summary>
        /// Gets single instance of NoChannel object.
        /// </summary>
        /// <returns>Single instance of NoChannel object.</returns>
        public static NoChannel GetNoChannel
        {
            get
            {
                if (instance == null)
                {
                    instance = new NoChannel();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the default name of NoChannel class.
        /// </summary>
        public string Name { get => NoChannelName; }

        /// <summary>
        /// Gets a value indicating whether TV signal available. Always 'false'.
        /// </summary>
        public bool HasTVSignal { get => false; }
    }
}
