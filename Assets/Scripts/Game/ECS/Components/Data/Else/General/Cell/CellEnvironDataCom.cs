using Assets.Scripts.Abstractions.ValuesConsts;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.EnvironmentValues;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

namespace Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell
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

                if (HaveEnvir(EnvirTypes.Fertilizer))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_FERTILIZE;

                if (HaveEnvir(EnvirTypes.AdultForest))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_ADULTTREE;

                if (HaveEnvir(EnvirTypes.Hill))
                    amountSteps += UnitValues.NEED_AMOUNT_STEPS_HILL;

                return amountSteps;
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

        internal bool HaveEnvir(EnvirTypes environmentType) => _haveCellEnvirOnCell[environmentType];
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
                    return MAX_AMOUNT_FOOD;

                case EnvirTypes.YoungForest:
                    throw new Exception();

                case EnvirTypes.AdultForest:
                    return MAX_AMOUNT_FOREST;

                case EnvirTypes.Hill:
                    return MAX_AMOUNT_ORE;

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
                    return MIN_AMOUNT_FOOD;

                case EnvirTypes.YoungForest:
                    throw new Exception();

                case EnvirTypes.AdultForest:
                    return MIN_AMOUNT_WOOD;

                case EnvirTypes.Hill:
                    return MIN_AMOUNT_ORE;

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


        internal int PowerProtectionUnit(UnitTypes unitType)
        {
            var powerProtection = 0;

            switch (unitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    if (HaveEnvir(EnvirTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_KING;

                    if (HaveEnvir(EnvirTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_KING;

                    if (HaveEnvir(EnvirTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_KING;
                    break;

                case UnitTypes.Pawn:
                    if (HaveEnvir(EnvirTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_PAWN;

                    if (HaveEnvir(EnvirTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_PAWN;

                    if (HaveEnvir(EnvirTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_PAWN;
                    break;


                case UnitTypes.Rook:
                    if (HaveEnvir(EnvirTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_AND_BISHOP;

                    if (HaveEnvir(EnvirTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_AND_BISHOP;

                    if (HaveEnvir(EnvirTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_AND_BISHOP;
                    break;


                case UnitTypes.Bishop:
                    if (HaveEnvir(EnvirTypes.Fertilizer))
                        powerProtection -= PROTECTION_FOOD_FOR_ROOK_AND_BISHOP;

                    if (HaveEnvir(EnvirTypes.AdultForest))
                        powerProtection += PROTECTION_TREE_FOR_ROOK_AND_BISHOP;

                    if (HaveEnvir(EnvirTypes.Hill))
                        powerProtection += PROTECTION_HILL_FOR_ROOK_AND_BISHOP;

                    break;
                    throw new Exception();
            }


            return powerProtection;
        }
    }
}
