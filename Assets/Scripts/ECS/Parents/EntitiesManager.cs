using Leopotam.Ecs;

public abstract class EntitiesManager
{
    protected EcsWorld _ecsWorld;
    protected EcsEntity _soloEntity;
    protected EcsEntity _runUpdateEntity;

    protected EntitiesManager(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;
    }
}
