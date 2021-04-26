using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateInitSystems(ECSmanager eCSmanager, SupportGameManager supportGameManager, PhotonGameManager photonGameManager)
    {
        _elseSystems
            .Add(new RefresherMasterSystem(eCSmanager, supportGameManager), nameof(RefresherMasterSystem))
            .Add(new SetterUnitMasterSystem(eCSmanager, supportGameManager), nameof(SetterUnitMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager, supportGameManager, photonGameManager), nameof(ShiftUnitMasterSystem))
            .Add(new BuilderCellMasterSystem(eCSmanager, supportGameManager), nameof(BuilderCellMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager, supportGameManager, photonGameManager), nameof(AttackUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(eCSmanager, supportGameManager), nameof(GetterUnitMasterSystem))
            .Add(new ProtecterUnitMasterSystem(eCSmanager, supportGameManager), nameof(ProtecterUnitMasterSystem));

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