using System;

namespace IndependentReserve.DotNetClientApi.Data
{
    public class Event
    {
        public DateTimeOffset Published { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public DateTimeOffset Start { get; set; }

        public int EstimationDurationMinutes { get; set; }

        public bool IsComplete { get; set; }
    }
}