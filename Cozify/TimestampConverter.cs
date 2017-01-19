using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    public class TimestampConverter : JsonConverter
    {
        private static readonly DateTime start = new DateTime(1970, 1, 1);
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var timestamp = reader.Value as long?;
            if (timestamp.HasValue)
            {
                return start.AddMilliseconds(timestamp.Value);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var time = value as DateTime?;
            if (time.HasValue)
            {
                writer.WriteValue((int)(time.Value - start).TotalMilliseconds);
            }
        }
    }
}
