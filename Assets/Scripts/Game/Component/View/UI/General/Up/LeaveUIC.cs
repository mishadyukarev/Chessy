using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct LeaveUIC
    {
        private static Button _button;

        public LeaveUIC(Button button)
        {
            _button = button;
        }

        public static void AddListener(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
    }
}
