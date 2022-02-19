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
                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    if (Es.CellEs(idx_0).Player(Es.CurPlayerI.Player).IsVisibleTrail)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dirT, idx_0).SetActive(Es.CellEs(idx_0).TrailHealthC(dirT).IsAlive);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dirT, idx_0).Disable();
                }
            }
        }
    }
}