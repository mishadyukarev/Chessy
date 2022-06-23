using System;

namespace Chessy.Model.Values.Cell.Unit
{
    public static class AbilityCooldownValues
    {
        public const int AFTER_GROW_ADULT_FOREST = 5;

        public static int NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.CircularAttack: return 2;

                case AbilityTypes.GrowAdultForest: return AFTER_GROW_ADULT_FOREST;
                case AbilityTypes.StunElfemale: return 5;
                case AbilityTypes.ChangeDirectionWind: return 6;

                //Snowy
                case AbilityTypes.DecreaseWindSnowy: return 2;
                case AbilityTypes.IncreaseWindSnowy: return 2;

                case AbilityTypes.Resurrect: return 3;
                case AbilityTypes.SetTeleport: return 10;

                default: throw new Exception();
            }
        }
    }
}