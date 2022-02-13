using System;

namespace Game.Game
{
    public static class CellUnitStatWaterValues
    {
        public const float MAX_WATER_WITHOUT_EFFECTS = 1;

        public static float Need(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.DirectWave: return MAX_WATER_WITHOUT_EFFECTS * 0.3f;
                case AbilityTypes.IceWall: return MAX_WATER_WITHOUT_EFFECTS * 0.3f;
                case AbilityTypes.ActiveAroundBonusSnowy: return MAX_WATER_WITHOUT_EFFECTS * 0.5f;
                default: throw new Exception();
            }
        }
        public static float NeedWaterThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                case UnitTypes.Pawn: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                case UnitTypes.Scout: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                case UnitTypes.Elfemale: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                case UnitTypes.Snowy: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Camel: return MAX_WATER_WITHOUT_EFFECTS * 0.1f;
                default: throw new Exception();
            }
        }
    }
}