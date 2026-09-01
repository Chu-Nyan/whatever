using UnityEngine;

namespace Chunity.Framework
{
    public class VCamStaticData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float POV;

        public VCamStaticData(Vector3 pos, Quaternion rot, float pov)
        {
            Position = pos;
            Rotation = rot;
            POV = pov;
        }
    }
}
