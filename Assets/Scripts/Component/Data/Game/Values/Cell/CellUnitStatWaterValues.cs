using System;

namespace Game.Game
{
    public static class CellUnitStatWaterValues
    {
        public const int MAX_WATER_WITHOUT_EFFECTS = 100;

        internal static int Need(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.DirectWave: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.5f);
                case AbilityTypes.IceWall: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.5f);
                case AbilityTypes.ActiveAroundBonusSnowy: return MAX_WATER_WITHOUT_EFFECTS;
                default: throw new Exception();
            }
        }
        internal static int NeedWaterThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                case UnitTypes.Pawn: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                case UnitTypes.Archer: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                case UnitTypes.Scout: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                case UnitTypes.Elfemale: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                case UnitTypes.Snowy: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.3f);
                case UnitTypes.Undead: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0);
                case UnitTypes.Hell: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0);
                case UnitTypes.Camel: return (int)(MAX_WATER_WITHOUT_EFFECTS * 0.15f);
                default: throw new Exception();
            }
        }
    }
}