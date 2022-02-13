using System;

namespace Game.Game
{
    public static class CellUnitStatHpValues
    {
        public const float MAX_HP = 1;

        public static float FIRE_DAMAGE = 0.4f;

        public static float Damage(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 0.25f;
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