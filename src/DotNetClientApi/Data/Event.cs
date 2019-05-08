using System;
using IndependentReserve.DotNetClientApi.Converters;
using Newtonsoft.Json;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class Event
    {
        public DateTimeOffset Published { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Start { get; set; }

        [JsonConverter(typeof(TimeSpanToIsoConverter))]
        public TimeSpan EstimatedDuration { get; set; }

        public bool IsComplete { get; set; }
    }
}