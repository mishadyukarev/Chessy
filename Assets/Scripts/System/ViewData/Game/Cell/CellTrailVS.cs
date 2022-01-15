using static Game.Game.CellE;
using static Game.Game.EntityCellTrailPool;

namespace Game.Game
{
    struct CellTrailVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var trailData_0 = ref Trail<TrailCellEC>(idx_0);

                foreach (var item in trailData_0.DictTrail)
                {
                    if (Trail<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
                    {
                        CellTrailVE.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).SetActive(trailData_0.Have(item.Key));
                    }
                    else CellTrailVE.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).Disable();
                }
            }
        }
    }
}