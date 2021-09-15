using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component
{
    internal struct ConnectButtonUIComp
    {
        private Button _connectButton;
        private TextMeshProUGUI _text_TextMP;

        internal string Text
        {
            get => _text_TextMP.text;
            set => _text_TextMP.text = value;
        }

        internal ConnectButtonUIComp(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _connectButton = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _connectButton = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();

            _text_TextMP = _connectButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void AddListenerToConnectButton(UnityAction unityAction) => _connectButton.onClick.AddListener(unityAction);
        internal void SetActiveButton(bool isActive) => _connectButton.gameObject.SetActive(isActive);
    }
}
