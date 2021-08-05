using Assets.Scripts;
using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers;
using Assets.Scripts.ECS.Entities.Game.General.UI.Vis.Containers;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.System.Data.Common;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Game.General.UI.View;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.General.UI.RunUpdate.CenterZone;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo;
using Assets.Scripts.Workers.Game.UI.Vis.Up;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class GameGeneralSystemManager : SystemAbstManager
{

    private EcsSystems _eventSystems;

    internal static EcsSystems SyncCellVisionSystems { get; private set; }

    internal GameGeneralSystemManager(EcsWorld gameWorld, EcsWorld commonWorld) : base(gameWorld)
    {
        UpdateSystems
            .Add(new MainGameSystem(commonWorld))

            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new FliperAndRotatorUnitSystem(), nameof(FliperAndRotatorUnitSystem))

            .Add(new SoundEventsSystem(), nameof(SoundEventsSystem))

            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new GetterUnitsUISystem(), nameof(GetterUnitsUISystem))
            .Add(new ConditionAbilitiesUISystem(), nameof(ConditionAbilitiesUISystem))
            .Add(new StatsUISystem(), nameof(StatsUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new LeftBuildingUISystem(), nameof(LeftBuildingUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem))
            .Add(new MistakeBarUISystem())
            .Add(new MistakeUISystem())
            .Add(new CenterSupTextUISystem())

            .Add(new CellEnvrDataSystem())
            .Add(new CellFireDataSystem())
            .Add(new CellUnitsDataSystem())
            .Add(new CellBuildDataSystem())

            .Add(new StartSpawnCellsViewSystem())
            .Add(new CellBlocksViewSystem())
            .Add(new CellUnitViewSystem())
            .Add(new CellSupVisBarsViewSystem())
            .Add(new CellSupViewSystem())
            .Add(new CellFireViewSystem())
            .Add(new CellBuildViewSystem())
            .Add(new CellEnvViewSystem())
            .Add(new CellViewSystem());


        _eventSystems = new EcsSystems(gameWorld)
            .Add(new EventGeneralSystem(), nameof(EventGeneralSystem));


        SyncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitVisSystem())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildingsVisSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem());


        new SoundGameGeneralViewWorker(new SoundElseViewContainer(gameWorld));

        new MistakeDataUIContainer(gameWorld);
        new ResourcesUIDataContainer(new ResourcesDataUIContainer(gameWorld));
    }

    internal override void Init()
    {
        base.Init();

        _eventSystems.Init();
        SyncCellVisionSystems.Init();
    }
}
