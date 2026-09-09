using Chunity.GameSystem.Physics;
using Chunity.Utility;
using System;
using UnityEngine;

namespace Whatever.Entities.Skill
{
    public class SkillObject : MonoBehaviour, INyanCollisionProvider
    {
        private const NyanLayer _layer = NyanLayer.Projectile;
        private const int _hitUnityLayer = 0;

        public int ID;
        [SerializeField]
        private MeshFilter _meshFilter;
        private NyanCollider _nyanCollider;
        private SkillObjectContext _context;
        private int _hostilityMask;
        private int _logicStep;
        private float _timer;

        public event Action<SkillObject> Destroyed;

        public NyanCollider Collider
        {
            get => _nyanCollider;
        }

        public SkillObjectContext Context
        {
            get => _context;
        }

        public void Awake()
        {
            _nyanCollider = NyanColliderFactory.Instance.Create(this, new RectShape(), false, $"Projectile {1}");
            _nyanCollider.SetInstigatorID(0);
        }

        private void Update()
        {
            var sequenceData = Context.SkillData.SequenceData;
            _timer += Time.deltaTime;

            if (_timer > sequenceData[_logicStep].ActiveTime)
            {
                _logicStep++;
                if (_logicStep < sequenceData.Length)
                    ChangeShape(_logicStep);
                else
                {
                    SetActive(false);
                    return;
                }
            }

            transform.position += sequenceData[_logicStep].Speed * Time.deltaTime * transform.forward;
            _nyanCollider.SetTransform(transform.position.ToVector2XZ(), transform.eulerAngles.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((1 << other.gameObject.layer & _hitUnityLayer) == 0)
                return;

            Debug.Log("환경 오브젝트 충돌");
            SetActive(false);
        }

        public void Setup(int instigator, SkillObjectContext context, int hostilityMask)
        {
            _context = context;
            _hostilityMask = hostilityMask;
            _nyanCollider.SetInstigatorID(instigator);
            _nyanCollider.SetLayer(_layer, _context.Mask);
            _logicStep = 0;
            ChangeShape(_logicStep);
            _nyanCollider.SetTransform(transform.position.ToVector2XZ(), transform.eulerAngles.y);
            _nyanCollider.SetActive(true);
        }

        public void SetActive(bool value)
        {
            if (value == false)
            {
                Destroyed?.Invoke(this);
                Destroyed = null;
            }

            _nyanCollider.SetActive(value);
            gameObject.SetActive(value);
        }

        private void ChangeShape(int step)
        {
            var sequenceData = Context.SkillData.SequenceData[step];
            var shape = ShapeParam.ConvertShape(sequenceData.Hitboxes);
            _meshFilter.mesh = sequenceData.HitBoxMesh;
            _nyanCollider.SetShape(shape);
            _timer = 0;
        }

        public void SetTarget(Vector3 start, Vector3 dir)
        {
            transform.position = start;
            _timer = 0f;
            transform.rotation = Quaternion.LookRotation(dir);
            _nyanCollider.SetTransform(transform.position.ToVector2XZ(), transform.eulerAngles.y);
        }

        public void OnNyanCollisionEnter(INyanCollisionProvider provider)
        {
            // TODO 적중시 처리
        }

        public void OnNyanCollisionExit(INyanCollisionProvider collider)
        {
        }
    }
}
