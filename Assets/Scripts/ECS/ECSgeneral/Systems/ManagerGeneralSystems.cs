using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    internal SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager, StartSpawnManager startSpawnManager)
    {
        _updateSystems
            .Add(new InputWindowsSystem(eCSmanager), nameof(InputWindowsSystem))
            .Add(new SelectorSystem(eCSmanager, supportManager, photonManager), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(eCSmanager, supportManager), nameof(SupportVisionSystem))
            .Add(new UISystem(eCSmanager, supportManager,photonManager, startSpawnManager), nameof(UISystem));

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
}
