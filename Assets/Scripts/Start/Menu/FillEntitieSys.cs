using Leopotam.Ecs;
using Game.Common;
using UnityEngine;

namespace Game.Menu
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
            new DataSC(runUpdate.Run);

            

            menuSysts
                .Add(this)    
                .Add(launchLikeGameSys)
                .Add(runUpdate)
                .Add(new EventSys());

            menuSysts.Init();



            Common.DataSC.Invoke(ActionDataTypes.LaunchAdd);
            MenuSysDataViewC.LaunchLikeGame.Invoke();
        }

        public void Init()
        {
            CanvasC.SetCurZone(SceneTypes.Menu);
            ToggleZoneVC.ReplaceZone(SceneTypes.Menu);


            var centerZone_Trans = CanvasC.FindUnderCurZone<Transform>("CenterZone");


            _curMenuWorld.NewEntity()
                .Replace(new CenterZoneUICom(centerZone_Trans, SoundComC.Volume, HintComC.IsOnHint))
                .Replace(new LikeGameUICom(centerZone_Trans));


            var rightZone = CanvasC.FindUnderCurZone<RectTransform>("OnlineRightZone");
            _curMenuWorld.NewEntity()
                .Replace(new OnZoneUIC(rightZone))
                .Replace(new ConnectorUIC(true, rightZone))
                .Replace(new BackgroundUIC(true, rightZone));


            var leftZone = CanvasC.FindUnderCurZone<RectTransform>("OfflineLeftZone");
            _curMenuWorld.NewEntity()
                .Replace(new OffZoneUIC(leftZone))
                .Replace(new ConnectorUIC(false, leftZone))
                .Replace(new BackgroundUIC(false, leftZone));
        }
    }
}
