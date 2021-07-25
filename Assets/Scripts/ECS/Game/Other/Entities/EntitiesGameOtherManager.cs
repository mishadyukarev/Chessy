using Assets.Scripts;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;


public class EntitiesGameOtherManager : EntitiesManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();

    internal EntitiesGameOtherManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity();

        FromInfoEnt_FromInfoCom.StartFill();
    }

}