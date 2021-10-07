using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct OnlineZoneUICom
    {
        private Button _createPublicRoom_Button;
        private Button _joinRandomPublicRoom_Button;


        private Button _createFR_Button;
        private TMP_InputField _createFR_InputField;

        private Button _joinFR_Button;
        private TMP_InputField _joinFR_InputField;


        internal string TextCreateFriendRoom => _createFR_InputField.text;
        internal string TextJoinFriendRoom => _joinFR_InputField.text;


        internal OnlineZoneUICom(RectTransform rightZone_RectTrans)
        {
            _createPublicRoom_Button = rightZone_RectTrans.transform.Find("CreateRoomButton").GetComponent<Button>();

            _joinRandomPublicRoom_Button = rightZone_RectTrans.transform.Find("JoinRandom_Button").GetComponent<Button>();

            _createFR_Button = rightZone_RectTrans.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            _createFR_InputField = rightZone_RectTrans.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();


            _joinFR_Button = rightZone_RectTrans.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            _joinFR_InputField = rightZone_RectTrans.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();
        }

        internal void AddListCreatePublicRoom(UnityAction unityAction) => _createPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListJoinRandomPublicRoom(UnityAction unityAction) => _joinRandomPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListCreateFriendRoom(UnityAction unityAction) => _createFR_Button.onClick.AddListener(unityAction);
        internal void AddListJoinFriendRoom(UnityAction unityAction) => _joinFR_Button.onClick.AddListener(unityAction);
    }
}
