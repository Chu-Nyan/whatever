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
            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SkillSlot.Basic], InputSystem.Instance.Always.MousePosition);
        }

        private void UseSupportSkill()
        {
            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SkillSlot.Support], InputSystem.Instance.Always.MousePosition);
        }

        private void UseUltimate()
        {
            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SkillSlot.Ultimate], InputSystem.Instance.Always.MousePosition);
        }

        private void UseSkillSlot(int skillSlot)
        {
            if (_isSkillSwap == true)
                skillSlot += 4;

            var skillID = _quickSlot[QuickSlotLoadout.SkillSlot.Slot1 + skillSlot];
            _offenseReceiver.UseSkill(skillID, InputSystem.Instance.Always.MousePosition);
        }

        private void UseItem(int itemSlot)
        {
            var itemID = _quickSlot[(QuickSlotLoadout.ItemSlot)itemSlot];
            _offenseReceiver.UseItem(itemID);
        }

        private void Dodge()
        {
            // TODO : 스킬 사용으로 변경
            _offenseReceiver.UseSkill(_quickSlot[QuickSlotLoadout.SkillSlot.Dodge], InputSystem.Instance.Always.MousePosition);
            _movementReceiver.OrderDodge(_direction);
        }

        private void ToggleSprint(bool value)
        {
            _movementReceiver.ToggleSprint(value);
        }

    }
}
