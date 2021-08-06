using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component
{
    internal struct ConnectButtonUIComponent
    {
        private Button _connectButton;

        internal ConnectButtonUIComponent(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline) _connectButton = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            else _connectButton = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();
        }

        internal void AddListenerToConnectButton(UnityAction unityAction) => _connectButton.onClick.AddListener(unityAction);
        internal void SetActiveButton(bool isActive) => _connectButton.gameObject.SetActive(isActive);
    }
}
