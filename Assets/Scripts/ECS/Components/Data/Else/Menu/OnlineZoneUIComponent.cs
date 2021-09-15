using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct OnlineZoneUIComponent
    {
        private TextMeshProUGUI _publicGame_TextMP;
        private Button _createPublicRoom_Button;
        private TextMeshProUGUI _createPublicRoom_TextMP;

        private TextMeshProUGUI _friendGame_TextMP;
        private Button _joinRandomPublicRoom_Button;
        private TextMeshProUGUI _joinRandomPublicRoom_TextMP;


        private Button _createFR_Button;
        private TextMeshProUGUI _createFR_TextMP;
        private TMP_InputField _createFR_InputField;

        private Button _joinFR_Button;
        private TextMeshProUGUI _joinFR_TextMP;
        private TMP_InputField _joinFR_InputField;


        internal string TextCreateFriendRoom => _createFR_InputField.text;
        internal string TextJoinFriendRoom => _joinFR_InputField.text;


        internal OnlineZoneUIComponent(RectTransform rightZone_RectTrans)
        {
            _publicGame_TextMP = rightZone_RectTrans.transform.Find("PublicGme_TextMP").GetComponent<TextMeshProUGUI>();
            _createPublicRoom_Button = rightZone_RectTrans.transform.Find("CreateRoomButton").GetComponent<Button>();
            _createPublicRoom_TextMP = _createPublicRoom_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _friendGame_TextMP = rightZone_RectTrans.Find("FriendGame_TextMP").GetComponent<TextMeshProUGUI>();
            _joinRandomPublicRoom_Button = rightZone_RectTrans.transform.Find("JoinRandom_Button").GetComponent<Button>();
            _joinRandomPublicRoom_TextMP = _joinRandomPublicRoom_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _createFR_Button = rightZone_RectTrans.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            _createFR_TextMP = _createFR_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            _createFR_InputField = rightZone_RectTrans.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();


            _joinFR_Button = rightZone_RectTrans.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            _joinFR_TextMP = _joinFR_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            _joinFR_InputField = rightZone_RectTrans.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();
        }

        internal void AddListenerCreatePublicRoom(UnityAction unityAction) => _createPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListenerJoinRandomPublicRoom(UnityAction unityAction) => _joinRandomPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListenerCreateFriendRoom(UnityAction unityAction) => _createFR_Button.onClick.AddListener(unityAction);
        internal void AddListenerJoinFriendRoom(UnityAction unityAction) => _joinFR_Button.onClick.AddListener(unityAction);

        internal void SetTextPublicG(string text) => _publicGame_TextMP.text = text;
        internal void SetTextCreatePGR(string text) => _createPublicRoom_TextMP.text = text;
        internal void SetTextJoinPGR(string text) => _joinRandomPublicRoom_TextMP.text = text;

        internal void SetTextFriendG(string text) => _friendGame_TextMP.text = text;
        internal void SetTextCreateFGR(string text) => _createFR_TextMP.text = text;
        internal void SetTextJoinFGR(string text) => _joinFR_TextMP.text = text;
    }
}
