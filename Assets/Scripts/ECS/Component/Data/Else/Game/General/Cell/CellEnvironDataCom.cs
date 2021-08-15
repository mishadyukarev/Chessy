using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
{
    internal struct CellEnvironDataCom
    {
        private Dictionary<EnvironmentTypes, bool> _haveCellEnvironmentOnCell;
        private Dictionary<EnvironmentTypes, int> _amountResourcesOnCell;

        internal int NeedAmountSteps
        {
            get
            {
                int amountSteps = 1;

                if (HaveEnvironment(EnvironmentTypes.Fertilizer))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_FOOD;

                if (HaveEnvironment(EnvironmentTypes.AdultForest))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_TREE;

                if (HaveEnvironment(EnvironmentTypes.Hill))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

                return amountSteps;
            }
        }

        internal CellEnvironDataCom(Dictionary<EnvironmentTypes, bool> haveCellEnvironments)
        {
            _haveCellEnvironmentOnCell = haveCellEnvironments;
            _amountResourcesOnCell = new Dictionary<EnvironmentTypes, int>();

            for (EnvironmentTypes envirType = (EnvironmentTypes)1; envirType < (EnvironmentTypes)Enum.GetNames(typeof(EnvironmentTypes)).Length; envirType++)
            {
                _haveCellEnvironmentOnCell.Add(envirType, default);
                _amountResourcesOnCell.Add(envirType, default);
            }
        }

        private void SetHaveEnvironment(EnvironmentTypes environmentType, bool haveEnvironment) => _haveCellEnvironmentOnCell[environmentType] = haveEnvironment;

        internal bool HaveEnvironment(EnvironmentTypes environmentType) => _haveCellEnvironmentOnCell[environmentType];
        internal void SetEnvironment(EnvironmentTypes environmentType) => SetHaveEnvironment(environmentType, true);
        internal void ResetEnvironment(EnvironmentTypes environmentType) => SetHaveEnvironment(environmentType, default);



        internal int GetAmountResources(EnvironmentTypes environmentType) => _amountResourcesOnCell[environmentType];
        internal void SetAmountResources(EnvironmentTypes environmentType, int value) => _amountResourcesOnCell[environmentType] = value;

        internal void AddAmountResources(EnvironmentTypes environmentType, int adding = 1) => SetAmountResources(environmentType, GetAmountResources(environmentType) + adding);
        internal void TakeAmountResources(EnvironmentTypes environmentType, int taking = 1) => SetAmountResources(environmentType, GetAmountResources(environmentType) - taking);

        internal bool HaveResources(EnvironmentTypes environmentType) => GetAmountResources(environmentType) > 0;
        internal int MaxAmountResources(EnvironmentTypes environmentTypes)
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
        internal int MinAmountResources(EnvironmentTypes environmentTypes)
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


        internal void SetNewEnvironment(EnvironmentTypes environmentType)
        {
            SetEnvironment(environmentType);

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

            SetAmountResources(environmentType, randAmountResour);
        }


        internal int PowerProtection(UnitTypes unitType)
        {
            var powerProtection = 0;

            switch (unitType)
            {
                case UnitTypes.King:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.Rook:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_ROOK;

                    if (HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_ROOK;
                    break;


                case UnitTypes.Bishop:
                    if (HaveEnvironment(EnvironmentTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

                    if (HaveEnvironment(EnvironmentTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_BISHOP;

                    if (HaveEnvironment(EnvironmentTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_BISHOP;
                    break;
            }


            return powerProtection;
        }
    }
}
