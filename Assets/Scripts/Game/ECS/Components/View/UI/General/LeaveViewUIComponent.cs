﻿using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct LeaveViewUIComponent
    {
        private Button _leave_button;

        internal LeaveViewUIComponent(Button button)
        {
            _leave_button = button;
        }

        internal void AddListener(UnityAction unityAction) => _leave_button.onClick.AddListener(unityAction);
    }
}
