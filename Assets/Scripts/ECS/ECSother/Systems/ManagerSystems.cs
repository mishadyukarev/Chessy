using Leopotam.Ecs;

public sealed class SystemsOtherManager : SystemsManager
{
    public SystemsOtherManager(EcsWorld ecsWorld) : base(ecsWorld) { }


    internal void CreateInitSystems(ECSmanager eCSmanager)
    {
        InitAndProcessInjectsSystems();
    }
}
