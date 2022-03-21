namespace Chessy.Game
{
    static class SyncFireVS
    {
        public static void Sync(in byte idx_0, in EntitiesView eV, in Chessy.Game.Entity.Model.EntitiesModel e)
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