using System;

namespace Chessy.Game.Values.Cell.Unit
{
    public struct AbilityCooldownValues
    {
        public const float AFTER_ICE_WALL = 10;
        public const float AFTER_GROW_ADULT_FOREST = 10;
        public const float DIRECT_WAVE = 5;

        public static int NeedAfterAbility(in AbilityTypes ability)
        {
            switch (ability)
            {
                case AbilityTypes.CircularAttack: return 2;
                case AbilityTypes.KingPassiveNearBonus: return 3;
                case AbilityTypes.StunElfemale: return 5;
                case AbilityTypes.ChangeDirectionWind: return 6;

                case AbilityTypes.ActiveAroundBonusSnowy: return 5;
                case AbilityTypes.IceWall: return 10;

                case AbilityTypes.Resurrect: return 3;
                case AbilityTypes.SetTeleport: return 10;

                default: throw new Exception();
            }
        }
    }
}