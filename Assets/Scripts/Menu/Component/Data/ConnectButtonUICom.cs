using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct ConnectButtonUICom
    {
        private Button _connectButton;

        public ConnectButtonUICom(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _connectButton = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _connectButton = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();
        }

        public void AddListConnect_Button(UnityAction unityAction) => _connectButton.onClick.AddListener(unityAction);
        public void SetActive_Button(bool isActive) => _connectButton.gameObject.SetActive(isActive);
    }
}
