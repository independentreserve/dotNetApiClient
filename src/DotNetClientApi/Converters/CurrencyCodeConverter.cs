using IndependentReserve.DotNetClientApi.Data;
using IndependentReserve.DotNetClientApi.Helpers;
using Newtonsoft.Json;
using System;

namespace IndependentReserve.DotNetClientApi.Converters
{
    internal class CurrencyCodeConverter : JsonConverter<CurrencyCode>
    {
        public override CurrencyCode ReadJson(JsonReader reader, Type objectType, CurrencyCode existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return CurrencyCodeHelper.Parse((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, CurrencyCode value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
