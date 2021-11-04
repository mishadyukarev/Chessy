using Leopotam.Ecs;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class GameGenSysViewM : SystemAbstManager
    {
        internal EcsSystems SyncCanvasViewSyss { get; private set; }
        internal EcsSystems SyncCellViewSyss { get; private set; }

        public GameGenSysViewM(EcsWorld gameWorld, EcsSystems allSystems) : base(gameWorld, allSystems)
        {
            SyncCellViewSyss = new EcsSystems(gameWorld)
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
                .Add(new SyncCellTrailSys());


            SyncCanvasViewSyss = new EcsSystems(gameWorld)
            ///left
            .Add(new BuildZoneUISys())
               .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISys())
                .Add(new StatsUISystem())
                .Add(new ProtectUISys())
                .Add(new RelaxUISys())
                .Add(new UniqueAbilitUISys())
                .Add(new SecondUniqueUISys())
                .Add(new FirstButtonBuildUISys())
                .Add(new SecButtonBuildUISys())
                .Add(new ThirdButtonBuildUISys())
                .Add(new ShieldUISys())
                .Add(new EffectsUISys())

            ///down
            .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new GiveTakeUISystem())
                .Add(new ScoutZoneUISys())

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
                .Add(new PickUpgUISys());


            UpdateOnlySystems
                .Add(SyncCellViewSyss)
                .Add(SyncCanvasViewSyss);
        }
    }
}