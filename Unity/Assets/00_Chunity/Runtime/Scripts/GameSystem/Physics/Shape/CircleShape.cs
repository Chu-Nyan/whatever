using Chunity.Data;
using Chunity.Utility;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public class CircleShape : IShape
    {
        private CircleRangeData _data;
        private RectBound _aabb;

        public ShapeType ShapeType
        {
            get => ShapeType.Circle;
        }

        public CircleRangeData Data
        {
            get => _data;
        }

        public RectBound AABB
        {
            get => _aabb;
        }

        public CircleShape() : this(CircleRangeData.Default) { }

        public CircleShape(CircleRangeData data)
        {
            Setup(data);
        }

        public void Setup(CircleRangeData data)
        {
            _data = data;
            _aabb = new(-_data.Radius, _data.Radius, -_data.Radius, _data.Radius);
            UpdateAABB(Vector2.zero, 0);
        }

        public void UpdateAABB(Vector2 position, float degree)
        {
            _aabb.RefreshAABB(NyanMath.CalculateOffsetPosition(position, _data.Offset, degree));
        }

        public bool Intersects(IShape target)
        {
            if (RectBound.IsIntersecting(AABB, target.AABB) == false)
                return false;

            return target.Intersects(this);
        }

        public bool Intersects(RectShape shape)
        {
            return CollisionHelper.IsColliding(shape, this);
        }

        public bool Intersects(CircleShape shape)
        {
            return CollisionHelper.IsColliding(shape, this);
        }

        public bool Intersects(CompositeShape shape)
        {
            return CollisionHelper.IsColliding(shape, this);
        }
    }
}
