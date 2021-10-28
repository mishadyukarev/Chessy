using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct LeaveViewUIC
    {
        private Button _leave_button;

        public LeaveViewUIC(Button button)
        {
            _leave_button = button;
        }

        public void AddListener(UnityAction unityAction) => _leave_button.onClick.AddListener(unityAction);
    }
}
