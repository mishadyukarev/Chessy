using Assets.Scripts;
using Leopotam.Ecs;

public sealed class SysGameOtherManager : SystemAbstManager
{
    internal SysGameOtherManager(EcsWorld gameWorld) : base(gameWorld)
    {

    }

    //internal void CreateSystems(EcsWorld ecsWorld)
    //{
    //    RunUpdateSystems = new EcsSystems(ecsWorld);
    //}


    internal override void Init()
    {
        base.Init();

    }
}
