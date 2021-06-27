using Assets.Scripts;
using Leopotam.Ecs;

public sealed class EntitiesGameMasterManager : EntitiesManager
{
    private EcsEntity _masterRPCEntity;
    internal ref RPCMasterComponent RPCMasterEnt_RPCMasterCom => ref _masterRPCEntity.Get<RPCMasterComponent>();


    public EntitiesGameMasterManager(EcsWorld gameWorld)
    {
        _masterRPCEntity = gameWorld.NewEntity();

        _masterRPCEntity.Replace(new RPCMasterComponent());
    }
}
