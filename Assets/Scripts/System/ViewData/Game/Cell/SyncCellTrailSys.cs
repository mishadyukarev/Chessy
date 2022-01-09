namespace Game.Game
{
    public sealed class SyncCellTrailSys : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in EntityCellPool.Idxs)
            {
                ref var trailData_0 = ref EntityCellPool.Trail<TrailCellEC>(idx_0);
                ref var trailVisData_0 = ref EntityCellPool.Trail<VisibleC>(idx_0);
                ref var trailView_0 = ref EntityCellVPool.TrailCellVC<TrailVC>(idx_0);

                foreach (var item in trailData_0.DictTrail)
                {
                    if (trailVisData_0.IsVisibled(WhoseMoveC.CurPlayerI))
                    {
                        trailView_0.SetActive(item.Key, trailData_0.Have(item.Key));
                    }
                    else trailView_0.SetActive(item.Key, false);
                }
            }
        }
    }
}