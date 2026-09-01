using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Chunity.Utility.NetonsoftJson
{
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            writer.WriteProperty("x", value.x);
            writer.WriteProperty("y", value.y);
            writer.WriteProperty("z", value.z);

            writer.WriteEndObject();
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonSerializationException("Expected StartArray");

            float x = 0, y = 0, z = 0;

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
                }
            }

            return new Vector3(x, y, z);
        }
    }
}
