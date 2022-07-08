using System;

namespace Chessy.Model.Values
{
    public static class AbilityCooldownUnitValues
    {
        public const int AFTER_GROW_ADULT_FOREST = 30;

        public static int NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.CircularAttack: return 15;

                case AbilityTypes.GrowAdultForest: return AFTER_GROW_ADULT_FOREST;
                case AbilityTypes.StunElfemale: return 15;
                case AbilityTypes.ChangeDirectionWind: return 20;

                //Snowy
                case AbilityTypes.DecreaseWindSnowy: return 5;
                case AbilityTypes.IncreaseWindSnowy: return 5;

                case AbilityTypes.Resurrect: return 3;
                case AbilityTypes.SetTeleport: return 10;

                default: throw new Exception();
            }
        }
    }
}