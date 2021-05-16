using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    private EcsSystems _forSelectorSystem = default;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        _forSelectorSystem = new EcsSystems(_ecsWorld);
    }

    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        _updateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager), nameof(SupportVisionSystem))
            .Add(new SoundSystem(eCSmanager), nameof(SoundSystem))

            .Add(new UISystem(eCSmanager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager), nameof(DonerUISystem))
            .Add(new TakerUnitUISystem(eCSmanager), nameof(TakerUnitUISystem))
            .Add(new StandartAbilityUISystem(eCSmanager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(eCSmanager), nameof(EconomyUISystem))
            .Add(new ZoneUISystem(eCSmanager), nameof(ZoneUISystem))
            .Add(new WarningUISystem(eCSmanager), nameof(WarningUISystem))
            .Add(new CityUISystem(eCSmanager), nameof(CityUISystem))
            .Add(new RefreshUISystem(eCSmanager), nameof(RefreshUISystem));


        _forSelectorSystem
            .Add(new GetterCellSystem(eCSmanager), nameof(GetterCellSystem))
            .Add(new RaySystem(eCSmanager), nameof(RaySystem));

        this.InitAndProcessInjectsSystems();
    }

    internal override void InitAndProcessInjectsSystems()
    {
        base.InitAndProcessInjectsSystems();

        _forSelectorSystem.ProcessInjects();

        _forSelectorSystem.Init();
    }

    internal bool InvokeRunSystem(SystemGeneralTypes systemGeneralType, string namedSystem)
    {
        switch (systemGeneralType)
        {
            case SystemGeneralTypes.Multiple:
                _currentSystemsForInvoke = _multipleSystems;
                break;

            case SystemGeneralTypes.ForSelector:
                _currentSystemsForInvoke = _forSelectorSystem;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

    internal void ActiveRunSystem(bool isActive, SystemGeneralTypes systemGeneralType, string namedSystem)
    {
        switch (systemGeneralType)
        {
            case SystemGeneralTypes.Update:
                _currentSystemsForInvoke = _updateSystems;
                break;
        }

        ActiveRunSystem(isActive, namedSystem, _currentSystemsForInvoke);
    }
}
