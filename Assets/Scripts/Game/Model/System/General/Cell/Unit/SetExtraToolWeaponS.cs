using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetExtraToolWeaponS : SystemModelGameAbs
    {
        internal SetExtraToolWeaponS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection, in byte cell_0)
        {
            e.UnitExtraTWTC(cell_0).ToolWeapon = twT;
            e.UnitExtraLevelTC(cell_0).Level = levelT;
            e.UnitExtraProtectionC(cell_0).Protection = protection;
        }
        internal void Set(in byte cell_from, in byte cell_to) => e.UnitExtraTWE(cell_to) = e.UnitExtraTWE(cell_from);
    }
}