using System;

namespace Game.Game
{
    public static class CellUnitEffectShield_Values
    {
        public static int ProtectionAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.ActiveAroundBonusSnowy: return 1;
                case AbilityTypes.DirectWave: return 1;
                default: throw new Exception();
            }
        }
    }
}