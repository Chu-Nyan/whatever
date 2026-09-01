using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Chunity.Data
{
    [Serializable]
    public struct RectBound
    {
        public float MinX, MinY, MaxX, MaxY;
        [NonSerialized]
        public float HalfX, HalfY;

        public readonly Vector2 Center
        {
            get => new(MinX + HalfX, MinY + HalfY);
        }

        public RectBound(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;

            HalfX = (maxX - minX) * 0.5f;
            HalfY = (maxY - minY) * 0.5f;
        }

        public void OnAfterDeserialize()
        {
            HalfX = (MaxX - MinX) * 0.5f;
            HalfY = (MaxY - MinY) * 0.5f;
        }

        private void SetBound(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;

            HalfX = (maxX - minX) * 0.5f;
            HalfY = (maxY - minY) * 0.5f;
        }

        public void RefreshAABB(Vector2 pos)
        {
            SetBound(pos.x - HalfX, pos.x + HalfX, pos.y - HalfY, pos.y + HalfY);
        }

        // 회전 상태를 반영한 AABB 반환
        public void RefreshAABB(Vector2 pos, Vector2[] radius)
        {
            Vector2 r0 = radius[0];
            Vector2 r1 = radius[1];

            SetBound(pos.x - Mathf.Abs(r0.x) - Mathf.Abs(r1.x),
                     pos.x + Mathf.Abs(r0.x) + Mathf.Abs(r1.x),
                     pos.y - Mathf.Abs(r0.y) - Mathf.Abs(r1.y),
                     pos.y + Mathf.Abs(r0.y) + Mathf.Abs(r1.y));
        }

        // 내부에 target이 완벽하게 포함되는가?
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsFullyInside(RectBound target)
        {
            return MinX <= target.MinX && MaxX >= target.MaxX
                && MinY <= target.MinY && MaxY >= target.MaxY;
        }

        // 두 RectBound는 겹치는가?
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIntersecting(RectBound a, RectBound b)
        {
            if (a.MaxX < b.MinX) return false;
            if (a.MinX > b.MaxX) return false;
            if (a.MaxY < b.MinY) return false;
            if (a.MinY > b.MaxY) return false;

            return true;
        }

        public override readonly string ToString()
        {
            return $"X : {MinX} / {MaxX}, Y :{MinY} / {MaxY}";
        }
    }
}