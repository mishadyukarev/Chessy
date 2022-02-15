namespace Game.Game
{
    sealed class CellTrailVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellTrailVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                foreach (var dir in Es.TrailEs(idx_0).Keys)
                {
                    if (Es.TrailEs(idx_0).IsVisible(Es.WhoseMovePlayerTC.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).SetActive(Es.CellEs(idx_0).TrailEs.Trail(dir).HealthC.IsAlive);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dir, idx_0).Disable();
                }
            }
        }
    }
}