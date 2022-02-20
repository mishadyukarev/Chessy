using System;

namespace Game.Game
{
    public static class CellUnitStatStep_Values
    {
        public const float FOR_SHIFT_ATTACK_EMPTY_CELL = 0.5f;

        public const float BONUS_TRAIL = 0.5f;

        public const float FOR_GIVE_TAKE_TOOLWEAPON = 0.25f;

        public const float FOR_TOGGLE_CONDITION_UNIT = 0.5f;

        public static float StandartForUnit(in UnitTypes unit)
        {
            var steps = 0f;

            switch (unit)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 1; break;
                case UnitTypes.Pawn: steps = 1; break;

                case UnitTypes.Scout: steps = 2.5f; break;

                case UnitTypes.Elfemale: steps = 2; break;
                case UnitTypes.Snowy: steps = 3; break;
                case UnitTypes.Undead: steps = 3; break;
                case UnitTypes.Hell: steps = 1; break;

                case UnitTypes.Skeleton: steps = 2; break;

                case UnitTypes.Camel: steps = 2; break;
                default: throw new Exception();
            }

            return steps;
        }
        public static float NeedForAbility(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 0.5f;
                case AbilityTypes.BonusNear: return 0.5f;

                case AbilityTypes.FirePawn: return 0.5f;
                case AbilityTypes.PutOutFirePawn: return 0.5f;
                case AbilityTypes.SetFarm: return 0.5f;
                case AbilityTypes.SetCity: return 0.5f;
                case AbilityTypes.DestroyBuilding: return 0.5f;

                case AbilityTypes.Seed: return 0.5f;
                case AbilityTypes.FireArcher: return 1;
                case AbilityTypes.ChangeCornerArcher: return 0.5f;
                case AbilityTypes.GrowAdultForest: return 0.5f;
                case AbilityTypes.StunElfemale: return 0.5f;
                case AbilityTypes.ChangeDirectionWind: return 1;

                case AbilityTypes.IceWall: return 0.5f;
                case AbilityTypes.ActiveAroundBonusSnowy: return 0.5f;
                case AbilityTypes.DirectWave: return 0.5f;

                case AbilityTypes.Resurrect: return 0.5f;
                case AbilityTypes.SetTeleport: return 0.5f;
                case AbilityTypes.Teleport: return 0.5f;
                case AbilityTypes.InvokeSkeletons: return 0.5f;
                default: throw new Exception();
            }
        }
        public static float NeedSteps(in RpcMasterTypes rpc)
        {
            switch (rpc)
            {
                case RpcMasterTypes.Shift:
                    break;
                case RpcMasterTypes.Attack:
                    break;
                case RpcMasterTypes.ConditionUnit: return 1;
                case RpcMasterTypes.Mistake:
                    break;
                case RpcMasterTypes.SetUnit:
                    break;
                case RpcMasterTypes.Sound:
                    break;
                case RpcMasterTypes.GetHero:
                    break;
                case RpcMasterTypes.GiveTakeToolWeapon:
                    break;
                case RpcMasterTypes.UpgCenterUnits:
                    break;
                case RpcMasterTypes.UpgCenterBuild:
                    break;
                case RpcMasterTypes.UpgWater:
                    break;
                case RpcMasterTypes.UniqueAbility:
                    break;
                case RpcMasterTypes.End:
                    break;
                default:
                    break;
            }

            return 1;
        }

        public static float NeedSteps(in BuildingTypes build)
        {
            return 1;
        }
        public static float NeedStepsShiftAttackUnit(in EnvironmentTypes envT)
        {
            switch (envT)
            {
                case EnvironmentTypes.Fertilizer: return 0;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 0.5f;
                case EnvironmentTypes.Hill: return 0.5f;
                default: throw new Exception();
            }
        }
        public static float NeedStepsShiftAttackUnit(in bool haveFertilizer, in bool haveYoungForest, in bool haveAdultForest, in bool haveHill)
        {
            var needSteps = 0f;

            if (haveFertilizer) needSteps += NeedStepsShiftAttackUnit(EnvironmentTypes.Fertilizer);
            if (haveYoungForest) needSteps += NeedStepsShiftAttackUnit(EnvironmentTypes.YoungForest);
            if (haveAdultForest) needSteps += NeedStepsShiftAttackUnit(EnvironmentTypes.AdultForest);
            if (haveHill) needSteps += NeedStepsShiftAttackUnit(EnvironmentTypes.Hill);

            return needSteps;
        }
    }
}