using System;

namespace Chessy.Game.Values.Cell.Unit.Stats
{
    public static class HpValues
    {
        public const float MAX = 1;

        public const float FIRE_DAMAGE = 0.4f;
        public const float HP_FOR_DEATH_AFTER_ATTACK = 0.15f;

        public static float ThirstyPercent(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: return 0.4f;
                case UnitTypes.Pawn: return 0.5f;
                case UnitTypes.Elfemale: return 0.5f;
                case UnitTypes.Snowy: return 0.5f;
                case UnitTypes.Skeleton: return 0.5f;
                default: throw new Exception();
            }
        }
    }
}