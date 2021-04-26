using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnManager)
    {
        _updateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager, supportGameManager, photonManager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager, supportGameManager), nameof(SupportVisionSystem))
            .Add(new AnimationAttackUnitSystem(eCSmanager, supportGameManager), nameof(AnimationAttackUnitSystem))

            .Add(new UISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(DonerUISystem))
            .Add(new SelectorUnitUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(SelectorUnitUISystem))
            .Add(new StandartAbilityUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(StandartAbilityUISystem))
            .Add(new ConditionUnitUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(ConditionUnitUISystem))
            .Add(new TheEndGameUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(eCSmanager, photonManager, startSpawnManager), nameof(BuildingUISystem))
            .Add(new WarningUISystem(eCSmanager, supportGameManager, photonManager, startSpawnManager), nameof(WarningUISystem));


        _elseSystems
            .Add(new RaySystem(eCSmanager), nameof(RaySystem))
            .Add(new UnitPathSystem(eCSmanager, supportGameManager), nameof(UnitPathSystem))
            .Add(new GetterCellSystem(eCSmanager, supportGameManager), nameof(GetterCellSystem))
            .Add(new SoundSystem(eCSmanager, startSpawnManager), nameof(SoundSystem));

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
