using Leopotam.Ecs;

internal sealed class SystemsOtherManager : SystemsManager
{
    internal void CreateSystems(EcsWorld ecsWorld)
    {
        RunUpdateSystems = new EcsSystems(ecsWorld);
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

    }

    internal override void Init()
    {
        base.Init();

    }
}
