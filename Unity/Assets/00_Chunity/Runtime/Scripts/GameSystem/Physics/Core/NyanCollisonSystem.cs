using Chunity.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chunity.GameSystem.Physics
{
    /// <summary>
    /// 물리 객체를 관리, 충돌 시뮬레이션 실행
    /// </summary>
    /// - 개발 방향
    /// 시스템에서 등록된 객체는 해제 불가능
    public class NyanCollisonSystem : MonoBehaviour
    {
        private QuadTree<NyanCollider> _root;

        private HashSet<int> _dirtyObjects;
        private HashSet<int> _frameChecked;
        private HashSet<int> _candidateChecked;
        private Queue<CollisionInfo> _collisionInfoQueue;
        private Dictionary<int, NyanCollider> _collidersByID;
        private Dictionary<int, INyanCollisionProvider> _providerByID;

        /// <summary>
        /// 배열의 초기 크기 지정
        /// </summary>
        public void InitArray(int capacity)
        {
            // todo: 생성자로 빼기
            _dirtyObjects = new(capacity);
            _candidateChecked = new(capacity);
            _collisionInfoQueue = new(capacity);
            _frameChecked = new(capacity);
            _collidersByID = new(capacity);
            _providerByID = new(capacity);
        }

        /// <summary>
        /// 충돌 처리 구역 설정
        /// </summary>
        public void InitBound(RectBound bound)
        {
            _root = new(bound);
        }

        private void LateUpdate()
        {
            // 충돌 실행
            _frameChecked.Clear();
            foreach (var dirtyObj in _dirtyObjects)
            {
                _collidersByID[dirtyObj].UpdateAABB();
                Insert(_collidersByID[dirtyObj]);
            }

            foreach (var dirtyObj in _dirtyObjects)
            {
                CheckCollision(_collidersByID[dirtyObj]);
            }

            _dirtyObjects.Clear();

            foreach (var info in _collisionInfoQueue)
            {
                var primaryColider = _collidersByID[info.PrimaryID];
                var primaryProvider = _providerByID[info.PrimaryID];
                var targetColider = _collidersByID[info.TargetID];
                var targetProvider = _providerByID[info.TargetID];

                if (info.State == CollisionState.Enter)
                {
                    primaryColider.AddContactColliderID(info.TargetID);
                    targetColider.AddContactColliderID(info.PrimaryID);
                    primaryProvider.OnNyanCollisionEnter(targetProvider);
                    targetProvider.OnNyanCollisionEnter(primaryProvider);
                }
                else // EXIT
                {
                    primaryProvider.OnNyanCollisionExit(targetProvider);
                    targetProvider.OnNyanCollisionExit(primaryProvider);
                    primaryColider.RemoveContactColliderID(info.TargetID);
                    targetColider.RemoveContactColliderID(info.PrimaryID);
                }
            }

            _collisionInfoQueue.Clear();
        }

        /// <summary>
        /// 물리 시스템에 객체 등록, 해제 불가
        /// </summary>
        public void RegisterEntity(NyanCollider collider, INyanCollisionProvider provider)
        {
            if (_collidersByID.TryAdd(collider.InstanceID, collider) == false)
                throw new Exception("콜라이더 중복 등록");

            _providerByID.Add(collider.InstanceID, provider);
            collider.RegisterEnabled(OnShapeActivationChanged);
            OnShapeActivationChanged(collider);
        }

        private void OnShapeActivationChanged(NyanCollider collider)
        {
            if (collider.IsActive == true)
            {
                collider.RegisterPositionChanged(MarkAsDirty);
                MarkAsDirty(collider);
            }
            else // false
            {
                collider.UnregisterPositionChanged(MarkAsDirty);
                _dirtyObjects.Remove(collider.InstanceID);
                Remove(collider);
            }
        }

        private void MarkAsDirty(NyanCollider obj)
        {
            _dirtyObjects.Add(obj.InstanceID);
        }

        private void Insert(NyanCollider obj)
        {
            var nodes = obj.InsertedNodesID;
            if (nodes.Count == 1)
            {
                foreach (var item in nodes)
                {
                    if (_root.GetNode(item).IsFullyInside(obj.RectBound) == true)
                    {
                        return;
                    }
                }
            }

            _root.Insert(obj);
        }

        private void Remove(NyanCollider obj)
        {
            _root.Remove(obj);
        }

        private void CheckCollision(NyanCollider primary)
        {
            if (_frameChecked.Add(primary.InstanceID) == false)
                return;

            _candidateChecked.Clear();

            foreach (var nodeIndex in primary.InsertedNodesID)
            {
                var node = _root.GetNode(nodeIndex);

                foreach (var candidate in node.Entities)
                {
                    CheckCollisionState(primary, candidate);
                }
            }

            foreach (var num in primary.ContactIDs)
            {
                CheckCollisionState(primary, _collidersByID[num]);
            }
        }

        /// <summary>
        /// 두 오브젝트의 충돌 여부 판단 후 충돌 큐에 추가
        /// </summary>
        /// <param name="primary">주체</param>
        /// <param name="candidate">대상</param>
        private void CheckCollisionState(NyanCollider primary, NyanCollider candidate)
        {
            if (primary.InstigatorID == candidate.InstigatorID)
                return;
            if (_frameChecked.Contains(candidate.InstanceID) == true)
                return;
            if (_candidateChecked.Add(candidate.InstanceID) == false)
                return;
            if (primary.LayerMask.ContainsLayer(candidate.Layer) == false
             || candidate.LayerMask.ContainsLayer(primary.Layer) == false)
                return;

            bool isCollision = primary.Intersects(candidate);
            bool isContacted = primary.IsContacted(candidate.InstanceID);

            if (isCollision == true && isContacted == false)
                _collisionInfoQueue.Enqueue(new CollisionInfo(primary.InstanceID, candidate.InstanceID, CollisionState.Enter));
            else if (isCollision == false && isContacted == true)
                _collisionInfoQueue.Enqueue(new CollisionInfo(primary.InstanceID, candidate.InstanceID, CollisionState.Exit));
        }
    }
}
