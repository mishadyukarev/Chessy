using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct OfflineZoneUIComponent
    {
        private Button _startWithBotButton;

        internal OfflineZoneUIComponent(RectTransform leftZoneRectTrans)
        {
            _startWithBotButton = leftZoneRectTrans.Find("TestSoloGame_Button").GetComponent<Button>();
        }

        internal void AddListenerToStartWithBotButton(UnityAction unityAction) => _startWithBotButton.onClick.AddListener(unityAction);
    }
}
