using static Game.Game.EntityCellCloudPool;

namespace Game.Game
{
    struct CloudCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in EntityCellPool.Idxs)
            {
                EntityCellVPool.CloudCellVC<CloudVC>(idx_0).EnableCloud(Cloud<HaveEffectC>(idx_0).Have);
            }
        }
    }
}