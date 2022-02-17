using System;

namespace Game.Game
{
    public static class CellUnitToolWeapon_Values
    {
        public static float ProtectionShield(in LevelTypes levT)
        {
            switch (levT)
            {
                case LevelTypes.First: return 1;
                case LevelTypes.Second: return 3;
                default: throw new Exception();
            }
        }
    }
}