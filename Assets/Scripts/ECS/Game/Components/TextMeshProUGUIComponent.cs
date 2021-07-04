using TMPro;

internal struct TextMeshProUGUIComponent
{
    private TextMeshProUGUI _textMeshProUGUI;

    internal string Text
    {
        get => _textMeshProUGUI.text;
        set => _textMeshProUGUI.text = value;
    }

    internal void SetTextMeshProUGUI(TextMeshProUGUI textMeshProUGUI) => _textMeshProUGUI = textMeshProUGUI;
    internal void SetActive(bool isActive) => _textMeshProUGUI.gameObject.SetActive(isActive);
}
