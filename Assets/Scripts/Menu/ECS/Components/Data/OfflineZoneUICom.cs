﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct OfflineZoneUICom
    {
        private Button _startWithBot_Button;
        private Button _withFriend_Button;

        internal OfflineZoneUICom(RectTransform leftZoneRectTrans)
        {
            _startWithBot_Button = leftZoneRectTrans.Find("Training_Button").GetComponent<Button>();
            _withFriend_Button = leftZoneRectTrans.Find("WithFriend_Button").GetComponent<Button>();
        }

        internal void AddListTrain(UnityAction unityAction) => _startWithBot_Button.onClick.AddListener(unityAction);
        internal void AddListFriend(UnityAction unityAction) => _withFriend_Button.onClick.AddListener(unityAction);
    }
}
