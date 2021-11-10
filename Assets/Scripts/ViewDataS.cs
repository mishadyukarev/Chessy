using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class ViewDataS
    {
        public ViewDataS(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var syncCellViewSyss = new EcsSystems(gameWorld)
                .Add(new VisibElseSys())
                .Add(new SyncCellUnitViewSys())
                .Add(new SyncCellSelUnitViewSys())
                .Add(new SyncCellUnitSupVisSystem())
                .Add(new SyncCellBuildViewSystem())
                .Add(new SyncCellEnvirsVisSystem())
                .Add(new SyncCellEffectsVisSystem())
                .Add(new SyncSupportViewSystem())
                .Add(new CellWeatherViewSys())
                .Add(new CellRiverViewSys())
                .Add(new FliperAndRotatorUnitSystem())
                .Add(new CellBarsEnvSystem())
                .Add(new SyncCellTrailSys())
                .Add(new CellStunViewS());


            var syncCanvasViewSyss = new EcsSystems(gameWorld)
            ///left
            .Add(new BuildZoneUISys())
               .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISys())
                .Add(new StatsUISystem())
                .Add(new ProtectUISys())
                .Add(new RelaxUISys())
                .Add(new UniqButSyncUISys())
                .Add(new FirstButtonBuildUISys())
                .Add(new SecButtonBuildUISys())
                .Add(new ThirdButtonBuildUISys())
                .Add(new ShieldUISys())
                .Add(new EffectsUISys())

            ///down
            .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new GiveTakeUISystem())
                .Add(new ScoutSyncUIS())
                .Add(new HeroSyncUIS())

            ///up
            .Add(new EconomyUpUISys())
            .Add(new WindUISys())

            ///center
            .Add(new SelectorUISys())
                .Add(new TheEndGameUISystem())
                .Add(new MotionCenterUISystem())
                .Add(new ReadyZoneUISystem())
                .Add(new MistakeUISys())
                .Add(new KingZoneUISys())
                .Add(new FriendZoneUISys())
                .Add(new ActiveHitUISys())
                .Add(new PickUpgUISys())
                .Add(new HeroesSyncUISys());


            var rotateCurPlayer = new EcsSystems(gameWorld)
                .Add(new RotateAllSys());


            var sysGenDataView = new EcsSystems(gameWorld)
                .Add(syncCellViewSyss)
                .Add(syncCanvasViewSyss);


            new GameGenSysDataViewC(sysGenDataView.Run, rotateCurPlayer.Run);




            gameSysts
                .Add(rotateCurPlayer)
                .Add(sysGenDataView);
        }
    }
}