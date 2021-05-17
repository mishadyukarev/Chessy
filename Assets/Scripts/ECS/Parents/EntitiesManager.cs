using Leopotam.Ecs;

public abstract class EntitiesManager
{
    protected EcsWorld _ecsWorld;
    protected EcsEntity _elseEntity;

    protected EntitiesManager(EcsWorld ecsWorld) => _ecsWorld = ecsWorld;
}
