using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Game.Game.LeftEnvironmentUIEs;

namespace Game.Game
{
    public readonly struct ButtonUIC : ILeftEnvInfoButtonUIE
    {
        readonly Button _button;

        public Color Color
        {
            get => _button.image.color;
            set => _button.image.color = value;
        }
        public bool IsEnabled
        {
            get => _button.enabled;
            set => _button.enabled = value;
        }

        public ButtonUIC(Button button) => _button = button;

        public void AddListener(UnityAction action) => _button.onClick.AddListener(action);
        public void SetActive(in bool needActive) => _button.gameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => _button.transform.parent.gameObject.SetActive(needActive);
    }
}