using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    private EcsSystems _forSelectorSystem = default;
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld)
    {
        _forSelectorSystem = new EcsSystems(_ecsWorld);
    }

    internal void CreateInitSystems(ECSmanager eCSmanager, PhotonGameManager photonManager)
    {
        _updateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager, photonManager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager), nameof(SupportVisionSystem))
            .Add(new SoundSystem(eCSmanager), nameof(SoundSystem))

            .Add(new UISystem(eCSmanager, photonManager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager, photonManager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager, photonManager), nameof(DonerUISystem))
            .Add(new SelectorUnitUISystem(eCSmanager, photonManager), nameof(SelectorUnitUISystem))
            .Add(new StandartAbilityUISystem(eCSmanager, photonManager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager, photonManager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager, photonManager), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(eCSmanager), nameof(EconomyUISystem))
            .Add(new ZoneUISystem(eCSmanager), nameof(ZoneUISystem))
            .Add(new WarningUISystem(eCSmanager), nameof(WarningUISystem));


        _forSelectorSystem
            .Add(new GetterCellSystem(eCSmanager), nameof(GetterCellSystem))
            .Add(new RaySystem(eCSmanager), nameof(RaySystem));



        _multipleSystems
            .Add(new UnitPathSystem(eCSmanager), nameof(UnitPathSystem));

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
