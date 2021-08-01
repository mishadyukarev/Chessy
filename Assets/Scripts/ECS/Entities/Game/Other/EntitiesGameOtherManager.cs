using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;


public sealed class EntitiesGameOtherManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    internal EntitiesGameOtherManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());
    }
}