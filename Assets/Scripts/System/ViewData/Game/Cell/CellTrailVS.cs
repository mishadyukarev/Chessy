namespace Game.Game
{
    sealed class CellTrailVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellTrailVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                foreach (var dir in TrailEs(idx_0).Keys)
                {
                    if (TrailEs(idx_0).IsVisible(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).SetActive(CellEs(idx_0).TrailEs.Trail(dir).HaveTrail);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).Disable();
                }
            }
        }
    }
}