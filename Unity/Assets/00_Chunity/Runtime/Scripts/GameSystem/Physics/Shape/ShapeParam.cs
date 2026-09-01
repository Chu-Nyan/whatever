using Chunity.Utility;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    [Serializable]
    public struct ShapeParam
    {
        public ShapeType Type;
        public float OffsetX;
        public float OffsetY;
        public float Param1;
        public float Param2;

        [JsonIgnore]
        public readonly float Radius
        {
            get => Param1;
        }

        [JsonIgnore]
        public readonly float Width
        {
            get => Param1;
        }

        [JsonIgnore]
        public readonly float Height
        {
            get => Param2;
        }

        public ShapeParam(ShapeType type, float offsetX, float offsetY, float param1, float param2)
        {
            if (type == ShapeType.Composite)
                throw new Exception();

            Type = type;
            OffsetX = offsetX;
            OffsetY = offsetY;
            Param1 = param1;
            Param2 = param2;
        }

        public readonly RectRangeData GetRectData()
        {
            if (Type != ShapeType.Rectangle)
                Debug.LogWarning("지정된 타입과 호환되지 않는 데이터를 사용 중");

            return new RectRangeData(new Vector2(OffsetX, OffsetY), 0, Width, Height);
        }

        public readonly CircleRangeData GetCircleData()
        {
            if (Type != ShapeType.Circle)
                Debug.LogWarning("지정된 타입과 호환되지 않는 데이터를 사용 중");

            return new CircleRangeData(new Vector2(OffsetX, OffsetY), Radius);
        }

        public static IShape ConvertShape(ShapeParam[] param)
        {
            if (param.Length == 1)
                return param[0].ConvertShape();
            else
            {
                var composite = new CompositeShape();
                composite.Setup(param);
                return composite;
            }
        }

        public readonly IShape ConvertShape()
        {
            if (Type == ShapeType.Rectangle)
                return ConvertRectShape(this);
            if (Type == ShapeType.Circle)
                return ConvertCircleShape(this);

            throw new Exception("잘못된 타입 입력됨");
        }

        private static RectShape ConvertRectShape(ShapeParam param)
        {
            var rectShape = new RectShape();
            var data = param.GetRectData();
            rectShape.Setup(data);
            return rectShape;
        }

        private static CircleShape ConvertCircleShape(ShapeParam param)
        {
            var circleShape = new CircleShape();
            var data = param.GetCircleData();
            circleShape.Setup(data);
            return circleShape;
        }

        #region Editor
        public readonly void DrawGizmo(Vector3 anchor, Quaternion quaternion)
        {
            Vector3 center = new(anchor.x + OffsetX, anchor.y, anchor.z + OffsetY);

            Gizmos.color = Color.yellow;
            if (Type == ShapeType.Rectangle)
            {
                GizmoDrawer.DrawRectangle(center, Width, Height, quaternion);
            }
            else if (Type == ShapeType.Circle)
            {
                GizmoDrawer.DrawCircle(center, Radius);
            }
        }
        #endregion
    }
}
