using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    public struct SyncTrailVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eVGame, in EntitiesModelGame eMGame)
        {
            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                if (eMGame.CellEs(idx_0).Player(eMGame.CurPlayerITC.Player).IsVisibleTrail)
                {
                    eVGame.CellEs(idx_0).TrailCellVC(dirT).SetActive(eMGame.CellEs(idx_0).TrailHealthC(dirT).IsAlive);
                }
                else eVGame.CellEs(idx_0).TrailCellVC(dirT).Disable();
            }
        }
    }
}