using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers.Cell;
using System;

namespace Assets.Scripts
{
    public class CellEnvironmentWorker : MainGeneralWorker
    {
        private static CellEnvironmentEntsContainer _cellEnvironmentEntsContainer;

        private const int MAX_AMOUNT_FOOD = 10;
        private const int MAX_AMOUNT_WOOD = 10;
        private const int MAX_AMOUNT_ORE = 999;

        private const int MIN_AMOUNT_FOOD = 5;
        private const int MIN_AMOUNT_WOOD = 5;
        private const int MIN_AMOUNT_ORE = 999;

        internal const int START_FERTILIZER_PERCENT = 30;
        internal const int START_FOREST_PERCENT = 40;
        internal const int START_HILL_PERCENT = 25;
        internal const int START_MOUNTAIN_PERCENT = 15;


        internal CellEnvironmentWorker(CellEnvironmentEntsContainer cellEnvironmentEntsContainer)
        {
            _cellEnvironmentEntsContainer = cellEnvironmentEntsContainer;
        }

        private static void SetHaveEnvironment(bool haveEnvironment, EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.YoungForest:
                    _cellEnvironmentEntsContainer.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.AdultForest:
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.Hill:
                    _cellEnvironmentEntsContainer.CellHillEnt_HaveEnvCom(xy).HaveEnvironment = haveEnvironment;
                    break;

                case EnvironmentTypes.Mountain:
                    _cellEnvironmentEntsContainer.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment = haveEnvironment;
                    break;

                default:
                    throw new Exception();
            }
        }

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
                    return _cellEnvironmentEntsContainer.CellFertilizerEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.YoungForest:
                    return _cellEnvironmentEntsContainer.CellYoungForestEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.AdultForest:
                    return _cellEnvironmentEntsContainer.CellAdultForestEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Hill:
                    return _cellEnvironmentEntsContainer.CellHillEnt_HaveEnvCom(xy).HaveEnvironment;

                case EnvironmentTypes.Mountain:
                    return _cellEnvironmentEntsContainer.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment;

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
                    return _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources > 0;

                case ResourceTypes.Wood:
                    return _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources > 0;

                case ResourceTypes.Ore:
                    return _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources > 0;

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
                    return _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources;

                case ResourceTypes.Wood:
                    return _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources;

                case ResourceTypes.Ore:
                    return _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources;

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
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources = value;
                    break;

                case ResourceTypes.Wood:
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources = value;
                    break;

                case ResourceTypes.Ore:
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources = value;
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
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources += adding;
                    break;

                case ResourceTypes.Wood:
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources += adding;
                    break;

                case ResourceTypes.Ore:
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources += adding;
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
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources -= taking;
                    break;

                case ResourceTypes.Wood:
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources -= taking;
                    break;

                case ResourceTypes.Ore:
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources -= taking;
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
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.YoungForest:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellYoungForestEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Hill:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellHillEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources
                        = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Mountain:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellMountainEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
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
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources = amountEnvironmet;
                    break;

                case EnvironmentTypes.YoungForest:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellYoungForestEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    break;

                case EnvironmentTypes.AdultForest:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources = amountEnvironmet;
                    break;

                case EnvironmentTypes.Hill:
                    SetHaveEnvironment(true, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellHillEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources = amountEnvironmet;
                    break;

                case EnvironmentTypes.Mountain:
                    _cellEnvironmentEntsContainer.CellMountainEnt_HaveEnvCom(xy).HaveEnvironment = true;
                    _cellEnvironmentEntsContainer.CellMountainEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
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
                    SetHaveEnvironment(false, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources = default;
                    break;

                case EnvironmentTypes.YoungForest:
                    SetHaveEnvironment(false, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellYoungForestEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
                    break;

                case EnvironmentTypes.AdultForest:
                    SetHaveEnvironment(false, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources = default;
                    break;


                case EnvironmentTypes.Hill:
                    SetHaveEnvironment(false, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellHillEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources = default;
                    break;

                case EnvironmentTypes.Mountain:
                    SetHaveEnvironment(false, environmentType, xy);
                    _cellEnvironmentEntsContainer.CellMountainEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
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