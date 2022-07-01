using System;
namespace Chessy.Model.Values
{
    public static class WaterValues
    {
        public const float MAX = 1;


        #region Costs

        public const float AFTER_SHIFT_SNOWY = 0.1f;


        //public const float CHANGE_DIRECTION_WIND = 1;

        //public static float Need(in AbilityTypes abilityT)
        //{
        //    switch (abilityT)
        //    {
        //        case AbilityTypes.IncreaseWindSnowy: return 0.5f;
        //        case AbilityTypes.DecreaseWindSnowy: return 0.5f;
        //        case AbilityTypes.ChangeDirectionWind: return CHANGE_DIRECTION_WIND;
        //        default: throw new Exception();
        //    }
        //}


        public static float NeedWaterForThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return MAX * 0.1f;
                case UnitTypes.Pawn: return MAX * 0.1f;
                case UnitTypes.Elfemale: return 0;
                case UnitTypes.Snowy: return MAX * 0.1f;
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