namespace Whatever.UI
{
    public class SettingUIContent : MainMenuUIContent
    {
        private static readonly string[] _subMenuNameKeys = new string[] { "Game" };

        public override string TitleKey => "Setting";
        public override string[] SubMenuNameKeys => _subMenuNameKeys;

        public override void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public override void ShowSubMenu(int index)
        {

        }
    }
}
