using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct SetMainToolWeaponUnitS
    {
        readonly MainToolWeaponE _mainTWUnitE;

        internal SetMainToolWeaponUnitS(in MainToolWeaponE mainToolWeaponE) => _mainTWUnitE = mainToolWeaponE;

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            _mainTWUnitE.ToolWeaponTC.ToolWeaponT = twT;
            _mainTWUnitE.LevelTC.LevelT = levelT;
        }
    }
}