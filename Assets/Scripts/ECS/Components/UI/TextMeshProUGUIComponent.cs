using TMPro;
using UnityEngine;

internal struct TextMeshProUGUIComponent
{
    private TextMeshProUGUI _textMeshProUGUI;
    internal string Text
    {
        get => _textMeshProUGUI.text;
        set => _textMeshProUGUI.text = value;
    }
    internal Color Color
    {
        set => _textMeshProUGUI.color = value;
    }

    internal void StartFill(TextMeshProUGUI textMeshProUGUI) => _textMeshProUGUI = textMeshProUGUI;
    internal void SetActive(bool isActive) => _textMeshProUGUI.gameObject.SetActive(isActive);
    internal void SetText(string text) => _textMeshProUGUI.text = text;
}
