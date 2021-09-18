using Assets.Scripts;
using Assets.Scripts.ECS.Components.View.UI.Game.General.Right;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Else.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Else.Game.General.Event;
using Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells;
using Assets.Scripts.ECS.Systems.Else.Game.General.Sync.Unit;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.UI.Game.General.RightZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.RightZone.BuildAbilit;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone;
using Leopotam.Ecs;

public sealed class GameGeneralSysManager : SystemAbstManager
{
    internal RpcSys RpcGameSys { get; private set; }
    internal EcsSystems SyncCanvasViewSyss { get; private set; }
    internal EcsSystems SyncCellViewSyss { get; private set; }
    internal EcsSystems FillAvailCellsSyss { get; private set; }

    internal GameGeneralSysManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
    {
        RpcGameSys = ECSManager.RpcView_GO.AddComponent<RpcSys>();

        SyncCellViewSyss = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitViewSys())
            .Add(new SyncCellSelUnitViewSys())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildViewSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem())
            .Add(new SyncSupportViewSystem())
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
            .Add(new ToolsDownUISys())

            ///up
            .Add(new EconomyUpUISys())

            ///center
            .Add(new SelectorTypeUISystem())
            .Add(new TheEndGameUISystem())
            .Add(new MotionCenterUISystem())
            .Add(new ReadyZoneUISystem())
            .Add(new CenterSupTextUISystem())
            .Add(new KingZoneUISys());


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
            .Add(new UnitUniqueEventSys())
            .Add(new EventUnitBuildUISys());


        InitOnlySystems
            .Add(eventExecuters);


        RunOnlySystems
            .Add(new VisibUnitsSys())

            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(soundSystems)

            .Add(FillAvailCellsSyss)

            .Add(SyncCellViewSyss)
            .Add(SyncCanvasViewSyss)

            .Add(RpcGameSys);
    }
}
