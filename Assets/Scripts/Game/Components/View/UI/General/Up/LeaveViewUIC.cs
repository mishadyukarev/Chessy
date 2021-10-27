using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct LeaveViewUIC
    {
        private Button _leave_button;

        internal LeaveViewUIC(Button button)
        {
            _leave_button = button;
        }

        internal void AddListener(UnityAction unityAction) => _leave_button.onClick.AddListener(unityAction);
    }
}
