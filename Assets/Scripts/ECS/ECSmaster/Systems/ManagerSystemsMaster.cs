using Leopotam.Ecs;

public sealed class SystemsMasterManager : SystemsManager
{
    public SystemsMasterManager(EcsWorld ecsWorld) : base(ecsWorld) { }

    internal void CreateInitSystems(ECSmanager eCSmanager,  PhotonGameManager photonGameManager)
    {
        _soloSystems
            .Add(new RefresherMasterSystem(eCSmanager), nameof(RefresherMasterSystem))
            .Add(new SetterUnitMasterSystem(eCSmanager), nameof(SetterUnitMasterSystem))
            .Add(new ShiftUnitMasterSystem(eCSmanager, photonGameManager), nameof(ShiftUnitMasterSystem))
            .Add(new BuilderCellMasterSystem(eCSmanager), nameof(BuilderCellMasterSystem))
            .Add(new AttackUnitMasterSystem(eCSmanager, photonGameManager), nameof(AttackUnitMasterSystem))
            .Add(new GetterUnitMasterSystem(eCSmanager), nameof(GetterUnitMasterSystem))
            .Add(new ProtecterUnitMasterSystem(eCSmanager), nameof(ProtecterUnitMasterSystem));

        InitAndProcessInjectsSystems();
    }


    public bool InvokeRunSystem(SystemMasterTypes systemType, string namedSystem)
    {
        switch (systemType)
        {
            case SystemMasterTypes.Else:
                _currentSystemsForInvoke = _soloSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }
}