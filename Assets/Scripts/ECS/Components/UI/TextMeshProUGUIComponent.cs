using TMPro;
using UnityEngine;

internal struct TextMeshProUGUIComponent
{
    internal TextMeshProUGUI TextMeshProUGUI { get; }

    internal TextMeshProUGUIComponent(TextMeshProUGUI text) => TextMeshProUGUI = text;
}
