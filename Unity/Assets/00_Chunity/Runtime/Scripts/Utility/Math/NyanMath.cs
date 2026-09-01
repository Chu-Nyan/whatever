using UnityEngine;

namespace Chunity.Utility
{
    public static class NyanMath
    {
        public static Vector2 CalculateOffsetPosition(Vector2 pos, Vector2 offset, float degree)
        {
            if (offset != Vector2.zero)
            {
                float rad = degree * Mathf.Deg2Rad;
                float sin = Mathf.Sin(rad);
                float cos = Mathf.Cos(rad);

                offset = new Vector2(
                    offset.x * cos - offset.y * sin,
                    offset.x * sin + offset.y * cos
                );

                pos += offset;
            }

            return pos;
        }
    }
}
