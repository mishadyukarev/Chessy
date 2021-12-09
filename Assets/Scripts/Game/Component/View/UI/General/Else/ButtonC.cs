using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct ButtonC
    {
        Button _button;

        internal ButtonC(Button button) => _button = button;

        public void AddList(UnityAction action) => _button.onClick.AddListener(action);
    }
}