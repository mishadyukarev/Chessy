using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;

namespace Chessy.Game
{
    sealed class TryExecuteFarmExtractMS : SystemModel
    {
        internal TryExecuteFarmExtractMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryExecute()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.FarmExtractFertilizeC(cellIdx0).HaveAnyResources)
                {
                    var extract = eMG.FarmExtractFertilizeC(cellIdx0).Resources;

                    eMG.ResourcesC(eMG.BuildingPlayerTC(cellIdx0).PlayerT, ResourceTypes.Food).Resources += extract;
                    eMG.FertilizeC(cellIdx0).Resources -= extract;

                    //if (!E.FertilizeC(cell_0).HaveAnyResources)
                    //{
                    //    E.BuildingTC(cell_0).Building = BuildingTypes.None;
                    //}
                }
            }
        }
    }
}