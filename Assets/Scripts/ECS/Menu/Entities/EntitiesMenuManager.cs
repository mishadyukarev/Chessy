using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Main;

namespace Assets.Scripts.ECS.Menu.Entities
{
    public sealed class EntitiesMenuManager
    {
        #region RightZone

        private EcsEntity _joinOnlineEnt;
        internal ref ButtonComponent JoinOnlineEnt_ButtonCom => ref _joinOnlineEnt.Get<ButtonComponent>();


        private EcsEntity _onlineRightZoneEnt;
        internal ref ImageComponent OnlineRightZoneEnt_ImageCom => ref _onlineRightZoneEnt.Get<ImageComponent>();


        private EcsEntity _stepModeUIEnt;
        internal ref DropdownTMPComponent StepModUIEnt_DropDownTMPCom => ref _stepModeUIEnt.Get<DropdownTMPComponent>();


        #region PublicGameZone

        private EcsEntity _createRoomEnt;
        internal ref ButtonComponent CreateRoomEnt_ButtonCom => ref _createRoomEnt.Get<ButtonComponent>();


        private EcsEntity _joinRandomRoomEnt;
        internal ref ButtonComponent JoinRandomRoomEnt_ButtonCom => ref _joinRandomRoomEnt.Get<ButtonComponent>();

        #endregion


        #region FriendGameZone

        private EcsEntity _createFriendRoomEnt;
        internal ref ButtonComponent CreateFriendRoomEnt_ButtonCom => ref _createFriendRoomEnt.Get<ButtonComponent>();
        internal ref InputFieldComponent CreateFriendRoomEnt_InputFieldCom => ref _createFriendRoomEnt.Get<InputFieldComponent>();


        private EcsEntity _joinFriendRoomEnt;
        internal ref ButtonComponent JoinFriendRoomEnt_ButtonCom => ref _joinFriendRoomEnt.Get<ButtonComponent>();
        internal ref InputFieldComponent JoinFriendRoomEnt_InputFieldCom => ref _joinFriendRoomEnt.Get<InputFieldComponent>();

        #endregion

        #endregion


        #region LeftZone

        private EcsEntity _joinOfflineEnt;
        internal ref ButtonComponent JoinOfflineEnt_ButtonCom => ref _joinOfflineEnt.Get<ButtonComponent>();


        private EcsEntity _offlineLeftZoneEnt;
        internal ref ImageComponent OfflineLeftZoneEnt_ImageCom => ref _offlineLeftZoneEnt.Get<ImageComponent>();


        private EcsEntity _testSoloGameEnt;
        internal ref ButtonComponent TestSoloGameEnt_ButtonCom => ref _testSoloGameEnt.Get<ButtonComponent>();

        #endregion


        internal EntitiesMenuManager(EcsWorld ecsWorld)
        {
            _joinOnlineEnt = ecsWorld.NewEntity();
            _onlineRightZoneEnt = ecsWorld.NewEntity();
            _stepModeUIEnt = ecsWorld.NewEntity();

            _createRoomEnt = ecsWorld.NewEntity();
            _joinRandomRoomEnt = ecsWorld.NewEntity();
            _createFriendRoomEnt = ecsWorld.NewEntity();
            _joinFriendRoomEnt = ecsWorld.NewEntity();

            _joinOfflineEnt = ecsWorld.NewEntity();
            _offlineLeftZoneEnt = ecsWorld.NewEntity();
            _testSoloGameEnt = ecsWorld.NewEntity();
        }

        internal void FillEntities()
        {
            var rightZone = Instance.CanvasManager.FindUnderParent<RectTransform>(SceneTypes.Menu, "OnlineRightZone");

            var connectOnlineButton = rightZone.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            JoinOnlineEnt_ButtonCom.StartFill(connectOnlineButton);

            var onlineRightZoneImage = rightZone.transform.Find("RightZone_Image").GetComponent<Image>();
            OnlineRightZoneEnt_ImageCom.StartFill(onlineRightZoneImage);

            var stepMod_Dropdown = rightZone.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
            StepModUIEnt_DropDownTMPCom.StartFill(stepMod_Dropdown);

            var createRoomButton = rightZone.transform.Find("CreateRoomButton").GetComponent<Button>();
            CreateRoomEnt_ButtonCom.StartFill(createRoomButton);

            var joinRandomRoomButton = rightZone.transform.Find("JoinRandom_Button").GetComponent<Button>();
            JoinRandomRoomEnt_ButtonCom.StartFill(joinRandomRoomButton);

            var createFriendRoomButton = rightZone.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            CreateFriendRoomEnt_ButtonCom.StartFill(createFriendRoomButton);
            var createFriendRoomInputField = rightZone.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();
            CreateFriendRoomEnt_InputFieldCom.StartFill(createFriendRoomInputField);

            var joinFriendRoomButton = rightZone.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            JoinFriendRoomEnt_ButtonCom.StartFill(joinFriendRoomButton);
            var joinFriendRoomInputField = rightZone.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();
            JoinFriendRoomEnt_InputFieldCom.StartFill(joinFriendRoomInputField);




            var leftZone = Instance.CanvasManager.FindUnderParent<RectTransform>(SceneTypes.Menu, "OfflineLeftZone");

            var connectOfflineButton = leftZone.transform.Find("ConnectOffline_Button").GetComponent<Button>();
            JoinOfflineEnt_ButtonCom.StartFill(connectOfflineButton);

            var leftZoneImage = leftZone.transform.Find("LeftZone_Image").GetComponent<Image>();
            OfflineLeftZoneEnt_ImageCom.StartFill(leftZoneImage);

            var testSoloGameButton = leftZone.transform.Find("TestSoloGame_Button").GetComponent<Button>();
            TestSoloGameEnt_ButtonCom.StartFill(testSoloGameButton);
        }
    }
}
