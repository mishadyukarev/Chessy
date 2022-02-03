using System;

namespace Game.Game
{
    public static class CellUnitStatStepValues
    {
        public static int MaxAmountSteps(in UnitTypes unit, in bool haveEffect/*, in int upgradeSteps*/)
        {
            var steps = 0;

            switch (unit)
            {
                case UnitTypes.None: steps = 0; break;
                case UnitTypes.King: steps = 2; break;
                case UnitTypes.Pawn: steps = 2; break;
                case UnitTypes.Archer: steps = 3; break;
                case UnitTypes.Scout: steps = 5; break;
                case UnitTypes.Elfemale: steps = 3; break;
                case UnitTypes.Snowy: steps = 3; break;
                case UnitTypes.Undead: steps = 2; break;
                case UnitTypes.Hell: steps = 1; break;
                case UnitTypes.Camel: steps = 3; break;
                default: throw new Exception();
            }

            if (haveEffect) steps += 1;

            //steps += upgradeSteps;

            return steps;
        }
        internal static int NeedSteps(in AbilityTypes uniq)
        {
            switch (uniq)
            {
                case AbilityTypes.CircularAttack: return 1;
                case AbilityTypes.BonusNear: return 1;

                case AbilityTypes.FirePawn: return 1;
                case AbilityTypes.PutOutFirePawn: return 1;
                case AbilityTypes.Farm: return 1;
                case AbilityTypes.Mine: return 1;
                case AbilityTypes.City: return 1;
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
                default: throw new Exception();
            }
        }
        internal static int NeedSteps(in RpcMasterTypes rpc)
        {
            switch (rpc)
            {
                case RpcMasterTypes.DestroyBuild:
                    break;
                case RpcMasterTypes.Shift:
                    break;
                case RpcMasterTypes.Attack:
                    break;
                case RpcMasterTypes.ConditionUnit: return 1;
                case RpcMasterTypes.Mistake:
                    break;
                case RpcMasterTypes.CreateUnit:
                    break;
                case RpcMasterTypes.MeltOre:
                    break;
                case RpcMasterTypes.SetUnit:
                    break;
                case RpcMasterTypes.Sound:
                    break;
                case RpcMasterTypes.GetHero:
                    break;
                case RpcMasterTypes.UpgradeCellUnit:
                    break;
                case RpcMasterTypes.ToNewUnit:
                    break;
                case RpcMasterTypes.CreateHeroFromTo:
                    break;
                case RpcMasterTypes.GiveTakeToolWeapon:
                    break;
                case RpcMasterTypes.BuyRes:
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
        internal static int NeedSteps(in ConditionUnitTypes cond)
        {
            return 1;
        }
        internal static int NeedSteps(in ToolWeaponTypes tw)
        {
            return 1;
        }

        internal static int NeedSteps(in BuildingTypes build)
        {
            return 1;
        }
        internal static int NeedStepsShiftAttackUnit(in EnvironmentTypes envT)
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