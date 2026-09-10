using Chunity.Core;
using Newtonsoft.Json;

namespace Chunity.Utility
{
    public static class JsonHelper
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings().WithUnity();

        public static T[] DeserializeObject<T>(string path)
        {
            return JsonConvert.DeserializeObject<T[]>(AssetManager.LoadJson(path), JsonSerializerSettings);
        }
    }
}
