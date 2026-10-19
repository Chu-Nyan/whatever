using Chunity.Core;
using Chunity.GameSystem.Physics;
using Chunity.Utility;
using System;
using UnityEngine;
using UnityEngine.AI;
using Whatever.Core;
using Whatever.Entities.Items;
using Whatever.GameSystems;
using Whatever.GameSystems.Combat;
using Whatever.ObjectControl;

namespace Whatever.Entities.Characters
{
    public class Character : MonoBehaviour, ISceneObject, IMovementReceiver, IOffenseReceiver
    {
        public static readonly NyanLayerMask BodyMask = new(NyanLayer.Projectile);

        private int _instanceID;

        [SerializeField]
        private NavMeshAgent _nav;
        private CharacterState _state;
        private CharacterStateContext _stateContext;
        private CombatSystem _combatSystem;
        private CooldownSystem _cooldownSys;

        private CharacterData _data;
        private SkillBook _skillBook;
        private QuickSlotInventory _inventory;

        public event Action<Character> Deactivated;

        public int InstanceID => _instanceID;
        public int ReceiverID => _instanceID;
        public CharacterData Data => _data;
        public CombatSystem CombatSystem => _combatSystem;
        public CooldownSystem CooldownSys => _cooldownSys;
        public SkillBook SkillBook => _skillBook;
        public QuickSlotInventory Inventory => _inventory;

        private void Awake()
        {
            _instanceID = IDGenerator.Next();
            if (NavMesh.SamplePosition(transform.position, out var hit, 2f, NavMesh.AllAreas))
                transform.position = hit.position;

            _nav.enabled = true;
            _stateContext = new();
            _state = new(_stateContext);
            _cooldownSys = new();
            _combatSystem = new(_instanceID, BodyMask);
            _data = new();
            _skillBook = new();

            _data.StatIns.HpChanged += (a) => { if (a <= 0) SetActive(false); };
        }

        private void Update()
        {
            _cooldownSys.Tick();
            _combatSystem.Tick(_data.StatIns);
            CheckExternalConditions();
            _state.Update(_stateContext);
            ExcuteMovementOrder();
            ExcuteActionOrder();
        }

        private void CheckExternalConditions()
        {
            if (_stateContext.ActionOrder.CanMove == false)
            {
                _stateContext.MovementOrder.RequestOrder(MovementOrder.Order.Idle);
            }
        }

        private void ExcuteMovementOrder()
        {
            if (_state.MovementState == MovementOrder.Order.Idle)
            {
                if (_stateContext.MovementOrder.IsRequestedOrder == true)
                {
                    _stateContext.MovementOrder.ExcuteRequestedOrder();
                    _stateContext.MovementOrder.EndOrder();
                }
            }
            if (_state.MovementState == MovementOrder.Order.Running || _state.MovementState == MovementOrder.Order.Sprinting)
            {
                Move(_stateContext.MovementOrder);
            }
        }

        private void ExcuteActionOrder()
        {
            if (_state.ActionState == ActionOrder.Order.Idle)
                return;

            if (_state.ActionState == ActionOrder.Order.Dodge)
            {
                Dodge(_stateContext.ActionOrder);
            }
        }

        public void Setup(CharacterDefinitionData def, QuickSlotInventory inven)
        {
            _data.Setup(def);
            _inventory = inven;
            // TODO : 캐릭터 정보로 바디 충돌체 범위 지정
            _combatSystem.SetBodyShape(new CircleShape());
        }

        private void Move(MovementOrder movementOrder)
        {
            if (movementOrder.IsRequestedOrder == true)
            {
                _nav.SetDestination(movementOrder.Destination);
                movementOrder.ExcuteRequestedOrder();
            }

            if ((transform.position.ToVector2XZ() - movementOrder.Destination.ToVector2XZ()).sqrMagnitude <= Const.Epsilon)
            {
                movementOrder.EndOrder();
            }
            UpdateCollider();
        }

        private void Dodge(ActionOrder actionOrder)
        {
            if (actionOrder.IsRequestedOrder == true)
            {
                StopMovement();
                actionOrder.ExcuteRequestedOrder();
            }

            actionOrder.OrderTime += Time.deltaTime;
            var v2 = 10 * Time.deltaTime * actionOrder.DodgeDirection;
            var v3 = new Vector3(v2.x, 0, v2.y);
            SetPositionWithNavMash(transform.position + v3);

            if (actionOrder.OrderTime >= 0.5f)
                actionOrder.EndOrder();
        }

        public void SetDestination(Vector3 destination)
        {
            _stateContext.MovementOrder.RequestOrder(MovementOrder.Order.Running);
            _stateContext.MovementOrder.Destination = destination;
        }

        public void SetPositionWithNavMash(Vector3 position)
        {
            if (NavMesh.SamplePosition(position, out var hit, 2f, NavMesh.AllAreas) == true)
            {
                _nav.Warp(hit.position);
            }

            UpdateCollider();
        }

        public void ToggleSprint(bool value)
        {
            _stateContext.MovementOrder.IsSprinting = value;
        }

        public void OrderDodge(Vector2 dir)
        {
            if (dir == Vector2.zero)
                dir = Vector2.up;
            _stateContext.ActionOrder.RequestOrder(ActionOrder.Order.Dodge);
            _stateContext.ActionOrder.DodgeDirection = dir;
            _stateContext.ActionOrder.CanMove = false;
        }

        public void StopMovement()
        {
            _nav.ResetPath();
        }

        public void UseSkill(int skillID, Vector3 targetPoint)
        {
            if (_skillBook.CanUse(skillID) == false)
            {
                Debug.Log("Use failed" + " Skill ID : " + skillID);
                return;
            }
            if (_cooldownSys.ContainSkills(skillID) == true)
            {
                Debug.Log("Cooldown remained" + " Skill ID : " + skillID);
                return;
            }

            Vector3 direction = (targetPoint - transform.position).normalized;
            bool result = _combatSystem.FireSkill(skillID, transform.position, direction);
            _cooldownSys.Set(CooldownSystem.Category.Skill, skillID, _skillBook[skillID].Data.Cooldown);
            Debug.Log(result + " Skill ID : " + skillID);
        }

        public void UseItem(int itemID)
        {
            if (_inventory.QuickSlot.TryGetItem(itemID, out var item) == false)
            {
                Debug.Log("No item" + " Item ID : " + itemID);
                return;
            }
            if (item.CanUse() == false)
                return;
            if (_cooldownSys.ContainItems(itemID) == true)
            {
                Debug.Log("Cooldown remained" + " Item ID : " + itemID);
                return;
            }

            item.Use();
            // TODO : 아이템 효과 적용
            Debug.Log($"{itemID} Used");
            _cooldownSys.Set(CooldownSystem.Category.Item, itemID, ((Consumable)item).ConsumableDef.Cooldown);
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

        private void UpdateCollider()
        {
            _combatSystem.Collider.SetTransform(transform.position.ToVector2XZ(), transform.eulerAngles.y);
        }
    }
}
