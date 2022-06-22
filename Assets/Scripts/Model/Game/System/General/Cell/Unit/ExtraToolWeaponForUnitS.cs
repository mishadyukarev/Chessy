using Chessy.Game.Model.Entity.Cell.Unit;

namespace Chessy.Game.Model.System
{
    static class ExtraToolWeaponForUnitS
    {
        internal static void Set(this ExtraToolWeaponE extraTWE, in ToolWeaponTypes twT, in LevelTypes levelT, in float protection)
        {
            extraTWE.ToolWeaponT = twT;
            extraTWE.LevelT = levelT;
            extraTWE.ProtectionC.Protection = protection;
        }
    }
}