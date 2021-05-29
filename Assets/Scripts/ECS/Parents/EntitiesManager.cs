using Leopotam.Ecs;
using static MainGame;

internal abstract class EntitiesManager
{
    protected ObjectPool ObjectPool => Instance.ObjectPool;
    protected StartValuesGameConfig StartValuesGameConfig => Instance.StartValuesGameConfig;

    internal EcsWorld GameWorld;

    protected EntitiesManager(EcsWorld ecsWorld) => GameWorld = ecsWorld;
}
