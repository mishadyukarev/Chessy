using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class SetExtraToolWeaponS : SystemModelGameAbs
    {
        internal SetExtraToolWeaponS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection, in byte cell_0)
        {
            eMG.UnitExtraTWTC(cell_0).ToolWeaponT = twT;
            eMG.UnitExtraLevelTC(cell_0).LevelT = levelT;
            eMG.UnitExtraProtectionC(cell_0).Protection = protection;
        }
        internal void Set(in byte cell_from, in byte cell_to) => eMG.UnitExtraTWE(cell_to) = eMG.UnitExtraTWE(cell_from);
    }
}