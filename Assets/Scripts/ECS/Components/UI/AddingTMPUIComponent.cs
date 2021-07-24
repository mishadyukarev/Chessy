using TMPro;

namespace Assets.Scripts.ECS.Components.UI
{
    internal struct AddingTMPUIComponent
    {
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal void StartFill(TextMeshProUGUI textMeshProUGUI) => _textMeshProUGUI = textMeshProUGUI;
    }
}
