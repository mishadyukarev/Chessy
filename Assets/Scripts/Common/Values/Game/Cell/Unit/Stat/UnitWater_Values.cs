using System;

namespace Chessy.Game
{
    public static class UnitWater_Values
    {
        #region Start

        public const float MAX = 1;

        #endregion

        public static float NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.DirectWave: return MAX * 0.3f;
                case AbilityTypes.IceWall: return MAX * 0.3f;
                case AbilityTypes.ActiveAroundBonusSnowy: return MAX * 0.5f;
                default: throw new Exception();
            }
        }
        public static float NeedWaterForThirsty(in UnitTypes unit)
        {
            switch (unit)
            {
                case UnitTypes.King: return MAX * 0.1f;
                case UnitTypes.Pawn: return MAX * 0.1f;
                case UnitTypes.Scout: return MAX * 0.1f;
                case UnitTypes.Elfemale: return MAX * 0.1f;
                case UnitTypes.Snowy: return MAX * 0.1f;
                case UnitTypes.Undead: return 0;
                case UnitTypes.Hell: return 0;
                case UnitTypes.Skeleton: return 0;
                case UnitTypes.Camel: return MAX * 0.1f;
                default: throw new Exception();
            }
        }
    }
}