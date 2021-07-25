using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers.Cell;
using System;

namespace Assets.Scripts
{
    public sealed class CellEnvirDataWorker : MainGeneralWorker
    {
        private static CellEnvirEntsContainer _cellEnvironmentEntsContainer;

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


        internal CellEnvirDataWorker(CellEnvirEntsContainer cellEnvironmentEntsContainer)
        {
            _cellEnvironmentEntsContainer = cellEnvironmentEntsContainer;
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

        internal static void ResetEnvironment(EnvironmentTypes environmentType, int[] xy) => SetEnvironment(environmentType, false, 0, xy);

        #region Resources

        internal static int GetAmountResources(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    return _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources;

                case EnvironmentTypes.Hill:
                    return _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal static void SetAmountResources(EnvironmentTypes environmentType, int value, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    _cellEnvironmentEntsContainer.CellFertilizerEnt_AmountResourcesCom(xy).AmountResources = value;
                    break;

                case EnvironmentTypes.YoungForest:
                    throw new Exception();

                case EnvironmentTypes.AdultForest:
                    _cellEnvironmentEntsContainer.CellAdultForestEnt_AmountResourcesCom(xy).AmountResources = value;
                    break;

                case EnvironmentTypes.Hill:
                    _cellEnvironmentEntsContainer.CellHillEnt_AmountResourcesCom(xy).AmountResources = value;
                    break;

                case EnvironmentTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }

        internal static void AddAmountResources(EnvironmentTypes environmentType, int[] xy, int adding = 1)
            => SetAmountResources(environmentType, GetAmountResources(environmentType, xy) + adding, xy);
        internal static void TakeAmountResources(EnvironmentTypes environmentType, int[] xy, int taking = 1)
            => SetAmountResources(environmentType, GetAmountResources(environmentType, xy) - taking, xy);

        internal static bool HaveResources(EnvironmentTypes environmentType, int[] xy) => GetAmountResources(environmentType, xy) > 0;
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

        #endregion

        internal static void SetEnvironment(EnvironmentTypes environmentType, bool haveEnv, int amountEnvironmet, int[] xy)
        {
            SetHaveEnvironment(haveEnv, environmentType, xy);

            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.YoungForest:
                    SetHaveEnvironment(haveEnv, environmentType, xy);
                    break;

                case EnvironmentTypes.AdultForest:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.Hill:
                    SetAmountResources(environmentType, amountEnvironmet, xy);
                    break;

                case EnvironmentTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }
        }
        internal static void SetNewEnvironment(EnvironmentTypes environmentType, int[] xy)
        {
            int randAmountResour = 0;       
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.YoungForest:
                    break;

                case EnvironmentTypes.AdultForest:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Hill:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType));
                    break;

                case EnvironmentTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }

            SetEnvironment(environmentType, true, randAmountResour, xy);
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