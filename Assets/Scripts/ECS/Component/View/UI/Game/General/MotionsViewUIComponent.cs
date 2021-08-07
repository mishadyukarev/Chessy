using TMPro;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct MotionsViewUIComponent
    {
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal MotionsViewUIComponent(TextMeshProUGUI textMeshProUGUI)
        {
            _textMeshProUGUI = textMeshProUGUI;
        }

        internal void SetActiveParent(bool isActive) => _textMeshProUGUI.gameObject.SetActive(isActive);
    }
}
