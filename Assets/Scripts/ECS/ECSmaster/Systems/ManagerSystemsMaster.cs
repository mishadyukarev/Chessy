using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    public void CreateInitProccessInjectsSystems(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager)
    {
        _cellSystems
            .Add(new SetterUnitMasterSystem(eCSmanager, supportManager), nameof(SetterUnitMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager, supportManager), nameof(ShiftUnitMasterSystem))
            .Add(new BuilderCellMasterSystem(eCSmanager, supportManager), nameof(BuilderCellMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager, supportManager), nameof(AttackUnitMasterSystem));

        _economySystems
            .Add(new GetterUnitMasterSystem(eCSmanager, supportManager), nameof(GetterUnitMasterSystem));

        _elseSystems
            .Add(new RefresherMasterSystem(eCSmanager, supportManager), nameof(RefresherMasterSystem));


        base.InitAndProcessInjectsSystems();
    }

    public void RunUpdate()
    {

    }

    public override void Destroy()
    {
        base.Destroy();
    }


    public bool InvokeRunSystem(SystemMasterTypes systemType, string namedSystem)
    {
        switch (systemType)
        {
            case SystemMasterTypes.Update:
                _currentSystemsForInvoke = _updateSystems;
                break;

            case SystemMasterTypes.Cell:
                _currentSystemsForInvoke = _cellSystems;
                break;

            case SystemMasterTypes.Inventor:
                _currentSystemsForInvoke = _economySystems;
                break;

            case SystemMasterTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }
}