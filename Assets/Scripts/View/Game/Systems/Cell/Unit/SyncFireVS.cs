using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    public struct SyncFireVS
    {
        bool _needActive;

        public void Sync(in byte idx_0, in EntitiesViewGame eV, in EntitiesModelGame e)
        {
            if (e.HaveFire(idx_0))
            {
                _needActive = true;
            }

            else
            {
                _needActive = false;
            }

            eV.CellEs(idx_0).FireVE.SRC.GO.SetActive(_needActive);
        }
    }
}