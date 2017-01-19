using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Cozify
{
    public class CozifyJsonConverter : JsonConverter
    {
        private CamelCasePropertyNamesContractResolver camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetCustomAttribute<TypeNameAttribute>() != null;
            //return objectType.IsClass && !objectType.IsArray && objectType != typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(!objectType.IsAbstract)
            {
                var result = Activator.CreateInstance(objectType);
                serializer.Populate(reader, result);
                return result;
            }
            var jObject = JObject.Load(reader);           
            var type = (string)jObject.Property("type");
            var concreteType = this.GetType().Assembly.GetTypes().SingleOrDefault(t => t.GetCustomAttribute<TypeNameAttribute>()?.Type == type);
            if(concreteType != null)
            {
                return jObject.ToObject(concreteType, serializer);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType().GetCustomAttribute<TypeNameAttribute>().Type;           
            var jObject = new JObject();
            jObject.Add("type", new JValue(type));
            foreach (var prop in value.GetType().GetProperties())
            {
                var propValue = prop.GetValue(value);
                var name = prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? prop.Name;
                if (propValue != null)
                {
                    jObject.Add(camelCaseContractResolver.GetResolvedPropertyName(name), JToken.FromObject(propValue, serializer));
                }

            }
            jObject.WriteTo(writer);
        }
    }
}
