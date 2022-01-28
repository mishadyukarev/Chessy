namespace Game.Game
{
    struct CellTrailVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                foreach (var dir in Entities.CellEs.TrailEs.Keys)
                {
                    if (Entities.CellEs.TrailEs.IsVisible(Entities.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).SetActive(Entities.CellEs.TrailEs.Trail(dir, idx_0).Health.Have);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).Disable();
                }
            }
        }
    }
}