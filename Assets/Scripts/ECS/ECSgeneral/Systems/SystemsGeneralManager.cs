using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorSystem = default;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        ForSelectorSystem = new EcsSystems(_ecsWorld);
    }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        RunUpdateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager), nameof(SupportVisionSystem))
            .Add(new SoundSystem(eCSmanager), nameof(SoundSystem))

            .Add(new UISystem(eCSmanager), nameof(UISystem))
            .Add(new ReadySystem(eCSmanager), nameof(ReadySystem))
            .Add(new DonerSystem(eCSmanager), nameof(DonerSystem))
            .Add(new TakerUnitsSystem(eCSmanager), nameof(TakerUnitsSystem))
            .Add(new StandartAbilityUISystem(eCSmanager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager), nameof(BuildingUISystem))
            .Add(new EconomySystem(eCSmanager), nameof(EconomySystem))
            .Add(new ZoneUISystem(eCSmanager), nameof(ZoneUISystem))
            .Add(new MistakeUpdateSystem(eCSmanager), nameof(MistakeUpdateSystem))
            .Add(new CityUISystem(eCSmanager), nameof(CityUISystem))
            .Add(new RefreshUISystem(eCSmanager), nameof(RefreshUISystem));


        ForSelectorSystem
            .Add(new GetterCellSystem(eCSmanager), nameof(GetterCellSystem))
            .Add(new RaySystem(eCSmanager), nameof(RaySystem));


        SoloSystems
            .Add(new MistakeUpdateSystem(eCSmanager), nameof(MistakeUpdateSystem));

        this.InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();

        ForSelectorSystem.ProcessInjects();

        ForSelectorSystem.Init();
    }

}
