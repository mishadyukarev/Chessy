using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class GameOtherSystemManager : SystemAbstManager
{
    internal GameOtherSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        gameWorld.NewEntity().Replace(new FromInfoComponent());
    }
}
