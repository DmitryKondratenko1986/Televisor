using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA5394 // Do not use insecure randomness
namespace Televisor
{
    /// <summary>
    /// Describes a simple TVSet.
    /// </summary>
    public class SimpleTvset
    {
        private const string UknownModel = "Unknown model";


        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTvset"/> class.
        /// </summary>
        /// <param name="model">Model of the TVSet.</param>
        /// <param name="tvChannelsCapacity">Max channels capacity of the TVSet.</param>
        /// <exception cref="ArgumentNullException">Thrown when model is null.</exception>
        /// <exception cref="ArgumentException">Thrown when tvChannelsCapacity is less or equal zero.</exception>
        public SimpleTvset(string model, int tvChannelsCapacity)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (tvChannelsCapacity <= 0)
            {
                throw new ArgumentException("Channels capacity of the TVSet cannot be less or equal zero.", nameof(tvChannelsCapacity));
            }

            model = model.Trim();

            if (string.IsNullOrEmpty(model))
            {
                this.Model = UknownModel;
            }
            else
            {
                this.Model = model;
            }

            this.TVChannelsCapacity = tvChannelsCapacity;
            this.AvailableChannels = new List<ITvChannel>();
            this.CurrentChannel = NoChannel.GetNoChannel;
        }

        /// <summary>
        /// Gets a model of the TV.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets the max channels capacity of the TVSet.
        /// </summary>
        public int TVChannelsCapacity { get; private set; }

        /// <summary>
        /// Gets a value indicating whether TV is switched on.
        /// </summary>
        public bool IsTurnedOn { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a TV signal is present.
        /// </summary>
        public bool HasTvSignal { get; private set; }

        /// <summary>
        /// Gets the list of channels for the TVSet.
        /// </summary>
        public IList<ITvChannel> AvailableChannels { get; private set; }

        /// <summary>
        /// Gets the value of the number of available channels.
        /// </summary>
        public int AvailableChannelsCount { get; private set; }

        /// <summary>
        /// Gets the current TV channel being watched.
        /// </summary>
        public ITvChannel CurrentChannel { get; private set; }

        /// <summary>
        /// Gets the number of times the TVset is turned on.
        /// </summary>
        public int NumberOfTurnOn { get; private set; }

        /// <summary>
        /// Represents a set of operations performed when the TV is turned on.
        /// </summary>
        public void TurnOn()
        {
            this.IsTurnedOn = true;
            this.NumberOfTurnOn++;

            // If no channels detected before (first time turn on)
            if (this.AvailableChannelsCount == 0)
            {
                this.AutoDetectChannels();
            }

            this.HasTvSignal = this.CurrentChannel.HasTVSignal;
        }

        /// <summary>
        /// Represents a set of operations performed when the TV is turned off.
        /// </summary>
        public void TurnOff()
        {
            if (this.IsTurnedOn)
            {
                this.HasTvSignal = false;
                this.IsTurnedOn = false;
            }
        }

        /// <summary>
        /// Represents a set of operations performed when user switches to the next channel.
        /// </summary>
        public void SwitchNextChannel()
        {
            if (this.IsTurnedOn)
            {
                var currentChannelIndex = this.AvailableChannels.IndexOf(this.CurrentChannel);

                // go to first channel
                if (currentChannelIndex == this.AvailableChannelsCount - 1)
                {
                    this.CurrentChannel = this.AvailableChannels.First();
                }
                else
                {
                    this.CurrentChannel = this.AvailableChannels[++currentChannelIndex];
                }

                this.HasTvSignal = this.CurrentChannel.HasTVSignal;
            }
        }

        /// <summary>
        /// Represents a set of operations performed when user switches to the previous channel.
        /// </summary>
        public void SwitchPreviousChannel()
        {
            if (this.IsTurnedOn)
            {
                var currentChannelIndex = this.AvailableChannels.IndexOf(this.CurrentChannel);

                // go to first channel
                if (currentChannelIndex == 0)
                {
                    this.CurrentChannel = this.AvailableChannels.Last();
                }
                else
                {
                    this.CurrentChannel = this.AvailableChannels[--currentChannelIndex];
                }

                this.HasTvSignal = this.CurrentChannel.HasTVSignal;
            }
        }

        /// <summary>
        /// Represents a set of operations performed when user switches to the channel with the specific number.
        /// </summary>
        /// <param name="channelNumber">The number of the TV channel to which the user switches.</param>
        /// <exception cref="ArgumentException">Thrown when channelNumber is less or equal zero.</exception>
        public void SwitchTo(int channelNumber)
        {
            if (channelNumber <= 0)
            {
                throw new ArgumentException("Channel number cannot be less or equal zero.", nameof(channelNumber));
            }

            if (channelNumber >= this.AvailableChannelsCount)
            {
                channelNumber = this.AvailableChannelsCount - 1;
            }

            if (this.IsTurnedOn)
            {
                this.CurrentChannel = this.AvailableChannels[channelNumber];
                this.HasTvSignal = this.CurrentChannel.HasTVSignal;
            }
        }

        /// <summary>
        /// Simulates!!! an automatic search for available TV channels.
        /// </summary>
        public void AutoDetectChannels()
        {
            var random = new Random();
            this.AvailableChannelsCount = random.Next(0, this.TVChannelsCapacity);

            for (int i = 0; i < this.AvailableChannelsCount; i++)
            {
                ITvChannel channel = new TVChannel($"Channel#{i + 1}", true);
                this.AvailableChannels.Add(channel);
            }

            this.CurrentChannel = this.AvailableChannels.Last();
            this.HasTvSignal = this.CurrentChannel.HasTVSignal;

            if (this.AvailableChannelsCount == 0)
            {
                this.HasTvSignal = false;
                this.CurrentChannel = NoChannel.GetNoChannel;
            }
        }
    }
}
