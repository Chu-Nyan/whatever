using Chunity.Utility.NetonsoftJson;
using Newtonsoft.Json;

namespace Chunity.Utility
{
    public static class JsonSettingsExtensions
    {
        public static void AddUnityConverters(this JsonSerializerSettings settings)
        {
            settings.Converters.Add(new Vector2Converter());
            settings.Converters.Add(new Vector3Converter());
            settings.Converters.Add(new QuaternionConverter());
        }

        public static JsonSerializerSettings WithUnity(this JsonSerializerSettings settings)
        {
            settings.Converters.Add(new Vector3Converter());
            settings.Converters.Add(new Vector2Converter());
            settings.Converters.Add(new QuaternionConverter());
            return settings;
        }
    }
}

