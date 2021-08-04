using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.View.Common;
using Assets.Scripts.Workers.Common;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class UIMenuMainViewSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _menuWorld;

        private EcsFilter<ToggleZoneComponent> _toggleZoneFilter;


        private static EcsEntity _canvasEnt;
        internal static ref CanvasComponent CanvasEnt_CanvasCom => ref _canvasEnt.Get<CanvasComponent>();




        private static EcsEntity _sliderEnt;
        internal static ref SliderCommComponent SoundEnt_SliderCom => ref _sliderEnt.Get<SliderCommComponent>();


        #region RightZone

        private static EcsEntity _rightOnlineEnt;
        internal static ref ButtonComponent JoinOnlineEnt_ButtonCom => ref _rightOnlineEnt.Get<ButtonComponent>();


        private static EcsEntity _onlineRightZoneEnt;
        internal static ref ImageComponent OnlineRightZoneEnt_ImageCom => ref _onlineRightZoneEnt.Get<ImageComponent>();


        private static EcsEntity _stepModeUIEnt;
        internal static ref DropdownTMPComponent StepModUIEnt_DropDownTMPCom => ref _stepModeUIEnt.Get<DropdownTMPComponent>();


        #region PublicGameZone

        private static EcsEntity _createRoomEnt;
        internal static ref ButtonComponent CreateRoomEnt_ButtonCom => ref _createRoomEnt.Get<ButtonComponent>();


        private static EcsEntity _joinRandomRoomEnt;
        internal static ref ButtonComponent JoinRandomRoomEnt_ButtonCom => ref _joinRandomRoomEnt.Get<ButtonComponent>();

        #endregion


        #region FriendGameZone

        private static EcsEntity _createFriendRoomEnt;
        internal static ref ButtonComponent CreateFriendRoomEnt_ButtonCom => ref _createFriendRoomEnt.Get<ButtonComponent>();
        internal static ref InputFieldComponent CreateFriendRoomEnt_InputFieldCom => ref _createFriendRoomEnt.Get<InputFieldComponent>();


        private static EcsEntity _joinFriendRoomEnt;
        internal static ref ButtonComponent JoinFriendRoomEnt_ButtonCom => ref _joinFriendRoomEnt.Get<ButtonComponent>();
        internal static ref InputFieldComponent JoinFriendRoomEnt_InputFieldCom => ref _joinFriendRoomEnt.Get<InputFieldComponent>();

        #endregion

        #endregion



        #region LeftZone

        private static EcsEntity _leftOfflineEnt;
        internal static ref ButtonComponent JoinOfflineEnt_ButtonCom => ref _leftOfflineEnt.Get<ButtonComponent>();


        private static EcsEntity _offlineLeftZoneEnt;
        internal static ref ImageComponent OfflineLeftZoneEnt_ImageCom => ref _offlineLeftZoneEnt.Get<ImageComponent>();


        private static EcsEntity _testSoloGameEnt;
        internal static ref ButtonComponent TestSoloGameEnt_ButtonCom => ref _testSoloGameEnt.Get<ButtonComponent>();

        #endregion


        public void Init()
        {
            var canvas = GameObject.Instantiate(MainDataCommSys.ResourcesEnt_ResourcesCommonCom.PrefabConfig.Canvas);
            canvas.name = "Canvas";
            _canvasEnt = _menuWorld.NewEntity()
                .Replace(new CanvasComponent(MainDataCommSys.ResourcesEnt_ResourcesCommonCom, Main.Instance.SceneType));


            _toggleZoneFilter.Get1(0).Attach(canvas.transform);



            var slider = CanvasEnt_CanvasCom.FindUnderParent<Slider>("Slider");
            _sliderEnt = _menuWorld.NewEntity()
                .Replace(new SliderCommComponent(slider));
            slider.value = MainDataCommSys.CommonZoneEnt_SaverCom.SliderVolume;



            var rightZone = CanvasEnt_CanvasCom.FindUnderParent<RectTransform>("OnlineRightZone");


            var connectOnlineButton = rightZone.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            _rightOnlineEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(connectOnlineButton));

            var onlineRightZoneImage = rightZone.transform.Find("RightZone_Image").GetComponent<Image>();
            _onlineRightZoneEnt = _menuWorld.NewEntity()
                .Replace(new ImageComponent(onlineRightZoneImage));

            var stepMod_Dropdown = rightZone.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();

            _stepModeUIEnt = _menuWorld.NewEntity()
                .Replace(new DropdownTMPComponent(stepMod_Dropdown));

            var createRoomButton = rightZone.transform.Find("CreateRoomButton").GetComponent<Button>();
            _createRoomEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(createRoomButton));


            var joinRandomRoomButton = rightZone.transform.Find("JoinRandom_Button").GetComponent<Button>();
            _joinRandomRoomEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(joinRandomRoomButton));


            var createFriendRoomButton = rightZone.transform.Find("CreateFriendRoom_Button").GetComponent<Button>();
            var createFriendRoomInputField = rightZone.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>();
            _createFriendRoomEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(createFriendRoomButton))
                .Replace(new InputFieldComponent(createFriendRoomInputField));


            var joinFriendRoomButton = rightZone.transform.Find("JoinFriendRoom_Button").GetComponent<Button>();
            var joinFriendRoomInputField = rightZone.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>();

            _joinFriendRoomEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(joinFriendRoomButton))
                .Replace(new InputFieldComponent(joinFriendRoomInputField));




            var leftZone = CanvasEnt_CanvasCom.FindUnderParent<RectTransform>("OfflineLeftZone");

            var connectOfflineButton = leftZone.transform.Find("ConnectOffline_Button").GetComponent<Button>();
            _leftOfflineEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(connectOfflineButton));

            var leftZoneImage = leftZone.transform.Find("LeftZone_Image").GetComponent<Image>();
            _offlineLeftZoneEnt = _menuWorld.NewEntity()
                .Replace(new ImageComponent(leftZoneImage));

            var testSoloGameButton = leftZone.transform.Find("TestSoloGame_Button").GetComponent<Button>();
            _testSoloGameEnt = _menuWorld.NewEntity()
                .Replace(new ButtonComponent(testSoloGameButton));
        }

        public void Run()
        {

        }
    }
}
