using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;


namespace Chessy.Game.Model.System
{
    sealed class SetMainToolWeaponUnitS : SystemModelGameAbs
    {
        internal SetMainToolWeaponUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in byte cell_0)
        {
            eMG.UnitMainTWTC(cell_0).ToolWeaponT = twT;
            eMG.UnitMainTWLevelTC(cell_0).LevelT = levelT;
        }

        internal void Set(in byte cell_from, in byte cell_to) => eMG.UnitMainTWE(cell_to) = eMG.UnitMainTWE(cell_from);
    }
}