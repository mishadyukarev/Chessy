using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class TryDestroyAdultForestS_M : SystemModel
    {
        internal TryDestroyAdultForestS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryDestroy(in byte cellIdx)
        {
            if (eMG.AdultForestC(cellIdx).HaveAnyResources)
            {
                eMG.AdultForestC(cellIdx).Resources = 0;

                sMG.MasterSs.TrySeedNewYoungForestS.TrySeed(cellIdx);
                sMG.MasterSs.TryClearAllTrailsOnCellS.TryDestroy(cellIdx);
            }
        }
    }
}