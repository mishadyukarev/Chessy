﻿using System;

namespace Game.Game
{
    public static class CellUnitStatStepValues
    {
        public static float MaxAmountSteps(in UnitTypes unit, in bool haveEffect/*, in int upgradeSteps*/)
        {
            var steps = 0;

            switch (unit)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 2; break;
                case UnitTypes.Pawn: steps = 2; break;
                case UnitTypes.Scout: steps = 5; break;
                case UnitTypes.Elfemale: steps = 3; break;
                case UnitTypes.Snowy: steps = 3; break;
                case UnitTypes.Undead: steps = 3; break;
                case UnitTypes.Hell: steps = 1; break;

                case UnitTypes.Skeleton: steps = 3; break;

                case UnitTypes.Camel: steps = 3; break;
                default: throw new Exception();
            }

            if (haveEffect) steps += 1;

            //steps += upgradeSteps;

            return steps;
        }
        internal static float NeedSteps(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 1;
                case AbilityTypes.BonusNear: return 1;

                case AbilityTypes.FirePawn: return 1;
                case AbilityTypes.PutOutFirePawn: return 1;
                case AbilityTypes.SetFarm: return 1;
                case AbilityTypes.SetCity: return 1;
                case AbilityTypes.DestroyBuilding: return 1;

                case AbilityTypes.Seed: return 1;
                case AbilityTypes.FireArcher: return 2;
                case AbilityTypes.ChangeCornerArcher: return 1;
                case AbilityTypes.GrowAdultForest: return 1;
                case AbilityTypes.StunElfemale: return 1;
                case AbilityTypes.ChangeDirectionWind: return 1;

                case AbilityTypes.IceWall: return 1;
                case AbilityTypes.ActiveAroundBonusSnowy: return 1;
                case AbilityTypes.DirectWave: return 1;

                case AbilityTypes.Resurrect: return 1;
                case AbilityTypes.SetTeleport: return 1;
                case AbilityTypes.Teleport: return 1;
                case AbilityTypes.InvokeSkeletons: return 1;
                default: throw new Exception();
            }
        }
        internal static float NeedSteps(in RpcMasterTypes rpc)
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
                case RpcMasterTypes.UpgradeCellUnit:
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
        internal static float NeedSteps(in ConditionUnitTypes cond)
        {
            return 1;
        }
        internal static float NeedSteps(in ToolWeaponTypes tw)
        {
            return 1;
        }

        internal static float NeedSteps(in BuildingTypes build)
        {
            return 1;
        }
        internal static float NeedStepsShiftAttackUnit(in EnvironmentTypes envT)
        {
            switch (envT)
            {
                case EnvironmentTypes.Fertilizer: return 0;
                case EnvironmentTypes.YoungForest: return 0;
                case EnvironmentTypes.AdultForest: return 1;
                case EnvironmentTypes.Hill: return 1;
                default: throw new Exception();
            }
        }
    }
}