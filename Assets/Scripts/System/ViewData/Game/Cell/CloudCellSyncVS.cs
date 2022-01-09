namespace Game.Game
{
    sealed class CloudCellSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (var idx_0 in EntityCellPool.Idxs)
            {
                EntityCellVPool.CloudCellVC<CloudVC>(idx_0).EnableCloud(EntityCellPool.Cloud<HaveEffectC>(idx_0).Have);
            }
        }
    }
}