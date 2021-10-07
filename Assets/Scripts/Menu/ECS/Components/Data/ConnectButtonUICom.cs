using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct ConnectButtonUICom
    {
        private Button _connectButton;

        internal ConnectButtonUICom(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _connectButton = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _connectButton = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();
        }

        internal void AddListConnect_Button(UnityAction unityAction) => _connectButton.onClick.AddListener(unityAction);
        internal void SetActive_Button(bool isActive) => _connectButton.gameObject.SetActive(isActive);
    }
}
