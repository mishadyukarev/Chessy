using Leopotam.Ecs;
using static MainGame;

public abstract class EntitiesManager
{
    protected StartValuesGameConfig StartValuesGameConfig => InstanceGame.StartValuesGameConfig;

    internal EcsWorld GameWorld;

    protected EntitiesManager(EcsWorld ecsWorld) => GameWorld = ecsWorld;
}
