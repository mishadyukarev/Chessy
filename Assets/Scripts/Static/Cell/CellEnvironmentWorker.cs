using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;

namespace Assets.Scripts
{
    public static class CellEnvironmentWorker
    {
        internal static EntitiesGameGeneralManager EGGM => Main.Instance.EntGGM;

        internal static void ResetAll(int[] xy)
        {
            ResetEnvironment(EnvironmentTypes.Fertilizer, xy);
            ResetEnvironment(EnvironmentTypes.YoungForest, xy);
            ResetEnvironment(EnvironmentTypes.AdultForest, xy);
            ResetEnvironment(EnvironmentTypes.Hill, xy);
            ResetEnvironment(EnvironmentTypes.Mountain, xy);
        }

        internal static bool HaveEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return EGGM.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.YoungForest:
                    return EGGM.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.AdultForest:
                    return EGGM.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Hill:
                    return EGGM.CellHillEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Mountain:
                    return EGGM.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment;

                default:
                    throw new Exception();
            }
        }

        internal static bool HaveResources(ResourceTypes resourceType, int[] xy)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return EGGM.CellFertilizerEnt_AmountCom(xy).HaveAmount;

                case ResourceTypes.Wood:
                    return EGGM.CellAdultForestEnt_AmountCom(xy).HaveAmount;

                case ResourceTypes.Ore:
                    return EGGM.CellHillEnt_AmountCom(xy).HaveAmount;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static int AmountResources(ResourceTypes resourceType, int[] xy)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return EGGM.CellFertilizerEnt_AmountCom(xy).Amount;

                case ResourceTypes.Wood:
                    return EGGM.CellAdultForestEnt_AmountCom(xy).Amount;

                case ResourceTypes.Ore:
                    return EGGM.CellHillEnt_AmountCom(xy).Amount;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void SetAmountResources(ResourceTypes resourceType, int value, int[] xy)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGM.CellFertilizerEnt_AmountCom(xy).Amount = value;
                    break;

                case ResourceTypes.Wood:
                    EGGM.CellAdultForestEnt_AmountCom(xy).Amount = value;
                    break;

                case ResourceTypes.Ore:
                    EGGM.CellHillEnt_AmountCom(xy).Amount = value;
                    break;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void AddAmountResources(ResourceTypes resourceType, int[] xy, int adding = 1)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGM.CellFertilizerEnt_AmountCom(xy).AddAmount(adding);
                    break;

                case ResourceTypes.Wood:
                    EGGM.CellAdultForestEnt_AmountCom(xy).AddAmount(adding);
                    break;

                case ResourceTypes.Ore:
                    EGGM.CellHillEnt_AmountCom(xy).AddAmount(adding);
                    break;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void TakeAmountResources(ResourceTypes resourceType, int[] xy, int taking = 1)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    EGGM.CellFertilizerEnt_AmountCom(xy).TakeAmount(taking);
                    break;

                case ResourceTypes.Wood:
                    EGGM.CellAdultForestEnt_AmountCom(xy).TakeAmount(taking);
                    break;

                case ResourceTypes.Ore:
                    EGGM.CellHillEnt_AmountCom(xy).TakeAmount(taking);
                    break;

                case ResourceTypes.Iron:
                    throw new Exception();

                case ResourceTypes.Gold:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static int MaxAmountResources(EnvironmentTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return MAX_AMOUNT_FOOD;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return MAX_AMOUNT_WOOD;

                case EnvironmentTypes.Hill:
                    return MAX_AMOUNT_ORE;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static int MinAmountResources(EnvironmentTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return MIN_AMOUNT_FOOD;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return MIN_AMOUNT_WOOD;

                case EnvironmentTypes.Hill:
                    return MIN_AMOUNT_ORE;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void SetNewEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    EGGM.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellFertilizerEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellFertilizerEnt_AmountCom(xy).Amount 
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.YoungForest:
                    EGGM.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellYoungForestEnt_SprRendCom(xy).Enabled = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    EGGM.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellAdultForestEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellAdultForestEnt_AmountCom(xy).Amount
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Hill:
                    EGGM.CellHillEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellHillEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellHillEnt_AmountCom(xy).Amount
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Mountain:
                    EGGM.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellMountainEnt_SprRendCom(xy).Enabled = true;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetEnvironment(EnvironmentTypes environmentType, int amountEnvironmet, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    EGGM.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellFertilizerEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellFertilizerEnt_AmountCom(xy).Amount = amountEnvironmet;
                    break;

                case EnvironmentTypes.YoungForest:
                    EGGM.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellYoungForestEnt_SprRendCom(xy).Enabled = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    EGGM.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellAdultForestEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellAdultForestEnt_AmountCom(xy).Amount = amountEnvironmet;
                    break;

                case EnvironmentTypes.Hill:
                    EGGM.CellHillEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellHillEnt_SprRendCom(xy).Enabled = true;
                    EGGM.CellHillEnt_AmountCom(xy).Amount = amountEnvironmet;
                    break;

                case EnvironmentTypes.Mountain:
                    EGGM.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    EGGM.CellMountainEnt_SprRendCom(xy).Enabled = true;
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void ResetEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    break;

                case EnvironmentTypes.Fertilizer:
                    EGGM.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment = false;
                    EGGM.CellFertilizerEnt_SprRendCom(xy).Enabled = false;
                    EGGM.CellFertilizerEnt_AmountCom(xy).ResetAmount();
                    break;

                case EnvironmentTypes.YoungForest:
                    EGGM.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment = false;
                    EGGM.CellYoungForestEnt_SprRendCom(xy).Enabled = false;
                    break;

                case EnvironmentTypes.AdultForest:
                    EGGM.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment = false;
                    EGGM.CellAdultForestEnt_SprRendCom(xy).Enabled = false;
                    EGGM.CellAdultForestEnt_AmountCom(xy).ResetAmount();
                    break;


                case EnvironmentTypes.Hill:
                    EGGM.CellHillEnt_HaveEnvCom(xy).HaveEnvironment = false;
                    EGGM.CellHillEnt_SprRendCom(xy).Enabled = false;
                    EGGM.CellHillEnt_AmountCom(xy).ResetAmount();
                    break;

                case EnvironmentTypes.Mountain:
                    EGGM.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment = false;
                    EGGM.CellMountainEnt_SprRendCom(xy).Enabled = false;
                    break;

                default:
                    break;
            }
        }

        internal static int NeedAmountSteps(int[] xy)
        {
            int amountSteps = 1;

            if (HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_FOOD;

            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_TREE;

            if (HaveEnvironment(EnvironmentTypes.Hill, xy))
                amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

            return amountSteps;
        }
    }
}