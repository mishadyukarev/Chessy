using Assets.Scripts.ECS.Components;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Menu.Entities
{
    public sealed class EntViewMenuElseManager
    {
        private EcsEntity _sliderEnt;
        internal ref SliderCommComponent SoundEnt_SliderCom => ref _sliderEnt.Get<SliderCommComponent>();


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


        internal EntViewMenuElseManager(EcsWorld menuWorld)
        {
            var slider = ViewCommonContainerUICanvas.FindUnderParent<Slider>(SceneTypes.Menu, "Slider");
            _sliderEnt = menuWorld.NewEntity()
                .Replace(new SliderCommComponent(slider));
            slider.value = DataCommContainerElseSaver.SliderVolume;












            var rightZone = ViewCommonContainerUICanvas.FindUnderParent<RectTransform>(SceneTypes.Menu, "OnlineRightZone");


            var connectOnlineButton = rightZone.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            _joinOnlineEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(connectOnlineButton));

            var onlineRightZoneImage = rightZone.transform.Find("RightZone_Image").GetComponent<Image>();
            _onlineRightZoneEnt = menuWorld.NewEntity()
                .Replace(new ImageComponent(onlineRightZoneImage));

            var stepMod_Dropdown = rightZone.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();

            _stepModeUIEnt = menuWorld.NewEntity()
                .Replace(new DropdownTMPComponent(stepMod_Dropdown));

            var createRoomButton = rightZone.transform.Find("CreateRoomButton").GetComponent<Button>();
            _createRoomEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(createRoomButton));


            var joinRandomRoomButton = rightZone.transform.Find("JoinRandom_Button").GetComponent<Button>();
            _joinRandomRoomEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(joinRandomRoomButton));


            var createFriendRoomButton = rightZone.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            var createFriendRoomInputField = rightZone.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();
            _createFriendRoomEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(createFriendRoomButton))
                .Replace(new InputFieldComponent(createFriendRoomInputField));


            var joinFriendRoomButton = rightZone.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            var joinFriendRoomInputField = rightZone.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();

            _joinFriendRoomEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(joinFriendRoomButton))
                .Replace(new InputFieldComponent(joinFriendRoomInputField));




            var leftZone = ViewCommonContainerUICanvas.FindUnderParent<RectTransform>(SceneTypes.Menu, "OfflineLeftZone");

            var connectOfflineButton = leftZone.transform.Find("ConnectOffline_Button").GetComponent<Button>();
            _joinOfflineEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(connectOfflineButton));

            var leftZoneImage = leftZone.transform.Find("LeftZone_Image").GetComponent<Image>();
            _offlineLeftZoneEnt = menuWorld.NewEntity()
                .Replace(new ImageComponent(leftZoneImage));

            var testSoloGameButton = leftZone.transform.Find("TestSoloGame_Button").GetComponent<Button>();
            _testSoloGameEnt = menuWorld.NewEntity()
                .Replace(new ButtonComponent(testSoloGameButton));
        }
    }
}
