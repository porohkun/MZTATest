using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MZTATest.Services.SerializationConverters
{
    public class Vector2Converter : JsonConverter<Vector2>
    {
        public static Vector2Converter Default { get; } = new Vector2Converter();

        enum Props
        {
            X,
            Y
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var data = (string)reader.Value;
            var parts = data.Split(';');
            float x = 0, y = 0;

            foreach (var part in parts)
            {
                var s = part.Split(':');
                if (s.Length == 2)
                    if (Enum.TryParse(s[0].Trim(), true, out Props prop))
                        if (float.TryParse(s[1].Trim(), out float value))
                            switch (prop)
                            {
                                case Props.X: x = value; continue;
                                case Props.Y: y = value; continue;
                            }
                throw new ArgumentException();
            }

            return new Vector2(x, y);
        }

        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {
            writer.WriteValue($"x:{value.x}; y:{value.y}");
        }
    }
}