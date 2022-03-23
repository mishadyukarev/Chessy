using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct OffZoneUIC
    {
        static Button _startWithBot_Button;
        static Button _withFriend_Button;

        public OffZoneUIC(RectTransform leftZoneRectTrans)
        {
            _startWithBot_Button = leftZoneRectTrans.Find("Training_Button").GetComponent<Button>();
            _withFriend_Button = leftZoneRectTrans.Find("WithFriend_Button").GetComponent<Button>();
        }

        public static void AddListTrain(UnityAction unityAction) => _startWithBot_Button.onClick.AddListener(unityAction);
        public static void AddListFriend(UnityAction unityAction) => _withFriend_Button.onClick.AddListener(unityAction);
    }
}
