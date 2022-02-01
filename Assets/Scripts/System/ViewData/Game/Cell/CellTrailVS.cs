namespace Game.Game
{
    sealed class CellTrailVS : SystemViewAbstract, IEcsRunSystem
    {
        public CellTrailVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellEs.Idxs)
            {
                foreach (var dir in CellEs.TrailEs.Keys)
                {
                    if (CellEs.TrailEs.IsVisible(Es.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).SetActive(CellEs.TrailEs.Trail(dir, idx_0).HaveTrail);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).Disable();
                }
            }
        }
    }
}