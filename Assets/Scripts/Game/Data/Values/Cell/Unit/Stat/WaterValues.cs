using System;

namespace Chessy.Game.Values.Cell.Unit.Stats
{
    public static class WaterValues
    {
        public const float MAX = 1;


        #region Costs

        public const float AFTER_SHIFT_SNOWY = 0.1f;


        public const float DIRECT_WAVE = 0.3f;
        public const float BONUS_AROUND_SNOWY = 0.3f;
        public const float CHANGE_DIRECTION_WIND = 0.7f;

        public static float Need(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.ActiveAroundBonusSnowy: return BONUS_AROUND_SNOWY;
                case AbilityTypes.DirectWave: return DIRECT_WAVE;
                case AbilityTypes.ChangeDirectionWind: return CHANGE_DIRECTION_WIND;
                default: throw new Exception();
            }
        }


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

        #endregion
    }
}