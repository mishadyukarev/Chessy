using System;

namespace Chessy.Game
{
    public static class UnitStep_Values
    {
        public const float FOR_SHIFT_ATTACK_EMPTY_CELL = 0.5f;
        public const float BONUS_TRAIL = 0.5f;
        public const float FOR_GIVE_TAKE_TOOLWEAPON = 0.25f;
        public const float FOR_TOGGLE_CONDITION_UNIT = 0.5f;


        public const float SCOUT_FIRST = 2f;


        #region CenterUpgrade

        public const float CENTER_BONUS_SCOUT = 3f;
        public const float CENTER_KING_BONUS = 1f;
        public const float CENTER_PAWN_BONUS = 0.5f;

        #endregion



        public const float CHANGE_CORNER_ARCHER = 0.5f;
        public const float ARCHER_FIRE = 1;

        public const float NEED_FOR_DESTROY_BUILDING = 0.5f;
        public const float NEED_FOR_BUILDING_ICEWALL = 0.5f;
        public const float NEED_FOR_BUILDING_CITY = 0.5f;
        public const float NEED_FOR_PAWN_FIRE = 0.5f;
        public const float NEED_FOR_PAWN_PUT_OUT_FIRE = 0.5f;
        public const float NEED_FOR_GROW_ADULT_FOREST = 0.5f;
        public static float NeedForAbility(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 0.5f;
                case AbilityTypes.BonusNear: return 0.5f;

                case AbilityTypes.SetFarm: return 0.5f;

                case AbilityTypes.Seed: return 0.5f;
                case AbilityTypes.StunElfemale: return 0.5f;
                case AbilityTypes.ChangeDirectionWind: return 1;

                case AbilityTypes.ActiveAroundBonusSnowy: return 0.5f;
                case AbilityTypes.DirectWave: return 0.5f;

                case AbilityTypes.Resurrect: return 0.5f;
                case AbilityTypes.SetTeleport: return 0.5f;
                case AbilityTypes.Teleport: return 0.5f;
                case AbilityTypes.InvokeSkeletons: return 0.5f;
                default: throw new Exception();
            }
        }

        public const float FERTILIZER = 0;
        public const float YOUNG_FOREST = 0;
        public const float ADULT_FOREST = 0.5f;
        public const float HILL = 0.5f;
    }
}