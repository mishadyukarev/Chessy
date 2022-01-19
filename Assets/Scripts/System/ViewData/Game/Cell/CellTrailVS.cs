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
                foreach (var dir in Keys)
                {
                    if (IsVisible(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).SetActive(Health(dir, idx_0).Have);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).Disable();
                }
            }
        }
    }
}