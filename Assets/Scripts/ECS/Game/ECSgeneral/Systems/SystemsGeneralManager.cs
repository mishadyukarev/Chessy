using Leopotam.Ecs;

internal sealed class SystemsGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorRunUpdateSystem;
    internal EcsSystems EventSystems;

    internal void CreateSystems(EcsWorld ecsWorld)
    {
        RunUpdateSystems = new EcsSystems(ecsWorld)
            .Add(new InputSystem(), nameof(InputSystem))
            .Add(new SelectorSystem(), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new SoundEventsSystem(), nameof(SoundEventsSystem))

            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new TakerUnitsUISystem(), nameof(TakerUnitsUISystem))
            .Add(new StandartAbilityUISystem(), nameof(StandartAbilityUISystem))
            .Add(new StatsUISystem(), nameof(StatsUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new MistakeUISystem(), nameof(MistakeUISystem))
            .Add(new LeftBuildingUISystem(), nameof(LeftBuildingUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new TruceUISystem(), nameof(TruceUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem));

        ForSelectorRunUpdateSystem = new EcsSystems(ecsWorld)
            .Add(new GetterCellSystem(), nameof(GetterCellSystem))
            .Add(new RaySystem(), nameof(RaySystem));

        EventSystems = new EcsSystems(ecsWorld)
            .Add(new EventGeneralSystem(), nameof(EventGeneralSystem));
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        ForSelectorRunUpdateSystem.ProcessInjects();
        EventSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        ForSelectorRunUpdateSystem.Init();
        EventSystems.Init();
    }
}
