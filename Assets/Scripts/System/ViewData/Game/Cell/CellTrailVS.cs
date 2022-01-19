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
                foreach (var item in DictTrail(idx_0))
                {
                    if (Trail<IsVisibleC>(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).SetActive(Have(idx_0, item.Key));
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(item.Key, idx_0).Disable();
                }
            }
        }
    }
}