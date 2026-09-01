using Chunity.Data;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public interface IShape
    {
        public ShapeType ShapeType { get; }
        public RectBound AABB { get; }

        public abstract void UpdateAABB(Vector2 position, float degree);
        public abstract bool Intersects(IShape target);
        public abstract bool Intersects(RectShape shape);
        public abstract bool Intersects(CircleShape shape);
        public abstract bool Intersects(CompositeShape shape);
    }
}
