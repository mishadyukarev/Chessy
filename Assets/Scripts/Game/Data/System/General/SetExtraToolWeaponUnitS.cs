using Chessy.Game.Entity.Model.Cell.Unit;

namespace Chessy.Game.System.Model
{
    static class SetExtraToolWeaponUnitS
    {
        public static void Set(this ExtraToolWeaponE extraTWE, in ToolWeaponTypes twT, in LevelTypes levT, in float protection)
        {
            extraTWE.ToolWeaponTC.ToolWeapon = twT;
            extraTWE.LevelTC.Level = levT;
            extraTWE.ProtectionC.Protection = protection;
        }
    }
}