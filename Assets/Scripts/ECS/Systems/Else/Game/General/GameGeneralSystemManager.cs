using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Else.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Else.Game.General.Event;
using Assets.Scripts.ECS.Systems.Else.Game.General.FillAvailCells;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone;
using Leopotam.Ecs;
using System;
using UnityEngine;

public sealed class GameGeneralSystemManager : SystemAbstManager
{
    internal GameGeneralSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
    {
        var syncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitViewSystem())
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
            .Add(new ConditionAbilitiesUISystem())
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
            .Add(new FillCellsForShiftSys());


        InitOnlySystems
            .Add(new EventsGameSys());


        RunOnlySystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(fillAvailCells)

            .Add(syncCellVisionSystems)
            .Add(syncCanvasSystems)

            .Add(new SoundSystem())

            .Add(new VisibilityUnitsMasterSystem());


        InitRunSystems
            .Add(new DinamicEventsGameSys());
    }
}
