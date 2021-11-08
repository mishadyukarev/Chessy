using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct LeaveViewUIC
    {
        private static Button _leave_button;

        public LeaveViewUIC(Button button)
        {
            _leave_button = button;
        }

        public static void AddListener(UnityAction unityAction) => _leave_button.onClick.AddListener(unityAction);
    }
}
