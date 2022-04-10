using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class SetExtraToolWeaponS_M : SystemModel
    {
        internal SetExtraToolWeaponS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection, in byte cell_0)
        {
            eMG.ExtraToolWeaponTC(cell_0).ToolWeaponT = twT;
            eMG.ExtraTWLevelTC(cell_0).LevelT = levelT;
            eMG.ExtraTWProtectionC(cell_0).Protection = protection;
        }
        internal void SetCopy(in byte cell_from, in byte cell_to)
        {
            eMG.ExtraToolWeaponTC(cell_to) = eMG.ExtraToolWeaponTC(cell_from);
            eMG.ExtraTWLevelTC(cell_to) = eMG.ExtraTWLevelTC(cell_from);
            eMG.ExtraTWProtectionC(cell_to) = eMG.ExtraTWProtectionC(cell_from);
        }
    }
}