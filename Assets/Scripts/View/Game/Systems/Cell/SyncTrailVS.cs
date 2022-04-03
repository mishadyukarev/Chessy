using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    public struct SyncTrailVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eVGame, in EntitiesModelGame eMGame)
        {
            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                if (eMGame.TrailVisibleC(idx_0).IsVisible(eMGame.CurPlayerITC.PlayerT))
                {
                    eVGame.CellEs(idx_0).TrailCellVC(dirT).SetActive(eMGame.HealthTrail(idx_0).IsAlive(dirT));
                }
                else eVGame.CellEs(idx_0).TrailCellVC(dirT).Disable();
            }
        }
    }
}