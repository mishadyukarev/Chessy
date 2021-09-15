using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct OfflineZoneUIComponent
    {
        private Button _startWithBotButton;
        private TextMeshProUGUI _textTraining;
        
        internal OfflineZoneUIComponent(RectTransform leftZoneRectTrans)
        {
            _startWithBotButton = leftZoneRectTrans.Find("TestSoloGame_Button").GetComponent<Button>();
            _textTraining = _startWithBotButton.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void AddListenerToStartWithBotButton(UnityAction unityAction) => _startWithBotButton.onClick.AddListener(unityAction);
        internal void SetTextTraining(string text) => _textTraining.text = text;
    }
}
