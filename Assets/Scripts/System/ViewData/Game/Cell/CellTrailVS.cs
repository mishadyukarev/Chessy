using static Game.Game.CellEs;
using static Game.Game.CellTrailEs;

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
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).SetActive(trailData_0.Have(item.Key));
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).Disable();
                }
            }
        }
    }
}