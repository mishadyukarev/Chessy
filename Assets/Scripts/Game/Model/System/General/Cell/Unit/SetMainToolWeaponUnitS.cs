using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetMainToolWeaponUnitS
    {
        readonly MainToolWeaponE _mainTWE;

        internal SetMainToolWeaponUnitS(in MainToolWeaponE mainTWE) { _mainTWE = mainTWE; }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT)
        {
            _mainTWE.ToolWeaponTC.ToolWeapon = twT;
            _mainTWE.LevelTC.Level = levelT;
        }
        internal void Set(in MainToolWeaponE mainTWE)
        {
            _mainTWE.ToolWeaponTC = mainTWE.ToolWeaponTC;
            _mainTWE.LevelTC = mainTWE.LevelTC;
        }
    }
}