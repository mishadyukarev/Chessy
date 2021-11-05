using Leopotam.Ecs;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Menu
{
    public sealed class FillEntitieSys : IEcsInitSystem
    {
        private EcsWorld _curMenuWorld = default;

        public FillEntitieSys(EcsWorld menuWorld)
        {
            var menuSysts = new EcsSystems(menuWorld);



            var launchLikeGameSys = new EcsSystems(menuSysts.World)
                .Add(new LaunchLikeGameAndShopSys());

            var runUpdate = new EcsSystems(menuSysts.World)
                .Add(new SyncSys())
                .Add(new ConnectorMenuSys());

            new MenuSysDataViewC(launchLikeGameSys.Run);
            new MenuSysDataC(runUpdate.Run);

            

            menuSysts
                .Add(this)    
                .Add(launchLikeGameSys)
                .Add(runUpdate)
                .Add(new EventSys());

            menuSysts.Init();



            ComSysDataC.Invoke(EventDataTypes.LaunchAdd);
            MenuSysDataViewC.LaunchLikeGame.Invoke();
        }

        public void Init()
        {
            CanvasCom.ReplaceZone(SceneTypes.Menu);
            ToggleZoneComponent.ReplaceZone(SceneTypes.Menu);


            var centerZone_Trans = CanvasCom.FindUnderParent<Transform>("CenterZone");


            _curMenuWorld.NewEntity()
                .Replace(new CenterZoneUICom(centerZone_Trans, SoundComC.Volume, HintComC.IsOnHint))
                .Replace(new ShopZoneUICom(centerZone_Trans))
                .Replace(new LikeGameUICom(centerZone_Trans));


            var rightZone = CanvasCom.FindUnderParent<RectTransform>("OnlineRightZone");
            _curMenuWorld.NewEntity()
                .Replace(new OnlineZoneUICom(rightZone))
                .Replace(new ConnectButtonUICom(true, rightZone))
                .Replace(new BackgroundMenuUICom(true, rightZone));


            var leftZone = CanvasCom.FindUnderParent<RectTransform>("OfflineLeftZone");
            _curMenuWorld.NewEntity()
                .Replace(new OfflineZoneUICom(leftZone))
                .Replace(new ConnectButtonUICom(false, leftZone))
                .Replace(new BackgroundMenuUICom(false, leftZone));
        }
    }
}
