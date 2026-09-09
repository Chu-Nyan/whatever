using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Whatever.Entities.Skill
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EffectType
    {
        Instant,
        Dot,
        Area,
        Timer
    }
}
