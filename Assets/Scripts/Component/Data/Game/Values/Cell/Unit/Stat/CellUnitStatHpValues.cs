using System;

namespace Game.Game
{
    public static class CellUnitStatHpValues
    {
        public const int MAX_HP = 100;

        public static int FIRE_DAMAGE = 40;

        public static int Damage(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 25;
                default: throw new Exception();
            }
        }
        public static float ThirstyPercent(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 0.4f;
                case UnitTypes.Pawn: return 0.5f;
                case UnitTypes.Scout: return 0.5f;
                case UnitTypes.Elfemale: return 0.5f;
                case UnitTypes.Snowy: return 0.5f;
                case UnitTypes.Skeleton: return 0.5f;
                default: throw new Exception();
            }
        }
    }
}