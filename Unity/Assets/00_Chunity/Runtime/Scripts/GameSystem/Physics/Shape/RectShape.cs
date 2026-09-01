using Chunity.Data;
using Chunity.Utility;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public class RectShape : IShape
    {
        private const float _refreshEpsilon = 0.001f;

        private RectRangeData _data;
        private RectBound _aabb;

        private float _objDegree = float.MinValue;
        private float _degree = float.MinValue;
        private float _radian = float.MinValue;

        private readonly Vector2[] _axis = new Vector2[2];      // 0 : Right, 1 : Up
        private readonly Vector2[] _radius = new Vector2[2];    // 0 : Right, 1 : Up

        public ShapeType ShapeType
        {
            get => ShapeType.Rectangle;
        }

        public RectRangeData Data
        {
            get => _data;
        }

        public Vector2[] Axis
        {
            get => _axis;
        }

        public Vector2[] Radius
        {
            get => _radius;
        }

        public RectBound AABB
        {
            get => _aabb;
        }

        public RectShape() : this(RectRangeData.Default) { }

        public RectShape(RectRangeData data)
        {
            Setup(data);
        }

        public void Setup(RectRangeData data)
        {
            _data = data;
            _aabb = new(0, data.Width, 0, data.Height);
            UpdateAABB(Vector2.zero, 0);
        }

        public void UpdateAABB(Vector2 position, float degree)
        {
            RefreshAxis(degree);
            _aabb.RefreshAABB(NyanMath.CalculateOffsetPosition(position, _data.Offset, degree), _radius);
        }

        private void RefreshAxis(float degree)
        {
            if (Mathf.Abs(_objDegree - degree) < _refreshEpsilon)
                return;

            _objDegree = degree;
            _degree = degree + _data.Rotation;
            _radian = _degree * Mathf.Deg2Rad;

            float cos = Mathf.Cos(_radian);
            float sin = Mathf.Sin(_radian);
            _axis[0] = new Vector2(cos, sin);
            _axis[1] = new Vector2(-sin, cos);
            _radius[0] = _data.Width * 0.5f * _axis[0];
            _radius[1] = _data.Height * 0.5f * _axis[1];
        }

        public bool Intersects(IShape target)
        {
            if (RectBound.IsIntersecting(AABB, target.AABB) == false)
                return false;

            return target.Intersects(this);
        }

        public bool Intersects(RectShape shape)
        {
            return CollisionHelper.IsColliding(this, shape);
        }

        public bool Intersects(CircleShape shape)
        {
            return CollisionHelper.IsColliding(this, shape);
        }

        public bool Intersects(CompositeShape shape)
        {
            return CollisionHelper.IsColliding(shape, this);
        }
    }
}
