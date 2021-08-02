using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;


public sealed class EntDataGameOtherElseManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    internal EntDataGameOtherElseManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity()
            .Replace(new FromInfoComponent());
    }
}