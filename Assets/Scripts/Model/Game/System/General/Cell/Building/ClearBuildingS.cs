using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class ClearBuildingS : SystemModelGameAbs
    {
        public ClearBuildingS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Clear(in byte cell_0)
        {
            eMG.BuildingMainE(cell_0) = default;
        }
    }
}