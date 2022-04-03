using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class ClearBuildingS : SystemModelGameAbs
    {
        public ClearBuildingS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Clear(in byte cell_0)
        {
            eMG.BuildingEs(cell_0) = default;
        }
    }
}