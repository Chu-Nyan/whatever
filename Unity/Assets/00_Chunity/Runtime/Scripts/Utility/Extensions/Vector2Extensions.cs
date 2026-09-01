using UnityEngine;

namespace Chunity.Utility
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3XZ(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }
    }
}
