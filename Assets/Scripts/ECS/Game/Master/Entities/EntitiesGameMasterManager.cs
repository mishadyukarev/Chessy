using Assets.Scripts;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.Components;
using Leopotam.Ecs;

public sealed class EntitiesGameMasterManager : EntitiesManager
{
    private EcsEntity _fromInfoEnt;
    internal ref FromInfoComponent FromInfoEnt_FromInfoCom => ref _fromInfoEnt.Get<FromInfoComponent>();


    private EcsEntity _masterRPCEntity;
    internal ref RPCMasterComponent RPCMasterEnt_RPCMasterCom => ref _masterRPCEntity.Get<RPCMasterComponent>();


    private EcsEntity _fireEnt;
    internal ref FromToXyComponent FireEnt_FromToXyCom => ref _fireEnt.Get<FromToXyComponent>();


    public EntitiesGameMasterManager(EcsWorld gameWorld)
    {
        _fromInfoEnt = gameWorld.NewEntity();
        _masterRPCEntity = gameWorld.NewEntity();
        _fireEnt = gameWorld.NewEntity();

        FromInfoEnt_FromInfoCom.StartFill();
        _masterRPCEntity.Replace(new RPCMasterComponent());
        FireEnt_FromToXyCom.StartFill();
    }
}
