using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class TryTakeAdultForestResourcesS_M : SystemModel
    {
        internal TryTakeAdultForestResourcesS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void TryTake(in float taking, in byte cellIdx)
        {
            if (eMG.AdultForestC(cellIdx).HaveAnyResources)
            {
                eMG.AdultForestC(cellIdx).Resources -= taking;

                if (!eMG.AdultForestC(cellIdx).HaveAnyResources)
                {
                    eMG.AdultForestC(cellIdx).Resources = 0;
                    sMG.MasterSs.TrySeedNewYoungForestS.TrySeed(cellIdx);
                    sMG.MasterSs.TryClearAllTrailsOnCellS.TryDestroy(cellIdx);
                }
            }
        }
    }
}