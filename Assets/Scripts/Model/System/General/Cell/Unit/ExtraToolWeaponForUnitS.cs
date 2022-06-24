﻿using Chessy.Model.Model.Entity.Cell.Unit;

namespace Chessy.Model.Model.System
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