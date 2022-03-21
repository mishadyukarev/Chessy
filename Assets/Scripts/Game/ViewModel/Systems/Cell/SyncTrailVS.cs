namespace Chessy.Game
{
    sealed class SyncTrailVS
    {
        public static void Sync(in byte idx_0, in EntitiesView eV, in Chessy.Game.Entity.Model.EntitiesModel e)
        {
            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                if (e.CellEs(idx_0).Player(e.CurPlayerITC.Player).IsVisibleTrail)
                {
                    eV.CellEs(idx_0).TrailCellVC(dirT).SetActive(e.CellEs(idx_0).TrailHealthC(dirT).IsAlive);
                }
                else eV.CellEs(idx_0).TrailCellVC(dirT).Disable();
            }
        }
    }
}