using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellTrailPool;

namespace Game.Game
{
    struct SyncCellTrailSys : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var trailData_0 = ref Trail<TrailCellEC>(idx_0);
                ref var trailView_0 = ref EntityCellVPool.TrailCellVC<TrailVC>(idx_0);

                foreach (var item in trailData_0.DictTrail)
                {
                    if (Trail<VisibledC>(WhoseMoveC.CurPlayerI, idx_0).IsVisibled)
                    {
                        trailView_0.SetActive(item.Key, trailData_0.Have(item.Key));
                    }
                    else trailView_0.SetActive(item.Key, false);
                }
            }
        }
    }
}