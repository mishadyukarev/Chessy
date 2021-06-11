using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorRunUpdateSystem;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        ForSelectorRunUpdateSystem = new EcsSystems(ecsWorld);
    }

    internal void CreateSystems()
    {
        RunUpdateSystems
            .Add(new InputSystem(), nameof(InputSystem))
            .Add(new SelectorSystem(), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new SoundEventsSystem(), nameof(SoundEventsSystem))

            .Add(new UISystem(), nameof(UISystem))
            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new TakerUnitsUISystem(), nameof(TakerUnitsUISystem))
            .Add(new StandartAbilityUISystem(), nameof(StandartAbilityUISystem))
            .Add(new StatsUISystem(), nameof(StatsUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new MistakeUISystem(), nameof(MistakeUISystem))
            .Add(new CityUISystem(), nameof(CityUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new TruceUISystem(), nameof(TruceUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem));


        ForSelectorRunUpdateSystem
            .Add(new GetterCellSystem(), nameof(GetterCellSystem))
            .Add(new RaySystem(), nameof(RaySystem));
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();

        ForSelectorRunUpdateSystem.ProcessInjects();

        ForSelectorRunUpdateSystem.Init();
    }
}
