using Leopotam.Ecs;

public abstract class EntitiesManager
{
    internal EcsWorld GameWorld;
    protected EcsEntity _elseEntity;

    protected EntitiesManager(EcsWorld ecsWorld) => GameWorld = ecsWorld;
}
