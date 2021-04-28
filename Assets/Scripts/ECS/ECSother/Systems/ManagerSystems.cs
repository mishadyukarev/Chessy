using Leopotam.Ecs;

public sealed class SystemsOtherManager : SystemsManager
{
    public SystemsOtherManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager, PhotonGameManager photonManager)
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
                _currentSystemsForInvoke = _soloSystems;
                break;

            default:
                return false;
        }

        return TryInvokeRunSystem(namedSystem, _currentSystemsForInvoke);
    }

}
