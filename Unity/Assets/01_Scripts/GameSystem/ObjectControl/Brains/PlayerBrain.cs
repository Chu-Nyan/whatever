using Chunity.Framework;
using UnityEngine;

namespace Whatever.ObjectControl
{
    public class PlayerBrain : IBrainStrategy
    {
        private QuickSlotLoadout _quickSlot;

        private IMovementReceiver _movementReceiver;
        private IOffenseReceiver _offenseReceiver;
        private Vector2 _direction;
        private bool _isMoving;
        private bool _isSkillSwap;

        public BrainType BrainType
        {
            get => BrainType.Player;
        }

        public void Tick()
        {
            MoveDirection();
        }

        public void ControlReceiver(IInputReceiver obj)
        {
            if (obj is IMovementReceiver movement)
                _movementReceiver = movement;
            if (obj is IOffenseReceiver offense)
                _offenseReceiver = offense;
        }

        public void SetupLoadout(QuickSlotLoadout quickSlot)
        {
            _quickSlot = quickSlot;
        }

        public void Enable()
        {
            InputSystem.Instance.Player.WASD.RegisterPerformed(SetDiection);
            InputSystem.Instance.Player.WASD.RegisterCanceled(SetDiection);
            InputSystem.Instance.Player.Dodge.RegisterPerformed(Dodge);
            InputSystem.Instance.Player.Sprint.RegisterPerformed(() => ToggleSprint(true));
            InputSystem.Instance.Player.Sprint.RegisterCanceled(() => ToggleSprint(false));

            InputSystem.Instance.Player.LeftClick.RegisterPerformed(UseBasicSkill);
            InputSystem.Instance.Player.RightClick.RegisterPerformed(UseSupportSkill);
            InputSystem.Instance.Player.Ultimate.RegisterPerformed(UseUltimate);

            for (int i = 0; i < 4; i++)
            {
                InputSystem.Instance.Player.Skills[i].RegisterPerformed(UseSkillSlot);
            }
            InputSystem.Instance.Player.SkillSlotSwap.RegisterPerformed(() => _isSkillSwap = true);
            InputSystem.Instance.Player.SkillSlotSwap.RegisterCanceled(() => _isSkillSwap = false);

            for (int i = 0; i < 2; i++)
            {
                InputSystem.Instance.Player.Items[i].RegisterPerformed(UseItem);
            }
        }

        public void Disable()
        {
            InputSystem.Instance.Player.WASD.UnregisterPerformed(SetDiection);
            InputSystem.Instance.Player.WASD.UnregisterCanceled(SetDiection);
            SetDiection(Vector2.zero);
        }

        private void SetDiection(Vector2 dir)
        {
            _direction = dir;
            _isMoving = _direction != Vector2.zero;
        }

        private void MoveDirection()
        {
            if (_isMoving == false)
                return;

            var direction = new Vector3(_direction.x * 0.5f, 0, _direction.y * 0.5f) + _movementReceiver.transform.position;
            _movementReceiver.SetDestination(direction);
        }

        private void UseBasicSkill()
        {
            if (TryConvertScreenToGroundPosition(out var result) == false)
                return;

            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SlotType.Basic], result);
        }

        private void UseSupportSkill()
        {
            if (TryConvertScreenToGroundPosition(out var result) == false)
                return;

            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SlotType.Support], result);
        }

        private void UseUltimate()
        {
            if (TryConvertScreenToGroundPosition(out var result) == false)
                return;

            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SlotType.Ultimate], result);
        }

        private void UseSkillSlot(int skillSlot)
        {
            if (TryConvertScreenToGroundPosition(out var result) == false)
                return;

            if (_isSkillSwap == true)
                skillSlot += 4;
            var skillID = _quickSlot[QuickSlotLoadout.SlotType.Slot1 + skillSlot];
            _offenseReceiver.UseSkill(skillID, result);
        }

        private void UseItem(int itemSlot)
        {
            var itemID = _quickSlot[QuickSlotLoadout.SlotType.Q + itemSlot];
            _offenseReceiver.UseItem(itemID);
        }

        private void Dodge()
        {
            // TODO : 스킬 사용으로 변경
            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SlotType.Dodge], InputSystem.Instance.Always.MousePosition);
            _movementReceiver.OrderDodge(_direction);
        }

        private void ToggleSprint(bool value)
        {
            _movementReceiver.ToggleSprint(value);
        }

        private bool TryConvertScreenToGroundPosition(out Vector3 result)
        {
            var mousePoint = InputSystem.Instance.Always.MousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePoint);
            int flag = LayerMask.GetMask("Ground");

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, flag) == true)
            {
                result = hit.point;
                return true;
            }

            result = default;
            return false;
        }

    }
}
