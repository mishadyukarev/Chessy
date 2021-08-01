using Assets.Scripts;
using Leopotam.Ecs;

public sealed class SystemGameOtherManager : SystemsManager
{
    internal SystemGameOtherManager(EcsWorld gameWorld) : base(gameWorld)
    {

    }

    //internal void CreateSystems(EcsWorld ecsWorld)
    //{
    //    RunUpdateSystems = new EcsSystems(ecsWorld);
    //}

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

    }

    internal override void Init()
    {
        base.Init();

    }
}
