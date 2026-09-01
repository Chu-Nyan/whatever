using Chunity.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public class CompositeShape : IShape
    {
        private readonly List<IShape> _shapeList;
        private RectBound _aabb;

        public ShapeType ShapeType
        {
            get => ShapeType.Composite;
        }

        public RectBound AABB
        {
            get => _aabb;
        }

        public int Count
        {
            get => _shapeList.Count;
        }

        public IShape this[int index]
        {
            get => _shapeList[index];
        }

        public CompositeShape()
        {
            _shapeList = new();
        }

        public void Setup(ShapeParam[] list)
        {
            _shapeList.Clear();
            foreach (var item in list)
            {
                _shapeList.Add(item.ConvertShape());
            }
        }

        public void UpdateAABB(Vector2 position, float degree)
        {
            if (_shapeList.Count == 0)
                return;

            float minX = float.MaxValue;
            float maxX = float.MinValue;
            float minY = float.MaxValue;
            float maxY = float.MinValue;

            for (int i = 0; i < _shapeList.Count; i++)
            {
                _shapeList[i].UpdateAABB(position, degree);
                var elementBound = _shapeList[i].AABB;

                if (elementBound.MinX < minX) minX = elementBound.MinX;
                if (elementBound.MaxX > maxX) maxX = elementBound.MaxX;
                if (elementBound.MinY < minY) minY = elementBound.MinY;
                if (elementBound.MaxY > maxY) maxY = elementBound.MaxY;
            }

            _aabb = new RectBound(minX, maxX, minY, maxY);
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
            return CollisionHelper.IsColliding(this, shape);
        }
    }
}
