using System;

namespace Game.Game
{
    public static class CellUnitStatWaterValues
    {
        public const int MAX_WATER_WITHOUT_EFFECTS = 100;

        public static int Need(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.DirectWave: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.3f);
                case AbilityTypes.IceWall: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.3f);
                case AbilityTypes.ActiveAroundBonusSnowy: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.5f);
                default: throw new Exception();
            }
        }
        public static int NeedWaterThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1);
                case UnitTypes.Pawn: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1);
                case UnitTypes.Scout: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1);
                case UnitTypes.Elfemale: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1);
                case UnitTypes.Snowy: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1f);
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Camel: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.1);
                default: throw new Exception();
            }
        }
    }
}