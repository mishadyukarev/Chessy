using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class CloudCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in EntityPool.Idxs)
            {
                EntityVPool.CloudCellVC<CloudVC>(idx_0).EnableCloud(EntityPool.Cloud<CloudC>(idx_0).Have);
            }
        }
    }
}