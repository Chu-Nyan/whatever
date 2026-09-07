using UnityEngine;

namespace Chunity.Framework
{
    public class PlayerActionMap
    {
        public readonly InputEvent<Vector2> WASD;
        public readonly InputEvent LeftClick;
        public readonly InputEvent RightClick;
        public readonly InputEvent Sprint;
        public readonly InputEvent Dodge;
        public readonly FixedInputEvent<int>[] Skills;
        public readonly InputEvent SkillSlotSwap;
        public readonly InputEvent Ultimate;
        public readonly FixedInputEvent<int>[] Items;

        public PlayerActionMap(InputSystem_Actions.PlayerActions actions)
        {
            WASD = new(actions.WASD, InputEventType.Performed | InputEventType.Canceled);
            LeftClick = new(actions.LeftClick, InputEventType.Performed);
            RightClick = new(actions.RightClick, InputEventType.Performed);
            Sprint = new(actions.Sprint, InputEventType.Performed | InputEventType.Canceled);
            Dodge = new(actions.Dodge, InputEventType.Performed);
            Skills = new FixedInputEvent<int>[]
            {
                new(actions.SkillSlotA, InputEventType.Performed, 0),
                new(actions.SkillSlotB, InputEventType.Performed, 1),
                new(actions.SkillSlotC, InputEventType.Performed, 2),
                new(actions.SkillSlotD, InputEventType.Performed, 3)
            };
            SkillSlotSwap = new(actions.SkillSlotSwap, InputEventType.Performed | InputEventType.Canceled);
            Ultimate = new(actions.Ultimate, InputEventType.Performed);
            Items = new FixedInputEvent<int>[]
            {
                new(actions.ItemSlotA, InputEventType.Performed, 0),
                new(actions.ItemSlotB, InputEventType.Performed, 1),
                new(actions.SkillSlotD, InputEventType.Performed, 4)
            };
        }
    }
}
