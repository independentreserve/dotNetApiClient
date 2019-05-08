using Newtonsoft.Json;
using System;
using System.Xml;

namespace IndependentReserve.DotNetClientApi.Converters
{
    // https://stackoverflow.com/questions/12466188/how-do-i-convert-an-iso8601-timespan-to-a-c-sharp-timespan
    public class TimeSpanToIsoConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (!(value is TimeSpan))
            {
                throw new JsonSerializationException("Expected TimeSpan object value.");
            }

            var timeSpan = (TimeSpan)value;
            writer.WriteValue(XmlConvert.ToString(timeSpan));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return XmlConvert.ToTimeSpan((string)reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan);
        }
    }
}
