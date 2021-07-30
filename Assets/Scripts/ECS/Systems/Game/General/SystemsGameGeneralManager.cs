using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.RunUpdate.UI.DownZone;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.General.UI.RunUpdate.CenterZone;
using Leopotam.Ecs;

public sealed class SystemsGameGeneralManager : SystemsManager
{
    private EcsSystems _forSelectorSystem;
    private EcsSystems _eventSystems;

    internal EcsSystems SyncCellVisionSystems { get; private set; }

    internal SystemsGameGeneralManager(EcsWorld gameWorld) : base(gameWorld)
    {
        RunUpdateSystems
            .Add(new InputSystem(), nameof(InputSystem))
            .Add(new RaySystem(), nameof(RaySystem))
            .Add(new SelectorSystem(), nameof(SelectorSystem))

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
            .Add(new MistakeUISystem(), nameof(MistakeUISystem))
            .Add(new LeftBuildingUISystem(), nameof(LeftBuildingUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem))
            .Add(new FinderIdleUnitUISystem(), nameof(FinderIdleUnitUISystem))
            .Add(new MistakeBarUISystem());

        _forSelectorSystem = new EcsSystems(gameWorld)
            .Add(new RaySystem(), nameof(RaySystem));

        _eventSystems = new EcsSystems(gameWorld)
            .Add(new EventGeneralSystem(), nameof(EventGeneralSystem));

        SyncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitVisSystem())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildingsVisSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem());

        StartFillSystems
            .Add(new StartFillSystem());
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        _forSelectorSystem.ProcessInjects();
        _eventSystems.ProcessInjects();
        SyncCellVisionSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        _forSelectorSystem.Init();
        _eventSystems.Init();
        SyncCellVisionSystems.Init();
    }
}
