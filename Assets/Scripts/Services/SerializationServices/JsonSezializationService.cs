using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MZTATest.Services
{
    public class JsonSezializationService : ISerializationService
    {
        JsonSerializer _serializer;

        public T Deserialize<T>(string data)
        {
            using (var reader = new JsonTextReader(new StringReader(data)))
            {
                return GetSerializer().Deserialize<T>(reader);
            }
        }

        public string Serialize(object data)
        {
            var sb = new StringBuilder();
            using (var writer = new JsonTextWriter(new StringWriter(sb)))
            {
                GetSerializer().Serialize(writer, data);
            }
            return sb.ToString();
        }

        private JsonSerializer GetSerializer()
        {
            if (_serializer == null)
            {
                _serializer = new JsonSerializer()
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };
                _serializer.Converters.Add(SerializationConverters.ColorConverter.Default);
                _serializer.Converters.Add(SerializationConverters.Vector2Converter.Default);
            }
            return _serializer;
        }
    }
}
