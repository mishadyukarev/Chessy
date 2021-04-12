using Leopotam.Ecs;

public class SystemsGeneralManager : SystemsManager
{
    public SystemsGeneralManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    public void CreateInitProccessInjectsSystems(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager)
    {
        _cellSystems
            .Add(new UnitPathSystem(eCSmanager, supportManager), nameof(UnitPathSystem))
            .Add(new GetterCellSystem(eCSmanager, supportManager), nameof(GetterCellSystem))
            .Add(new SupportVisionSystem(eCSmanager, supportManager), nameof(SupportVisionSystem));

        _updateSystems
            .Add(new InputWindowsSystem(eCSmanager), nameof(InputWindowsSystem))
            .Add(new SelectorSystem(eCSmanager, supportManager, photonManager), nameof(SelectorSystem))
            .Add(new UIsystem(eCSmanager, supportManager), nameof(UIsystem))
            .Add(new ButtonSystem(eCSmanager, supportManager, photonManager), nameof(ButtonSystem));

        _elseSystems
            .Add(new RaySystem(eCSmanager), nameof(RaySystem));





        base.InitAndProcessInjectsSystems();
    }

    public void RunUpdate()
    {
        InvokeRunSystem(SystemGeneralTypes.Updates, nameof(InputWindowsSystem));
        InvokeRunSystem(SystemGeneralTypes.Updates, nameof(SelectorSystem));
        InvokeRunSystem(SystemGeneralTypes.Updates, nameof(UIsystem));
        InvokeRunSystem(SystemGeneralTypes.Updates, nameof(ButtonSystem));
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    internal bool InvokeRunSystem(SystemGeneralTypes systemGeneralType, string namedSystem)
    {
        switch (systemGeneralType)
        {
            case SystemGeneralTypes.Updates:
                _currentSystemsForInvoke = _updateSystems;
                break;

            case SystemGeneralTypes.Cell:
                _currentSystemsForInvoke = _cellSystems;
                break;

            case SystemGeneralTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

}
