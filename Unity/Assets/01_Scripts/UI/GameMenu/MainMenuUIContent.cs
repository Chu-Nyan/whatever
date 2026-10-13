using UnityEngine;

namespace Whatever.UI
{
    public abstract class MainMenuUIContent : MonoBehaviour
    {
        public abstract string TitleKey { get; }
        public abstract string[] SubMenuNameKeys { get; }
        public abstract void ShowSubMenu(int index);
        public abstract void SetActive(bool value);
    }
}
