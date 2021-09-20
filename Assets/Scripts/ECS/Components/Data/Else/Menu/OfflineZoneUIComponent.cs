using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct OfflineZoneUIComponent
    {
        private Button _startWithBot_Button;
        private TextMeshProUGUI _training_TextMP;
        private Button _withFriend_Button;
        private TextMeshProUGUI _withFriend_TextMP;

        internal OfflineZoneUIComponent(RectTransform leftZoneRectTrans)
        {
            _startWithBot_Button = leftZoneRectTrans.Find("Training_Button").GetComponent<Button>();
            _training_TextMP = _startWithBot_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            _withFriend_Button = leftZoneRectTrans.Find("WithFriend_Button").GetComponent<Button>();
            _withFriend_TextMP = _withFriend_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void AddListenerTrain(UnityAction unityAction) => _startWithBot_Button.onClick.AddListener(unityAction);
        internal void AddListenerFriend(UnityAction unityAction) => _withFriend_Button.onClick.AddListener(unityAction);

        internal void SetTextTraining(string text) => _training_TextMP.text = text;
        internal void SetTextFriend(string text) => _withFriend_TextMP.text = text;
    }
}
