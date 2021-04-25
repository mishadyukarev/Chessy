using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateInitSystems(ECSmanager eCSmanager, SupportGameManager supportManager, PhotonGameManager photonGameManager)
    {
        _elseSystems
            .Add(new RefresherMasterSystem(eCSmanager, supportManager), nameof(RefresherMasterSystem))
            .Add(new SetterUnitMasterSystem(eCSmanager, supportManager), nameof(SetterUnitMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager, supportManager), nameof(ShiftUnitMasterSystem))
            .Add(new BuilderCellMasterSystem(eCSmanager, supportManager), nameof(BuilderCellMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager, supportManager, photonGameManager), nameof(AttackUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(eCSmanager, supportManager), nameof(GetterUnitMasterSystem))
            .Add(new ProtecterUnitMasterSystem(eCSmanager, supportManager), nameof(ProtecterUnitMasterSystem));

        InitAndProcessInjectsSystems();
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