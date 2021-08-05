using Assets.Scripts.ECS.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.UI
{
    internal struct OnlineZoneUIComponent
    {
        private TMP_Dropdown _stepMod_DropDown;

        private Button _createPublicRoom_Button;
        private Button _joinRandomPublicRoom_Button;

        private Button _createFriendRoom_Button;
        private TMP_InputField _createFriendRoom_InputField;

        private Button _joinFriendRoom_Button;
        private TMP_InputField _joinFriendRoom_InputField;

        internal StepModeTypes StepModValue => (StepModeTypes)(_stepMod_DropDown.value + 1);

        internal string TextCreateFriendRoom => _createFriendRoom_InputField.text;
        internal string TextJoinFriendRoom => _joinFriendRoom_InputField.text;


        internal OnlineZoneUIComponent(RectTransform rightZone_RectTrans)
        {
            _stepMod_DropDown = rightZone_RectTrans.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();

            _createPublicRoom_Button = rightZone_RectTrans.transform.Find("CreateRoomButton").GetComponent<Button>();
            _joinRandomPublicRoom_Button = rightZone_RectTrans.transform.Find("JoinRandom_Button").GetComponent<Button>();

            _createFriendRoom_Button = rightZone_RectTrans.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            _createFriendRoom_InputField = rightZone_RectTrans.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();


            _joinFriendRoom_Button = rightZone_RectTrans.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            _joinFriendRoom_InputField = rightZone_RectTrans.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();
        }

        internal void AddListenerCreatePublicRoom(UnityAction unityAction) => _createPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListenerJoinRandomPublicRoom(UnityAction unityAction) => _joinRandomPublicRoom_Button.onClick.AddListener(unityAction);
        internal void AddListenerCreateFriendRoom(UnityAction unityAction) => _createFriendRoom_Button.onClick.AddListener(unityAction);
        internal void AddListenerJoinFriendRoom(UnityAction unityAction) => _joinFriendRoom_Button.onClick.AddListener(unityAction);

        internal void SetActiveZone(bool isActive) => _stepMod_DropDown.gameObject.SetActive(isActive);
    }
}
