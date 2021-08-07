using TMPro;

namespace Assets.Scripts.ECS.Component.Data.UI.Game.General
{
    internal struct MistakeViewUICom
    {
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal MistakeViewUICom(TextMeshProUGUI textMeshProUGUI)
        {
            _textMeshProUGUI = textMeshProUGUI;
        }

        internal void SetActiveParent(bool isActive) => _textMeshProUGUI.transform.parent.gameObject.SetActive(isActive);
    }
}
