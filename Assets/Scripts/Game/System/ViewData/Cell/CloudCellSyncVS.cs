using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class CloudCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in EntityCellPool.Idxs)
            {
                EntityCellVPool.CloudCellVC<CloudVC>(idx_0).EnableCloud(EntityCellPool.Cloud<CloudC>(idx_0).Have);
            }
        }
    }
}