using System;

namespace Chessy.Game.Values.Cell.Unit.Stats
{
    public static class StepValues
    {
        #region NON_CHANGE!!!!!!!

        public const float MAX = 1;

        public const float FOR_SHIFT_ATTACK_EMPTY_CELL = 0.5f;
        public const float BONUS_TRAIL = 0.5f;
        public const float FOR_GIVE_TAKE_TOOLWEAPON = 0.25f;
        public const float FOR_TOGGLE_CONDITION_UNIT = 0.5f;

        #endregion !!!!!!!!!!


        #region CenterUpgrade

        public const float CENTER_BONUS_SCOUT = 3f;
        public const float CENTER_KING_BONUS = 1f;
        public const float CENTER_PAWN_BONUS = 0.5f;

        #endregion


        #region Costs

        public const float CHANGE_CORNER_ARCHER = 0.5f;
        public const float ARCHER_FIRE = 0.5f;


        #region Ability

        public const float DESTROY_BUILDING = 0.5f;
        public const float PAWN_CITY_BUILDING = 0.5f;
        public const float FIRE_PAWN = 0.5f;
        public const float PUT_OUT_FIRE_PAWN = 0.5f;
        public const float GROW_ADULT_FOREST = 0.5f;
        public const float CIRCULAR_ATTACK = 0.5f;
        public const float SEED_PAWN = 0.5f;
        public const float SET_FARM = 0.5f;
        public const float STUN_ELFEMALE = 0.5f;
        public const float CHANGE_DIRECTION_WIND = 1f;
        public const float RESURRECT = 0.5f;
        public const float SET_TELEPORT = 0.5f;
        public const float TELEPORT = 0.5f;
        public const float INVOKE_SKELETON = 0.5f;

        public static float Need(in AbilityTypes abilityT)
        {
            switch (abilityT)
            {
                case AbilityTypes.CircularAttack:
                    return CIRCULAR_ATTACK;

                case AbilityTypes.FirePawn:
                    return FIRE_PAWN;

                case AbilityTypes.PutOutFirePawn:
                    return PUT_OUT_FIRE_PAWN;

                case AbilityTypes.Seed:
                    return SEED_PAWN;

                case AbilityTypes.SetFarm:
                    return SET_FARM;

                case AbilityTypes.DestroyBuilding:
                    return DESTROY_BUILDING;

                case AbilityTypes.FireArcher:
                    return ARCHER_FIRE;

                case AbilityTypes.ChangeCornerArcher:
                    return CHANGE_CORNER_ARCHER;

                case AbilityTypes.GrowAdultForest:
                    return GROW_ADULT_FOREST;

                case AbilityTypes.StunElfemale:
                    return STUN_ELFEMALE;

                case AbilityTypes.IncreaseWindSnowy:
                    return 0.5f;

                case AbilityTypes.DecreaseWindSnowy:
                    return 0.5f;

                case AbilityTypes.ChangeDirectionWind:
                    return CHANGE_DIRECTION_WIND;

                //case AbilityTypes.IceWall:
                //    return BUILDING_ICE_WALL;

                //case AbilityTypes.ActiveAroundBonusSnowy:
                //    return BONUS_AROUND_SNOWY;

                //case AbilityTypes.DirectWave:
                //    return DIRECT_WAVE;

                case AbilityTypes.Resurrect:
                    return RESURRECT;

                case AbilityTypes.SetTeleport:
                    return SET_TELEPORT;

                case AbilityTypes.Teleport:
                    return TELEPORT;

                case AbilityTypes.InvokeSkeletons:
                    return INVOKE_SKELETON;

                default: throw new Exception();
            }
        }

        #endregion

        #endregion

        public const float ADULT_FOREST = 0.5f;
        public const float HILL = 0.5f;

    }
}