using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, SupportGameManager supportManager, PhotonGameManager photonManager, StartSpawnGameManager startSpawnManager)
    {
        _updateSystems
            .Add(new InputSystem(eCSmanager), nameof(InputSystem))
            .Add(new SelectorSystem(eCSmanager, supportManager, photonManager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager, supportManager), nameof(SupportVisionSystem))

            .Add(new UISystem(eCSmanager, supportManager, photonManager, startSpawnManager), nameof(UISystem))
            .Add(new ReadyUISystem(eCSmanager, supportManager, photonManager, startSpawnManager), nameof(ReadyUISystem))
            .Add(new DonerUISystem(eCSmanager, supportManager, photonManager, startSpawnManager), nameof(DonerUISystem))
            .Add(new SelectorUnitUISystem(eCSmanager, supportManager, photonManager, startSpawnManager), nameof(SelectorUnitUISystem))
            .Add(new WarningUISystem(eCSmanager, supportManager, photonManager, startSpawnManager), nameof(WarningUISystem));


        _elseSystems
            .Add(new RaySystem(eCSmanager), nameof(RaySystem))
            .Add(new UnitPathSystem(eCSmanager, supportManager), nameof(UnitPathSystem))
            .Add(new GetterCellSystem(eCSmanager, supportManager), nameof(GetterCellSystem))
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

            case SystemGeneralTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                break;
        }

        ActiveRunSystem(isActive, namedSystem, _currentSystemsForInvoke);
    }
}
