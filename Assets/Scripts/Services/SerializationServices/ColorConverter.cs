using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MZTATest.Services.SerializationConverters
{
    public class ColorConverter : JsonConverter<Color>
    {
        public static ColorConverter Default { get; } = new ColorConverter();

        enum Props
        {
            R,
            G,
            B,
            A
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var data = (string)reader.Value;
            var parts = data.Split(';');
            float r = 0, g = 0, b = 0, a = 0;

            foreach (var part in parts)
            {
                var s = part.Split(':');
                if (s.Length == 2)
                    if (Enum.TryParse(s[0].Trim(), true, out Props prop))
                        if (float.TryParse(s[1].Trim(), out float value))
                            switch (prop)
                            {
                                case Props.R: r = value; continue;
                                case Props.G: g = value; continue;
                                case Props.B: b = value; continue;
                                case Props.A: a = value; continue;
                            }
                throw new ArgumentException();
            }

            return new Color(r, g, b, a);
        }

        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            writer.WriteValue($"r:{value.r}; g:{value.g}; b:{value.b}; a:{value.a}");
        }
    }
}