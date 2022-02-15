using System;

namespace Game.Game
{
    public static class CellUnitStatWaterValues
    {
        public const float WATER_MAX_STANDART = 1;

        public static float NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.DirectWave: return WATER_MAX_STANDART * 0.3f;
                case AbilityTypes.IceWall: return WATER_MAX_STANDART * 0.3f;
                case AbilityTypes.ActiveAroundBonusSnowy: return WATER_MAX_STANDART * 0.5f;
                default: throw new Exception();
            }
        }
        public static float NeedWaterForThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return WATER_MAX_STANDART * 0.1f;
                case UnitTypes.Pawn: return WATER_MAX_STANDART * 0.1f;
                case UnitTypes.Scout: return WATER_MAX_STANDART * 0.1f;
                case UnitTypes.Elfemale: return WATER_MAX_STANDART * 0.1f;
                case UnitTypes.Snowy: return WATER_MAX_STANDART * 0.1f;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Camel: return WATER_MAX_STANDART * 0.1f;
                default: throw new Exception();
            }
        }
    }
}