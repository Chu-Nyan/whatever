using UnityEngine;

namespace Chunity.Utility
{
    public static class Vector3Extensions
    {
        public static Vector2 ToVector2XZ(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.z);
        }
    }
}
