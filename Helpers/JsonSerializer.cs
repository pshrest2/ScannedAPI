using System.Text;
using Newtonsoft.Json;

namespace ScannedAPI.Helpers
{
    public static class JsonSerializer
    {
        private static JsonSerializerSettings settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        public static byte[] Serialize(object data)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, settings));
        }

        public static object Deserialize(byte[] data)
        {
            if (data == null) return null;
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), settings);

        }
    }
}

