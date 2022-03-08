using System;

namespace Chessy.Game.Values.Cell.Unit.Stats
{
    public struct WaterValues
    {
        public const float MAX = 1;

        public static float NeedWaterForThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return MAX * 0.1f;
                case UnitTypes.Pawn: return MAX * 0.1f;
                case UnitTypes.Elfemale: return MAX * 0.1f;
                case UnitTypes.Snowy: return MAX * 0.1f;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Camel: return MAX * 0.1f;
                default: throw new Exception();
            }
        }

        #region Costs

        public const float DIRECT_WAVE = 0.3f;
        public const float BONUS_AROUND_SNOWY = 0.3f;

        #endregion
    }
}