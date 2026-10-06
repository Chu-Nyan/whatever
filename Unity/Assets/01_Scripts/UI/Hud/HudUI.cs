using Chunity.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Whatever.UI
{
    public class HudUI : UIController
    {
        public StatsViewer Stats;
        public QuickSlotUIContent QuickSlot;
        [SerializeField]
        private Button EquipmentUIBtn;
        [SerializeField]
        private Button SkillUIBtn;
        [SerializeField]
        private Button SettingUIBtn;

        public void Awake()
        {
            SettingUIBtn.onClick.AddListener(() => Debug.Log("Setting UI"));
        }

        public void SetupBtn(Action equipment, Action skill, Action setting)
        {
            EquipmentUIBtn.onClick.AddListener(() => equipment.Invoke());
            SkillUIBtn.onClick.AddListener(() => skill.Invoke());
            SettingUIBtn.onClick.AddListener(() => setting.Invoke());
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
