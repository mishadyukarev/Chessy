using Game.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Menu
{
    public sealed class FillEntitieSys : IEcsInitSystem
    {
        private EcsWorld _curMenuW = default;

        public FillEntitieSys(EcsWorld menuWorld)
        {
            var menuSysts = new EcsSystems(menuWorld);



            var launchLikeGameSys = new EcsSystems(menuSysts.World)
                .Add(new LaunchLikeGameAndShopSys());

            var runUpdate = new EcsSystems(menuSysts.World)
                .Add(new SyncSys())
                .Add(new ConnectorMenuSys());

            new MenuSysDataViewC(launchLikeGameSys.Run);
            new DataSC(runUpdate.Run);



            menuSysts
                .Add(this)
                .Add(launchLikeGameSys)
                .Add(runUpdate)
                .Add(new EventSys());

            menuSysts.Init();

            MenuSysDataViewC.LaunchLikeGame.Invoke();
        }

        public void Init()
        {
            CanvasC.SetCurZone(SceneTypes.Menu);
            ToggleZoneVC.ReplaceZone(SceneTypes.Menu);


            var centerZone_Trans = CanvasC.FindUnderCurZone<Transform>("CenterZone");


            _curMenuW.NewEntity()
                .Replace(new CenterZoneUICom(centerZone_Trans, SoundC.Volume, HintC.IsOnHint))
                .Replace(new LikeGameUICom(centerZone_Trans));


            var rightZone = CanvasC.FindUnderCurZone<RectTransform>("OnlineRightZone");
            _curMenuW.NewEntity()
                .Replace(new OnZoneUIC(rightZone))
                .Replace(new ConnectorUIC(true, rightZone))
                .Replace(new BackgroundUIC(true, rightZone));


            var leftZone = CanvasC.FindUnderCurZone<RectTransform>("OfflineLeftZone");
            _curMenuW.NewEntity()
                .Replace(new OffZoneUIC(leftZone))
                .Replace(new ConnectorUIC(false, leftZone))
                .Replace(new BackgroundUIC(false, leftZone));
        }
    }
}
