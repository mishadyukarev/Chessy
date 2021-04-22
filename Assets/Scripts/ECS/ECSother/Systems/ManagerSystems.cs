using Leopotam.Ecs;

public sealed class SystemsOtherManager : SystemsManager
{
    public SystemsOtherManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, SupportGameManager supportManager, PhotonManager photonManager)
    {
        InitAndProcessInjectsSystems();
    }


    internal bool InvokeRunSystem(SystemOtherTypes systemOtherType, string namedSystem)
    {
        switch (systemOtherType)
        {
            case SystemOtherTypes.Update:
                _currentSystemsForInvoke = _updateSystems;
                break;

            case SystemOtherTypes.Else:
                _currentSystemsForInvoke = _elseSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

}
