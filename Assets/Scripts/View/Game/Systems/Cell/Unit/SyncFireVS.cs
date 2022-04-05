using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    public struct SyncFireVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eV, in EntitiesModelGame e)
        {
            if (e.HaveFire(idx_0))
            {
                eV.CellEs(idx_0).FireVE.SR.Enable();
            }

            else
            {
                eV.CellEs(idx_0).FireVE.SR.Disable();
            }
        }
    }
}