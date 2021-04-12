using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    public void CreateSystems(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager)
    {
        _elseSystems
            .Add(new RefresherMasterSystem(eCSmanager, supportManager), nameof(RefresherMasterSystem))
            .Add(new SetterUnitMasterSystem(eCSmanager, supportManager), nameof(SetterUnitMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager, supportManager), nameof(ShiftUnitMasterSystem))
            .Add(new BuilderCellMasterSystem(eCSmanager, supportManager), nameof(BuilderCellMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager, supportManager), nameof(AttackUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(eCSmanager, supportManager), nameof(GetterUnitMasterSystem));
    }


    public bool InvokeRunSystem(SystemMasterTypes systemType, string namedSystem)
    {
        switch (systemType)
        {
            case SystemMasterTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }
}