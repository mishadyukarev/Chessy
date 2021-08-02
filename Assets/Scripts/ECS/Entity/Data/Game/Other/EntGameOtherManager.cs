using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;


public sealed class EntGameOtherManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    internal EntGameOtherManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());
    }
}