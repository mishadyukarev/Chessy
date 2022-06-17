using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct MainToolWeaponUnitWorkS
    {
        readonly MainToolWeaponE _mainTWUnitE;

        internal MainToolWeaponUnitWorkS(in MainToolWeaponE mainToolWeaponE) => _mainTWUnitE = mainToolWeaponE;

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            _mainTWUnitE.ToolWeaponTC.ToolWeaponT = twT;
            _mainTWUnitE.LevelTC.LevelT = levelT;
        }
        internal void Clear()
        {
            _mainTWUnitE.ToolWeaponTC.ToolWeaponT = default;
            _mainTWUnitE.LevelTC.LevelT = default;
        }
    }
}