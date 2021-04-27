using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, PhotonGameManager photonManager)
    {
        _updateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager, photonManager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager), nameof(SupportVisionSystem))
            .Add(new AnimationAttackUnitSystem(eCSmanager), nameof(AnimationAttackUnitSystem))

            .Add(new UISystem(eCSmanager, photonManager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager, photonManager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager, photonManager), nameof(DonerUISystem))
            .Add(new SelectorUnitUISystem(eCSmanager, photonManager), nameof(SelectorUnitUISystem))
            .Add(new StandartAbilityUISystem(eCSmanager, photonManager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager, photonManager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager, photonManager), nameof(BuildingUISystem))
            .Add(new WarningUISystem(eCSmanager), nameof(WarningUISystem));


        _elseSystems
            .Add(new RaySystem(eCSmanager), nameof(RaySystem))
            .Add(new UnitPathSystem(eCSmanager), nameof(UnitPathSystem))
            .Add(new GetterCellSystem(eCSmanager), nameof(GetterCellSystem))
            .Add(new SoundSystem(eCSmanager), nameof(SoundSystem));

        InitAndProcessInjectsSystems();
    }

    internal bool InvokeRunSystem(SystemGeneralTypes systemGeneralType, string namedSystem)
    {
        switch (systemGeneralType)
        {
            case SystemGeneralTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
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

            case SystemGeneralTypes.TimeUpdate:
                _currentSystemsForInvoke = _timeUpdateSystems;
                break;

            case SystemGeneralTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                break;
        }

        ActiveRunSystem(isActive, namedSystem, _currentSystemsForInvoke);
    }
}
