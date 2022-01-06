using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Menu
{
    public struct ConnectorUIC
    {
        private Button _button;

        public ConnectorUIC(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _button = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _button = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();
        }

        public void AddListConnect_Button(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
        public void SetActive_Button(bool isActive) => _button.gameObject.SetActive(isActive);
    }
}
