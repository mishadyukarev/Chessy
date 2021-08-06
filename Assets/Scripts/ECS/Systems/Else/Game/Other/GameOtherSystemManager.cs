using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class GameOtherSystemManager : SystemAbstManager
{
    internal GameOtherSystemManager(EcsWorld gameWorld) : base(gameWorld)
    {
        gameWorld.NewEntity().Replace(new FromInfoComponent());
    }


    internal override void Init()
    {
        base.Init();

    }
}
