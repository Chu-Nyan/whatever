using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Chunity.Utility.NetonsoftJson
{
    public class QuaternionConverter : JsonConverter<Quaternion>
    {
        public override void WriteJson(JsonWriter writer, Quaternion value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);
            writer.WriteProperty("w", value.w);

            writer.WriteEndObject();
        }

        public override Quaternion ReadJson(JsonReader reader, Type objectType, Quaternion existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonSerializationException("Expected StartArray");

            float x = 0, y = 0, z = 0, w = 0;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                    break;
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;
                    reader.Read();
                    if (propertyName == "x") x = Convert.ToSingle(reader.Value);
                    else if (propertyName == "y") y = Convert.ToSingle(reader.Value);
                    else if (propertyName == "z") z = Convert.ToSingle(reader.Value);
                    else if (propertyName == "w") w = Convert.ToSingle(reader.Value);
                }
            }

            return new Quaternion(x, y, z, w);
        }
    }
}
