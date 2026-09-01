using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public struct CircleRangeData
    {
        public static CircleRangeData Default = new(Vector2.zero, 1);

        public Vector2 Offset;
        public float Radius;

        public CircleRangeData(Vector2 offset, float radius)
        {
            Offset = offset;
            Radius = radius;
        }
    }
}