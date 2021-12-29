using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct ButtonC
    {
        Button _button;

        public Color Color
        {
            get => _button.image.color;
            set => _button.image.color = value;
        }

        public ButtonC(Button button) => _button = button;

        public void AddList(UnityAction action) => _button.onClick.AddListener(action);
        public void SetActiveParent(in bool needActive) => _button.transform.parent.gameObject.SetActive(needActive);
    }
}