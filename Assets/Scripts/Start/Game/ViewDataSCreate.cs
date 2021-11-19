using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class ViewDataSCreate
    {
        public ViewDataSCreate(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var syncCellViewSyss = new EcsSystems(gameWorld)
                .Add(new VisibElseSys())
                .Add(new SyncCellUnitViewSys())
                .Add(new SyncCellSelUnitViewSys())
                .Add(new UnitStatCellSyncS())
                .Add(new SyncCellBuildViewSystem())
                .Add(new SyncCellEnvirsVisSystem())
                .Add(new CellEffsVisSyncS())
                .Add(new SupportSyncVS())
                .Add(new CellWeatherViewSys())
                .Add(new CellRiverViewSys())
                .Add(new FliperAndRotatorUnitSystem())
                .Add(new CellBarsEnvSystem())
                .Add(new SyncCellTrailSys())
                .Add(new CellStunViewS());


            var eventExecuters = new EcsSystems(gameWorld)
                .Add(new CenterEventUIS())
                .Add(new LeftCityEventUISys())
                .Add(new LeftEnvEventUISys())
                .Add(new DownEventUIS())
                .Add(new RightUnitEventUISys())
                .Add(new UpEventUIS());

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
                .Add(new PickUpgUIS())
                .Add(new HeroesSyncUISys());


            var rotateAll = new EcsSystems(gameWorld)
                .Add(new RotateAllSys());


            var runUpdate = new EcsSystems(gameWorld)
                .Add(syncCellViewSyss)
                .Add(syncCanvasViewSyss)
                .Add(new SoundClickCellS());


            new DataViewSC(runUpdate.Run, rotateAll.Run);




            gameSysts
                .Add(rotateAll)
                .Add(runUpdate)
                .Add(eventExecuters);
        }
    }
}