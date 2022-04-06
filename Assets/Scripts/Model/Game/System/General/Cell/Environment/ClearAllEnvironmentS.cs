using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class ClearAllEnvironmentS : SystemModel
    {
        internal ClearAllEnvironmentS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Clear(in byte cell_0)
        {
            eMG.FertilizeC(cell_0).Resources = 0;
            eMG.AdultForestC(cell_0).Resources = 0;
            eMG.YoungForestC(cell_0).Resources = 0;
            eMG.HillC(cell_0).Resources = 0;
            eMG.MountainC(cell_0).Resources = 0;
        }
    }
}