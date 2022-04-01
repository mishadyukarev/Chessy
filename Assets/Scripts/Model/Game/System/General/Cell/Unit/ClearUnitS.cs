using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class ClearUnitS : SystemModelGameAbs
    {
        internal ClearUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Clear(in byte cell_0)
        {
            eMG.UnitTC(cell_0).UnitT = UnitTypes.None;
        }
    }
}