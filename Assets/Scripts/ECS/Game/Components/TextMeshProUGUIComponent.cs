using TMPro;

internal struct TextMeshProUGUIComponent
{
    private TextMeshProUGUI _textMeshProUGUI;
    internal string Text => _textMeshProUGUI.text;

    internal void StartFill(TextMeshProUGUI textMeshProUGUI) => _textMeshProUGUI = textMeshProUGUI;
    internal void SetActive(bool isActive) => _textMeshProUGUI.gameObject.SetActive(isActive);
    internal void SetText(string text) => _textMeshProUGUI.text = text;
}
