using System;

namespace Chessy.Model.Values
{
    public static partial class ValuesChessy
    {
        public const float SHIELD_MAX_PROTECTION_LEVEL_FIRST = 1; //CONST!
        public const float SHIELD_MAX_PROTECTION_LEVEL_SECOND = 5; //CONST!

        public static float MaxShieldProtection(in LevelTypes levelT)
        {
            switch (levelT)
            {
                case LevelTypes.First: return SHIELD_MAX_PROTECTION_LEVEL_FIRST;
                case LevelTypes.Second: return SHIELD_MAX_PROTECTION_LEVEL_SECOND;
                default: throw new Exception();
            }
        }
    }
}