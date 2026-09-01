using Chunity.Data;
using System;

[Serializable]
public class EngineSetting
{
    public string Language;
    public string UIFilePathFormat;
    public bool UseCollision;
    public RectBound CollisionBound;
    public int Capacity;

    public EngineSetting(string language, string uiFilePathFormat, bool useCollision, RectBound collisionBound, int capacity)
    {
        Language = language;
        UIFilePathFormat = uiFilePathFormat;
        UseCollision = useCollision;
        CollisionBound = collisionBound;
        Capacity = capacity;
    }
}
