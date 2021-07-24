using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

internal struct ButtonComponent
{
    private Button _button;

    internal void StartFill(Button button) => _button = button;

    internal void AddListener(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
    internal void RemoveAllListeners() => _button.onClick.RemoveAllListeners();

    internal void SetColor(Color color) => _button.image.color = color;
    internal void SetActive(bool isActive) => _button.gameObject.SetActive(isActive);
}