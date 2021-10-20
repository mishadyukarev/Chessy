using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct CellEnvironDataCom
    {
        private Dictionary<EnvirTypes, bool> _haveCellEnvirOnCell;
        private Dictionary<EnvirTypes, int> _amountResourcesOnCell;



        internal int NeedAmountSteps
        {
            get
            {
                int amountSteps = 1;         

                if (Have(EnvirTypes.AdultForest))
                    amountSteps += UnitValues.NeedAmountSteps(EnvirTypes.AdultForest);

                if (Have(EnvirTypes.Hill))
                    amountSteps += UnitValues.NeedAmountSteps(EnvirTypes.Hill);

                return amountSteps;
            }
        }
        internal Dictionary<EnvirTypes, bool> Envronments
        {
            get
            {
                var envrs = new Dictionary<EnvirTypes, bool>();

                for (EnvirTypes envType = (EnvirTypes)1; envType < (EnvirTypes)Enum.GetNames(typeof(EnvirTypes)).Length; envType++)
                {
                    if (_haveCellEnvirOnCell[envType]) envrs.Add(envType, true);
                    else envrs.Add(envType, false);
                }

                return envrs;
            }
        }

        internal CellEnvironDataCom(Dictionary<EnvirTypes, bool> haveCellEnvironments)
        {
            _haveCellEnvirOnCell = haveCellEnvironments;
            _amountResourcesOnCell = new Dictionary<EnvirTypes, int>();

            for (EnvirTypes envirType = (EnvirTypes)1; envirType < (EnvirTypes)Enum.GetNames(typeof(EnvirTypes)).Length; envirType++)
            {
                _haveCellEnvirOnCell.Add(envirType, default);
                _amountResourcesOnCell.Add(envirType, default);
            }
        }


        internal void SetHaveEnvironment(EnvirTypes environmentType, bool haveEnvironment) => _haveCellEnvirOnCell[environmentType] = haveEnvironment;

        internal bool Have(EnvirTypes environmentType) => _haveCellEnvirOnCell[environmentType];
        internal void SetEnvironment(EnvirTypes environmentType) => SetHaveEnvironment(environmentType, true);
        internal void ResetEnvironment(EnvirTypes environmentType) => SetHaveEnvironment(environmentType, default);


        internal int GetAmountResources(EnvirTypes environmentType) => _amountResourcesOnCell[environmentType];
        internal void SetAmountResources(EnvirTypes environmentType, int value) => _amountResourcesOnCell[environmentType] = value;

        internal void AddAmountRes(EnvirTypes environmentType, int adding = 1) => SetAmountResources(environmentType, GetAmountResources(environmentType) + adding);
        internal void TakeAmountResources(EnvirTypes environmentType, int taking = 1) => SetAmountResources(environmentType, GetAmountResources(environmentType) - taking);

        internal bool HaveResources(EnvirTypes environmentType) => GetAmountResources(environmentType) > 0;
        internal int MaxAmountResources(EnvirTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    return EnvironmentValues.MAX_AMOUNT_FOOD;

                case EnvirTypes.YoungForest:
                    throw new Exception();

                case EnvirTypes.AdultForest:
                    return EnvironmentValues.MAX_AMOUNT_FOREST;

                case EnvirTypes.Hill:
                    return EnvironmentValues.MAX_AMOUNT_ORE;

                case EnvirTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }
        internal int MinAmountResources(EnvirTypes environmentTypes)
        {
            switch (environmentTypes)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    return EnvironmentValues.MIN_AMOUNT_FOOD;

                case EnvirTypes.YoungForest:
                    throw new Exception();

                case EnvirTypes.AdultForest:
                    return EnvironmentValues.MIN_AMOUNT_WOOD;

                case EnvirTypes.Hill:
                    return EnvironmentValues.MIN_AMOUNT_ORE;

                case EnvirTypes.Mountain:
                    throw new Exception();

                default:
                    throw new Exception();
            }
        }


        internal void SetNewEnvir(EnvirTypes environmentType)
        {
            SetEnvironment(environmentType);

            int randAmountResour = 0;
            switch (environmentType)
            {
                case EnvirTypes.None:
                    throw new Exception();

                case EnvirTypes.Fertilizer:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType) + 1);
                    break;

                case EnvirTypes.YoungForest:
                    break;

                case EnvirTypes.AdultForest:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType) + 1);
                    break;

                case EnvirTypes.Hill:
                    randAmountResour = UnityEngine.Random.Range(MinAmountResources(environmentType), MaxAmountResources(environmentType) + 1);
                    break;

                case EnvirTypes.Mountain:
                    break;

                default:
                    throw new Exception();
            }

            SetAmountResources(environmentType, randAmountResour);
        }
    }
}
