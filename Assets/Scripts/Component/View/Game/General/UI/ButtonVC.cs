using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Game.Game.EntityLeftCityUIPool;
using static Game.Game.EntityLeftEnvUIPool;

namespace Game.Game
{
    public readonly struct ButtonVC : ILeftCityMeltButtonUIE, ILeftCityBuyButtonsUIE, ILeftEnvInfoButtonUIE
    {
        readonly Button _button;

        public Color Color
        {
            get => _button.image.color;
            set => _button.image.color = value;
        }
        public bool IsActiveParent => _button.transform.parent.gameObject.activeSelf;

        public ButtonVC(Button button) => _button = button;

        public void AddList(UnityAction action) => _button.onClick.AddListener(action);
        public void SetActive(in bool needActive) => _button.gameObject.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => _button.transform.parent.gameObject.SetActive(needActive);
    }
}