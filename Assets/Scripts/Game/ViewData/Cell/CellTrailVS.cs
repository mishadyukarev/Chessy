namespace Game.Game
{
    sealed class CellTrailVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellTrailVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    if (E.CellEs(idx_0).Player(E.CurPlayerITC.Player).IsVisibleTrail)
                    {
                        CellTrailVEs.TrailCellVC<SpriteRendererVC>(dirT, idx_0).SetActive(E.CellEs(idx_0).TrailHealthC(dirT).IsAlive);
                    }
                    else CellTrailVEs.TrailCellVC<SpriteRendererVC>(dirT, idx_0).Disable();
                }
            }
        }
    }
}