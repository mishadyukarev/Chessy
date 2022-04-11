using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    sealed class SyncTrailVS
    {
        readonly bool[] _needActive = new bool[(byte)DirectTypes.End];


        public void Sync(in byte cell_start, in EntitiesViewGame eVGame, in EntitiesModelGame eMGame)
        {
            for (var i = 0; i < _needActive.Length; i++) _needActive[i] = false;

            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                if (eMGame.TrailVisibleC(cell_start).IsVisible(eMGame.CurPlayerITC.PlayerT))
                {
                    _needActive[(byte)dirT] = eMGame.HealthTrail(cell_start).IsAlive(dirT);
                }

                eVGame.CellEs(cell_start).TrailCellVC(dirT).GO.SetActive(_needActive[(byte)dirT]);
            }
        }
    }
}