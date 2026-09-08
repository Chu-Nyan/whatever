using Chunity.Core;
using Chunity.Utility;
using System;
using UnityEngine;
using UnityEngine.AI;
using Whatever.Core;
using Whatever.ObjectControl;

namespace Whatever.Entities.Characters
{
    public class Character : MonoBehaviour, ISceneObject, IMovementReceiver
    {
        private int _instanceID;

        [SerializeField]
        private NavMeshAgent _nav;
        private CharacterState _state;
        private CharacterStateContext _stateContext;

        private event Action<Character> Deactivated;

        public int InstanceID
        {
            get => _instanceID;
        }

        public int ReceiverID
        {
            get => _instanceID;
        }

        private void Awake()
        {
            _instanceID = IDGenerator.Next();
            if (NavMesh.SamplePosition(transform.position, out var hit, 2f, NavMesh.AllAreas))
                transform.position = hit.position;

            _nav.enabled = true;
            _stateContext = new();
            _state = new(_stateContext);
        }

        private void Update()
        {
            _state.Update(_stateContext);
            ExcuteMovementOrder();
            Debug.Log(_state.MovementState);
        }

        private void ExcuteMovementOrder()
        {
            if (_state.MovementState == MovementState.Idle)
                return;

            if (_stateContext.ExcuteMovement == false)
            {
                _nav.SetDestination(_stateContext.MovementOrder.Destination);
                _stateContext.ExcuteMovement = true;
            }
            else
            {
                if ((transform.position.ToVector2XZ() - _stateContext.MovementOrder.Destination.ToVector2XZ()).sqrMagnitude <= Const.Epsilon)
                {
                    _stateContext.ExcuteMovement = false;
                    _stateContext.MovementOrder.CanMove = false;
                }
            }

        }

        public void SetDestination(Vector3 destination)
        {
            _stateContext.MovementOrder.Destination = destination;
            _stateContext.MovementOrder.CanMove = true;
            _stateContext.ExcuteMovement = false;

        }

        public void SetPositionWithNavMash(Vector3 position)
        {
            if (NavMesh.SamplePosition(position, out var hit, 2f, NavMesh.AllAreas) == true)
            {
                _nav.Warp(hit.position);
            }
        }

        public void ToggleSprint(bool value)
        {
            _stateContext.MovementOrder.IsSprinting = value;
        }

        public void Dodge()
        {
            Debug.Log("Dodge");
        }

        public void StopMovement()
        {
            _nav.ResetPath();
        }

        public void SetActive(bool value)
        {
            if (gameObject.activeSelf == value)
                return;

            if (value == false)
            {
                Deactivated?.Invoke(this);
                Deactivated = null;
            }

            _nav.enabled = false;
            gameObject.SetActive(value);
        }

        public void RegisterDeactivated(Action<Character> callback)
        {
            Deactivated += callback;
        }
    }
}
