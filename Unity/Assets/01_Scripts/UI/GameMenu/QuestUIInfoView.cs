using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Whatever.UI
{
    public class QuestUIInfoView : MonoBehaviour
    {
        [SerializeField]
        private Image _backGround;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _tag;
        [SerializeField]
        private TMP_Text _desc;
        [SerializeField]
        private TMP_Text _requirements;

        public void Refresh(Sprite backGround, Sprite icon, string name, string tag, string desc, string requirements)
        {
            _backGround.sprite = backGround;
            _icon.sprite = icon;
            _name.text = name;
            _tag.text = tag;
            _desc.text = desc;
            _requirements.text = requirements;
        }
    }
}
