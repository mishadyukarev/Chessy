using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    struct ExtraToolWeaponForUnitS
    {
        readonly ExtraToolWeaponE _extraTWE;

        internal ExtraToolWeaponForUnitS(in ExtraToolWeaponE extraTWE)
        {
            _extraTWE = extraTWE;
        }

        internal void Set(in ToolWeaponTypes twT, in LevelTypes levelT, in float protection)
        {
            _extraTWE.ToolWeaponTC.ToolWeaponT = twT;
            _extraTWE.LevelTC.LevelT = levelT;
            _extraTWE.ProtectionC.Protection = protection;
        }
    }
}