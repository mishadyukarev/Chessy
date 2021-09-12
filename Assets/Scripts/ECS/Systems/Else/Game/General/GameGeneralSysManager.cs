using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Else.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Else.Game.General.Event;
using Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells;
using Assets.Scripts.ECS.Systems.Else.Game.General.Sync.Unit;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone;
using Leopotam.Ecs;

public sealed class GameGeneralSysManager : SystemAbstManager
{
    internal GameGeneralSysManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
    {
        var syncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitViewSys())
            .Add(new SyncCellSelUnitViewSys())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildViewSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem())
            .Add(new SyncSupportViewSystem())
            .Add(new FliperAndRotatorUnitSystem());


        var syncCanvasSystems = new EcsSystems(gameWorld)
           ///left
           .Add(new LeftBuildingUISystem())
           .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISystem())
            .Add(new StatsUISystem())
            .Add(new ConditionUISys())
            .Add(new UniqueAbilitiesUISystem())
            .Add(new BuildRighUISystem())

            ///down
            .Add(new DonerUISystem())
            .Add(new GetterUnitsUISystem())
            .Add(new GiveTakeUISystem())
            .Add(new SyncToolsDownUISystem())

            ///up
            .Add(new SyncEconomyUpUISystem())

            ///center
            .Add(new SelectorTypeUISystem())
            .Add(new TheEndGameUISystem())
            .Add(new MotionCenterUISystem())
            .Add(new ReadyZoneUISystem())
            .Add(new CenterSupTextUISystem())
            .Add(new KingZoneUISys());


        var fillAvailCells = new EcsSystems(gameWorld)
            .Add(new ClearAvailCellsSys())
            .Add(new FillCellsForAttackKingSys())
            .Add(new FillCellsForAttackPawnSys())
            .Add(new FillCellsForAttackRookSys())
            .Add(new FillCellsForAttackBishopSys())
            .Add(new FillCellsForSetUnitSys())
            .Add(new FillCellsForShiftSys())
            .Add(new FillCellsArsonSys());


        var executersEvents = new EcsSystems(gameWorld)
            .Add(new EventsGameSys())
            .Add(new EnventRightBuildZoneSys());


        InitOnlySystems
            .Add(executersEvents);


        RunOnlySystems
            .Add(new VisibilityUnitsMasterSystem())

            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(fillAvailCells)

            .Add(syncCellVisionSystems)
            .Add(syncCanvasSystems)

            .Add(new SoundSystem());
    }
}
