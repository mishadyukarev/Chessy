using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class GameGeneralSysManager : SystemAbstManager, IDisposable
    {
        internal RpcSys RpcGameSys { get; private set; }
        internal EcsSystems SyncCanvasViewSyss { get; private set; }
        internal EcsSystems SyncCellViewSyss { get; private set; }
        internal EcsSystems FillAvailCellsSyss { get; private set; }

        public GameGeneralSysManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
        {
            new PhotonRpcViewGameCom(true);

            RpcGameSys = PhotonRpcViewGameCom.RpcSys;

            SyncCellViewSyss = new EcsSystems(gameWorld)
                .Add(new VisibUnitsBuildsSys())
                .Add(new SyncCellUnitViewSys())
                .Add(new SyncCellSelUnitViewSys())
                .Add(new SyncCellUnitSupVisSystem())
                .Add(new SyncCellBuildViewSystem())
                .Add(new SyncCellEnvirsVisSystem())
                .Add(new SyncCellEffectsVisSystem())
                .Add(new SyncSupportViewSystem())
                .Add(new CellWeatherViewSys())
                .Add(new FliperAndRotatorUnitSystem());


            var soundSystems = new EcsSystems(gameWorld)
                .Add(new SoundSystem());


            SyncCanvasViewSyss = new EcsSystems(gameWorld)
           ///left
            .Add(new BuildZoneUISys())
               .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISys())
                .Add(new RightUnitInfoUISys())
                .Add(new StatsUISystem())
                .Add(new ProtectUISys())
                .Add(new RelaxUISys())
                .Add(new UniqueAbilitUISys())
                .Add(new FirstButtonBuildUISys())
                .Add(new SecButtonBuildUISys())
                .Add(new ThirdButtonBuildUISys())

            ///down
            .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new GiveTakeUISystem())
                .Add(new HeroZoneUISys())

            ///up
            .Add(new EconomyUpUISys())
            .Add(new WindUISys())

            ///center
            .Add(new SelectorTypeUISystem())
                .Add(new TheEndGameUISystem())
                .Add(new MotionCenterUISystem())
                .Add(new ReadyZoneUISystem())
                .Add(new MistakeUISys())
                .Add(new KingZoneUISys())
                .Add(new FriendZoneUISys())
                .Add(new ActiveHitUISys());


            FillAvailCellsSyss = new EcsSystems(gameWorld)
                .Add(new ClearAvailCellsSys())
                .Add(new FillCellsForAttackKingSys())
                .Add(new FillCellsForAttackPawnSys())
                .Add(new FillCellsForAttackRookSys())
                .Add(new FillCellsForAttackBishopSys())
                .Add(new FillCellsForSetUnitSys())
                .Add(new FillCellsForShiftSys())
                .Add(new FillCellsArsonSys());


            var eventExecuters = new EcsSystems(gameWorld)
                .Add(new EventsGameSys())
                .Add(new EventDownSys())
                .Add(new UnitUniqueEventSys())
                .Add(new EventUnitBuildUISys());


            InitOnlySystems
                .Add(eventExecuters);


            UpdateOnlySystems
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorSystem())

                .Add(soundSystems)

                .Add(FillAvailCellsSyss)

                .Add(SyncCellViewSyss)
                .Add(SyncCanvasViewSyss)

                .Add(RpcGameSys);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(PhotonRpcViewGameCom.RpcView_GO);
        }
    }
}