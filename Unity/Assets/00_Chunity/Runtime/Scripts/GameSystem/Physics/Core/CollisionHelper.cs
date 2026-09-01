using System;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    /// <summary>
    /// 도형 충돌 여부를 계산하는 유틸리티
    /// </summary>
    public static class CollisionHelper
    {
        public static bool IsColliding(RectShape a, RectShape b)
        {
            Vector2 distance = a.AABB.Center - b.AABB.Center;
            bool result = IsOverlapOnAxis(a.Axis[0]) && IsOverlapOnAxis(b.Axis[0])
                       && IsOverlapOnAxis(a.Axis[1]) && IsOverlapOnAxis(b.Axis[1]);

            return result;

            bool IsOverlapOnAxis(Vector2 axis)
            {
                float dotDistance = Mathf.Abs(Vector2.Dot(axis, distance));
                float dotThis = Mathf.Abs(Vector2.Dot(axis, a.Radius[0])) + Mathf.Abs(Vector2.Dot(axis, a.Radius[1]));
                float dotTarget = Mathf.Abs(Vector2.Dot(axis, b.Radius[0])) + Mathf.Abs(Vector2.Dot(axis, b.Radius[1]));

                return dotDistance <= dotThis + dotTarget;
            }
        }

        public static bool IsColliding(RectShape a, CircleShape b)
        {
            Vector2 distance = a.AABB.Center - b.AABB.Center;
            float localX = Vector2.Dot(distance, a.Axis[0]);
            float localY = Vector2.Dot(distance, a.Axis[1]);

            float closestX = Mathf.Clamp(localX, -a.AABB.HalfX, a.AABB.HalfX);
            float closestY = Mathf.Clamp(localY, -a.AABB.HalfY, a.AABB.HalfY);

            float x = localX - closestX;
            float y = localY - closestY;
            float rad = b.Data.Radius;

            return x * x + y * y <= rad * rad;
        }

        public static bool IsColliding(CircleShape a, CircleShape b)
        {
            Vector2 aCenter = a.AABB.Center;
            Vector2 bCenter = b.AABB.Center;
            float aNum = aCenter.x - bCenter.x;
            float bNum = aCenter.y - bCenter.y;
            float distance = (float)Math.Sqrt(aNum * aNum + bNum * bNum);

            return distance <= (a.Data.Radius + b.Data.Radius);
        }

        public static bool IsColliding(CompositeShape a, CircleShape b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Intersects(b) == true)
                    return true;
            }

            return false;
        }

        public static bool IsColliding(CompositeShape a, RectShape b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i].Intersects(b) == true)
                    return true;
            }

            return false;
        }

        public static bool IsColliding(CompositeShape a, CompositeShape b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (a[i].Intersects(b[j]) == true)
                        return true;
                }
            }

            return false;
        }
    }
}
