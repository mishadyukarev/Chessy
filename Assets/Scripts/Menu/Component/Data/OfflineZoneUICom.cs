using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct OfflineZoneUICom
    {
        private Button _startWithBot_Button;
        private Button _withFriend_Button;

        public OfflineZoneUICom(RectTransform leftZoneRectTrans)
        {
            _startWithBot_Button = leftZoneRectTrans.Find("Training_Button").GetComponent<Button>();
            _withFriend_Button = leftZoneRectTrans.Find("WithFriend_Button").GetComponent<Button>();
        }

        public void AddListTrain(UnityAction unityAction) => _startWithBot_Button.onClick.AddListener(unityAction);
        public void AddListFriend(UnityAction unityAction) => _withFriend_Button.onClick.AddListener(unityAction);
    }
}
