using Chunity.GameSystem.Physics;
using UnityEngine;

namespace Whatever.Entities.Skill
{
    public class SequenceData
    {
        public int ID;
        public int Order;
        public float ActiveTime;
        public float Speed;
        public ShapeParam[] Hitboxes;
        public IEffectData[] EffectDatas;

        public Mesh HitBoxMesh;
    }
}
