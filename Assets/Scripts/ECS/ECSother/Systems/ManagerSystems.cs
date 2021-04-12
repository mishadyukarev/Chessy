using Leopotam.Ecs;

public sealed class SystemsOtherManager : SystemsManager
{
    public SystemsOtherManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    public void CreateInitProccessInjectsSystems(ECSmanager eCSmanager, SupportManager supportManager, PhotonManager photonManager)
    {
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
            case SystemMasterTypes.UpdatesOther:
                _currentSystemsForInvoke = _updateSystems;
                break;

            case SystemMasterTypes.CellOther:
                _currentSystemsForInvoke = _cellSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

}
