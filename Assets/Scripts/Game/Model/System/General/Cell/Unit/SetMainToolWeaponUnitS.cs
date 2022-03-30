using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class SetMainToolWeaponUnitS : SystemModelGameAbs
    {
        internal SetMainToolWeaponUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in byte cell_0)
        {
            e.UnitMainTWTC(cell_0).ToolWeapon = twT;
            e.UnitMainTWLevelTC(cell_0).Level = levelT;
        }

        internal void Set(in byte cell_from, in byte cell_to) => e.UnitMainTWE(cell_to) = e.UnitMainTWE(cell_from);
    }
}