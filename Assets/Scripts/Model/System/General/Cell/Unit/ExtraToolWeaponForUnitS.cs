using Chessy.Model.Cell.Unit;
namespace Chessy.Model
{
    static class ExtraToolWeaponForUnitS
    {
        internal static void Set(this ref ExtraToolWeaponUnitC extraTWE, in ToolWeaponTypes twT, in LevelTypes levelT, in float protection)
        {
            extraTWE.ToolWeaponT = twT;
            extraTWE.LevelT = levelT;
            extraTWE.ProtectionShield = protection;
        }
    }
}