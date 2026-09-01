using UnityEngine;

namespace Chunity.Utility
{
    public static class GizmoDrawer
    {
        public static int CircleSegement = 32;
        private static float _angleStep = (Mathf.PI * 2) / CircleSegement;

        public static void DrawRectangle(Vector3 center, float width, float height, Quaternion rotation)
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);

            float hW = width * 0.5f;
            float hH = height * 0.5f;

            Vector3 p1 = new(-hW, 0, -hH);
            Vector3 p2 = new(-hW, 0, hH);
            Vector3 p3 = new(hW, 0, hH);
            Vector3 p4 = new(hW, 0, -hH);

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);

            Gizmos.DrawLine(p1, p3);
            Gizmos.DrawLine(p2, p4);

            Gizmos.matrix = oldMatrix;
        }

        public static void DrawCircle(Vector3 center, float radius)
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.identity, Vector3.one);

            Vector3 diag1 = new Vector3(1, 0, 1).normalized * radius;
            Vector3 diag2 = new Vector3(1, 0, -1).normalized * radius;

            Gizmos.DrawLine(-diag1, diag1);
            Gizmos.DrawLine(-diag2, diag2);

            Vector3 prevPoint = new(radius, 0, 0);

            for (int i = 1; i <= CircleSegement; i++)
            {
                float angle = i * _angleStep;
                Vector3 nextPoint = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

                Gizmos.DrawLine(prevPoint, nextPoint);
                prevPoint = nextPoint;
            }

            Gizmos.matrix = oldMatrix;
        }
    }
}
