using Leopotam.Ecs;
using static MainGame;

internal abstract class EntitiesManager
{
    protected GameObjectPool GameObjectPool => Instance.GameObjectPool;
    protected StartValuesGameConfig StartValuesGameConfig => Instance.StartValuesGameConfig;

    internal EcsWorld GameWorld;

    protected EntitiesManager(EcsWorld ecsWorld) => GameWorld = ecsWorld;
}
