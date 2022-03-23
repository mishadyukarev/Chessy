using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    public struct SyncFireVS
    {
        public void Sync(in byte idx_0, in EntitiesViewGame eV, in EntitiesModelGame e)
        {
            if (e.EffectEs(idx_0).HaveFire)
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