using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct ConnectButtonUICom
    {
        private Button _connectButton;
        private TextMeshProUGUI _text_TextMP;

        internal string Text
        {
            get => _text_TextMP.text;
            set => _text_TextMP.text = value;
        }

        internal ConnectButtonUICom(bool isOnline, RectTransform zoneRectTrans)
        {
            if (isOnline)
            {
                _connectButton = zoneRectTrans.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            }
            else _connectButton = zoneRectTrans.transform.Find("ConnectOffline_Button").GetComponent<Button>();

            _text_TextMP = _connectButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void AddListConnect_Button(UnityAction unityAction) => _connectButton.onClick.AddListener(unityAction);
        internal void SetActive_Button(bool isActive) => _connectButton.gameObject.SetActive(isActive);
    }
}
