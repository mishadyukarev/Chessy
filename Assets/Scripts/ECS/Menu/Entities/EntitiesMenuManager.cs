using Leopotam.Ecs;
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

        #endregion


        private EcsEntity _joinOfflineEnt;
        internal ref ButtonComponent JoinOfflineEnt_ButtonCom => ref _joinOfflineEnt.Get<ButtonComponent>();


        internal EntitiesMenuManager(EcsWorld ecsWorld)
        {
            _joinOnlineEnt = ecsWorld.NewEntity();
            _onlineRightZoneEnt = ecsWorld.NewEntity();
            _joinOfflineEnt = ecsWorld.NewEntity();
        }

        internal void FillEntities()
        {
            var rightZone = Instance.CanvasManager.FindUnderParent<RectTransform>(SceneTypes.Menu, "OnlineRightZone");

            var connectOnlineButton = rightZone.transform.Find("ConnectOnline_Button").GetComponent<Button>();
            JoinOnlineEnt_ButtonCom.StartFill(connectOnlineButton);

            var onlineRightZoneImage = rightZone.transform.Find("RightZone_Image").GetComponent<Image>();
            OnlineRightZoneEnt_ImageCom.StartFill(onlineRightZoneImage);


            var leftZone = Instance.CanvasManager.FindUnderParent<RectTransform>(SceneTypes.Menu, "OfflineLeftZone");

            var connectOfflineButton = leftZone.transform.Find("ConnectOffline_Button").GetComponent<Button>();
            JoinOfflineEnt_ButtonCom.StartFill(connectOfflineButton);

        }
    }
}
