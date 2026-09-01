using Chunity.Core;
using Chunity.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    public class NyanCollider : IQuadTreeEntity
    {
        public readonly int InstanceID;
        private readonly HashSet<int> _insertedNodes;
        private readonly HashSet<int> _contactIDs;

        private int _instigatorID;
        private IShape _shape;
        private Pose2D _pose2D;
        private NyanLayer _layer;
        private NyanLayerMask _mask;
        private bool _isActive;
        private string _comment;

        private event Action<NyanCollider> PositionChanged;
        private event Action<NyanCollider> EnabledChanged;

        public int InstigatorID
        {
            get => _instigatorID;
        }

        public HashSet<int> InsertedNodesID
        {
            get => _insertedNodes;
        }

        public HashSet<int> ContactIDs
        {
            get => _contactIDs;
        }

        public RectBound RectBound
        {
            get => _shape.AABB;
        }

        public NyanLayer Layer
        {
            get => _layer;
        }

        public NyanLayerMask LayerMask
        {
            get => _mask;
        }

        public bool IsActive
        {
            get => _isActive;
        }

        public NyanCollider(IShape shape, bool isActive, string comment)
        {
            _insertedNodes = new();
            _contactIDs = new();

            InstanceID = IDGenerator.Next();
            SetShape(shape);
            SetActive(isActive);
            _comment = comment;
        }

        public void SetShape(IShape shape)
        {
            if (shape == null)
            {
                Debug.LogWarning($"{_comment}, Null shape provided");
                var temp = new CircleShape();
                temp.Setup(new(Vector3.zero, 1));
                shape = temp;
            }

            _shape = shape;
        }

        public void SetLayer(NyanLayer layer, NyanLayerMask mask)
        {
            _layer = layer;
            _mask = mask;
        }

        public void SetInstigatorID(int id)
        {
            _instigatorID = id;
        }

        public void SetActive(bool value)
        {
            if (_isActive == value)
                return;

            _isActive = value;
            EnabledChanged?.Invoke(this);
        }

        public void SetTransform(Pose2D pose)
        {
            _pose2D = pose;
            PositionChanged?.Invoke(this);
        }

        public void SetTransform(Vector2 pos, float eulerY)
        {
            SetTransform(new Pose2D(pos, eulerY));
        }

        public void UpdateAABB()
        {
            _shape.UpdateAABB(_pose2D.Position, _pose2D.EulerY);
        }

        public bool Intersects(NyanCollider collider)
        {
            bool result = false;
            if (RectBound.IsIntersecting(RectBound, collider.RectBound) == true)
                result = _shape.Intersects(collider._shape);

            return result;
        }

        public bool IsContacted(int id)
        {
            return _contactIDs.Contains(id);
        }

        public void AddQuadTreeNodeID(int number)
        {
            _insertedNodes.Add(number);
        }

        public void AddContactColliderID(int id)
        {
            _contactIDs.Add(id);
        }

        public void RemoveContactColliderID(int id)
        {
            _contactIDs.Remove(id);
        }

        public void ClearInsertedNodes()
        {
            _insertedNodes.Clear();
        }

        #region 델리게이트 등록, 해제
        public void RegisterPositionChanged(Action<NyanCollider> action)
        {
            PositionChanged += action;
        }

        public void UnregisterPositionChanged(Action<NyanCollider> action)
        {
            PositionChanged -= action;
        }

        public void RegisterEnabled(Action<NyanCollider> action)
        {
            EnabledChanged += action;
        }
        #endregion

        public override string ToString()
        {
            return $"ID : {InstanceID} {_comment} {_instigatorID}";
        }
    }
}
