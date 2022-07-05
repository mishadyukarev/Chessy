using System;

namespace Chessy.Model.Values
{
    public static partial class ValuesChessy
    {
        public const float MAX_WATER_FOR_ANY_UNIT = 1;


        #region Costs

        public const float TAKING_WATER_AFTER_SHIFT_SNOWY = 0.1f;

        public static float NeedWaterForThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return MAX_WATER_FOR_ANY_UNIT * 0.1f;
                case UnitTypes.Pawn: return MAX_WATER_FOR_ANY_UNIT * 0.1f;
                case UnitTypes.Elfemale: return 0;
                case UnitTypes.Snowy: return MAX_WATER_FOR_ANY_UNIT * 0.1f;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Tree: return 0;
                case UnitTypes.Wolf: return 0;
                default: throw new Exception();
            }
        }

        #endregion
    }
}