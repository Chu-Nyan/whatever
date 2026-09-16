using Newtonsoft.Json;

namespace Whatever.Core
{
    public interface IHasKey
    {
        [JsonIgnore]
        public int Key { get; }
    }
}
