using Assets.Scripts;
using Leopotam.Ecs;

public sealed class GameOtherSystemManager : SystemAbstManager
{
    public GameOtherSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld, allGameSystems)
    {

    }
}
