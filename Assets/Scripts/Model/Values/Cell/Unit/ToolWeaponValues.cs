using System;

namespace Chessy.Model.Values.Cell.Unit
{
    static class ToolWeaponValues
    {
        internal const float SHIELD_PROTECTION_LEVEL_FIRST = 1; //CONST!
        internal const float SHIELD_PROTECTION_LEVEL_SECOND = 5; //CONST!

        internal static float ShieldProtection(in LevelTypes levelT)
        {
            switch (levelT)
            {
                case LevelTypes.First: return SHIELD_PROTECTION_LEVEL_FIRST;
                case LevelTypes.Second: return SHIELD_PROTECTION_LEVEL_SECOND;
                default: throw new Exception();
            }
        }
    }
}