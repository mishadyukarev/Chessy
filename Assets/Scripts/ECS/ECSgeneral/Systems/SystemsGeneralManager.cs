using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorRunUpdateSystem = default;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        ForSelectorRunUpdateSystem = new EcsSystems(_ecsWorld);
    }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        RunUpdateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager), nameof(SupportVisionSystem))
            .Add(new SoundSystem(eCSmanager), nameof(SoundSystem))

            .Add(new UISystem(eCSmanager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager), nameof(DonerUISystem))
            .Add(new TakerUnitsUISystem(eCSmanager), nameof(TakerUnitsUISystem))
            .Add(new StandartAbilityUISystem(eCSmanager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(eCSmanager), nameof(EconomyUISystem))
            .Add(new ZoneUISystem(eCSmanager), nameof(ZoneUISystem))
            .Add(new MistakeUISystem(eCSmanager), nameof(MistakeUISystem))
            .Add(new CityUISystem(eCSmanager), nameof(CityUISystem))
            .Add(new RefreshUISystem(eCSmanager), nameof(RefreshUISystem))
            .Add(new UniqueAbilitiesUISystem(eCSmanager), nameof(UniqueAbilitiesUISystem));


        ForSelectorRunUpdateSystem
            .Add(new GetterCellSystem(eCSmanager), nameof(GetterCellSystem))
            .Add(new RaySystem(eCSmanager), nameof(RaySystem));

        this.InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();

        ForSelectorRunUpdateSystem.ProcessInjects();

        ForSelectorRunUpdateSystem.Init();
    }

}
