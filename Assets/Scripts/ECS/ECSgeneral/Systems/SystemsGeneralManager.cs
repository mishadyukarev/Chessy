using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorRunUpdateSystem = default;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        ForSelectorRunUpdateSystem = new EcsSystems(ecsWorld);
    }

    internal void CreateSystems(ECSmanagerGame eCSmanager, CellManager cellManager, Names names)
    {
        RunUpdateSystems
            .Add(new InputSystem(), nameof(InputSystem))
            .Add(new SelectorSystem(), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new SoundSystem(), nameof(SoundSystem))

            .Add(new UISystem(), nameof(UISystem))
            .Add(new ReadyUISystem(), nameof(ReadyUISystem))
            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new TakerUnitsUISystem(), nameof(TakerUnitsUISystem))
            .Add(new StandartAbilityUISystem(), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new ZoneUISystem(), nameof(ZoneUISystem))
            .Add(new MistakeUISystem(), nameof(MistakeUISystem))
            .Add(new CityUISystem(), nameof(CityUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new TruceUISystem(), nameof(TruceUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem));


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
