using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.Model.System
{
    sealed class SetExtraToolWeaponS
    {
        readonly ExtraToolWeaponE _extraTWE;

        internal SetExtraToolWeaponS(in ExtraToolWeaponE extraTWE)  { _extraTWE = extraTWE; }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection)
        {
            _extraTWE.ToolWeaponTC.ToolWeapon = twT;
            _extraTWE.LevelTC.Level = levelT;
            _extraTWE.ProtectionC.Protection = protection;
        }
        internal void Set(in ExtraToolWeaponE extraToolWeaponE)
        {
            _extraTWE.ToolWeaponTC = extraToolWeaponE.ToolWeaponTC;
            _extraTWE.LevelTC = extraToolWeaponE.LevelTC;
            _extraTWE.ProtectionC = extraToolWeaponE.ProtectionC;
        }
    }
}