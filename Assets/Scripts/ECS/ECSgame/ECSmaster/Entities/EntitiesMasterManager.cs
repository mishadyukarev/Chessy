using Leopotam.Ecs;

internal sealed class EntitiesMasterManager : EntitiesManager
{
    private EcsEntity _masterRPCEntity;
    internal ref RPCMasterComponent RPCMasterEnt_RPCMasterCom => ref _masterRPCEntity.Get<RPCMasterComponent>();


    internal EntitiesMasterManager(EcsWorld gameWorld, ECSmanager eCSmanager)// : base(gameWorld)
    {
        var eGM = eCSmanager.EntitiesGeneralManager;

        _masterRPCEntity = gameWorld.NewEntity();

        _masterRPCEntity.Replace(new RPCMasterComponent());
    }
}
