using System;
using System.Text;

namespace SampleApplication.Extensions
{
    public class HumanReadableTimeSpanOptions
    {
        public HumanReadableLabelMode LabelMode { get; set; }

        public HumanReadableTimeLabels CustomLabels { get; set; }
    }

    public class HumanReadableTimeLabels
    {

        public string Day { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }
        public string Second { get; set; }
        public string Millisecond { get; set; }
    }

    public enum HumanReadableLabelMode
    {
        /// <summary>
        /// Hours Minutes Seconds
        /// </summary>
        Verbose,

        /// <summary>
        /// Hrs Mins Secs
        /// </summary>
        Abbreviated,

        Custom
    }

    public static class TimeSpanExtensions
    {
        public static string ToHumanReadable(this TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue)
            {
                return "<null>";
            }
            return ToHumanReadable(timeSpan.Value, HumanReadableLabelMode.Verbose);
        }

        /// <summary>
        /// Converts a timespan to a human readable format,
        /// eg. d days, h hours, m mins, s secs.
        /// </summary>
        public static string ToHumanReadable(this TimeSpan timeSpan)
        {
            return ToHumanReadable(timeSpan, HumanReadableLabelMode.Verbose);
        }

        public static string ToHumanReadable(this TimeSpan timeSpan, HumanReadableLabelMode labelMode)
        {
            var options2 = new HumanReadableTimeSpanOptions { LabelMode = labelMode };
            return ToHumanReadable(timeSpan, options2);
        }


        /// <summary>
        /// Converts a timespan to a human readable format,
        /// eg. d days, h hours, m mins, s secs.
        ///
        /// https://github.com/neutmute/kraken.utilities/blob/develop/source/Kraken.Core/Extensions/TimeSpanExtensions.cs
        /// </summary>
        /// <returns>Human readable time duration.</returns>
        public static string ToHumanReadable(this TimeSpan timeSpan, HumanReadableTimeSpanOptions options)
        {
            if (timeSpan.Equals(TimeSpan.MaxValue))
            {
                return "TimeSpan.Max";
            }
            if (timeSpan.Equals(TimeSpan.MinValue))
            {
                return "TimeSpan.Min";
            }
            decimal seconds = Convert.ToDecimal(timeSpan.TotalSeconds);
            var labels = new HumanReadableTimeLabels();
            switch (options.LabelMode)
            {
                case HumanReadableLabelMode.Abbreviated:
                    labels.Day = "day";
                    labels.Hour = "hr";
                    labels.Minute = "min";
                    labels.Second = "sec";
                    labels.Millisecond = "ms";
                    break;
                case HumanReadableLabelMode.Verbose:
                    labels.Day = "day";
                    labels.Hour = "hour";
                    labels.Minute = "minute";
                    labels.Second = "second";
                    labels.Millisecond = "millisecond";
                    break;
                case HumanReadableLabelMode.Custom:
                    labels = options.CustomLabels ?? throw new Exception("CustomLabels property required");
                    break;
                default:
                    throw new Exception("A new label mode?");
            }

            if (seconds == 0)
            {
                return $"0 {labels.Second}s";
            }

            StringBuilder sb = new StringBuilder();
            if (seconds >= 86400)
            {
                sb.AppendFormat("{0} {2}{1}"
                    , (long)seconds / 86400
                    , seconds >= 86400 * 2 ? "s" : string.Empty
                    , labels.Day);

                seconds -= (long)(seconds / 86400) * 86400;
            }
            if (seconds >= 3600)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat(
                    "{0} {1}{2}"
                    , (long)seconds / 3600
                    , labels.Hour
                    , seconds >= 3600 * 2 ? "s" : string.Empty
                    );
                seconds -= (long)(seconds / 3600) * 3600;
            }
            if (seconds >= 60)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.AppendFormat(
                    "{0} {1}{2}"
                    , (long)seconds / 60
                     , labels.Minute
                    , seconds >= 60 * 2 ? "s" : string.Empty
                    );
                seconds -= (long)(seconds / 60) * 60;
            }
            if (seconds > 0)
            {
                if (sb.Length > 0)
                {
                    sb.AppendFormat(
                        ", {0} {1}{2}"
                        , (long)seconds
                        , labels.Second
                        , seconds == 1 ? string.Empty : "s");
                }
                else
                {
                    if (seconds == (long)seconds)
                    {
                        sb.AppendFormat("{0} {1}", (long)seconds, labels.Second);
                    }
                    else if (seconds > decimal.One)
                    {
                        sb.AppendFormat("{0} {1}s", seconds.ToString("N2"), labels.Second);
                    }
                    else if (timeSpan.TotalMilliseconds >= 1)
                    {
                        var plural = labels.Millisecond == "ms" ? string.Empty : "s";
                        sb.AppendFormat("{0} {1}{2}", timeSpan.TotalMilliseconds.ToString("N0"), labels.Millisecond, plural);
                    }
                    else
                    {
                        var plural = Convert.ToDecimal(timeSpan.TotalMilliseconds) == Decimal.One ? "" : "s";
                        sb.AppendFormat("{0} {1}{2}", timeSpan.TotalMilliseconds.ToString("N2"), labels.Millisecond, plural);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
