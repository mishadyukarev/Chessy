using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

internal struct CellUnitDataComponent
{
    private Dictionary<bool, bool> _isVisibleDict;
    internal bool IsVisibleUnit(bool key) => _isVisibleDict[key];
    internal void SetIsVisibleUnit(bool key, bool value) => _isVisibleDict[key] = value;


    internal ConditionUnitTypes ConditionType { get; set; }
    internal void ResetConditionType() => ConditionType = default;
    internal bool IsConditionType(ConditionUnitTypes conditionUnitType) => ConditionType == conditionUnitType;




    internal Dictionary<ConditionUnitTypes, int> _amountStepsInCondition;
    internal int AmountStepsInProtectRelax(ConditionUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType];
    internal void SetAmountStepsInProtectRelax(ConditionUnitTypes conditionUnitType, int value) => _amountStepsInCondition[conditionUnitType] = value;
    internal void AddAmountStepsInProtectRelax(ConditionUnitTypes conditionUnitType, int adding = 1) => _amountStepsInCondition[conditionUnitType] += adding;
    internal void TakeAmountStepsInProtectRelax(ConditionUnitTypes conditionUnitType, int taking = 1) => _amountStepsInCondition[conditionUnitType] += taking;

    internal void ResetAmountStepsInProtectRelax(ConditionUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType] = default;




    internal int AmountSteps { get; set; }
    internal void AddAmountSteps(int adding = 1) => AmountSteps += adding;
    internal void TakeAmountSteps(int taking = 1) => AmountSteps -= taking;

    internal bool HaveMaxAmountSteps
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return AmountSteps == STANDART_AMOUNT_STEPS_KING;

                case UnitTypes.Pawn:
                    return AmountSteps == STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.PawnSword:
                    return AmountSteps == STANDART_AMOUNT_STEPS_PAWN_SWORD;

                case UnitTypes.Rook:
                    return AmountSteps == STANDART_AMOUNT_STEPS_ROOK;

                case UnitTypes.RookCrossbow:
                    return AmountSteps == STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return AmountSteps == STANDART_AMOUNT_STEPS_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return AmountSteps == STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;

                default:
                    throw new Exception();
            }
        }
    }
    internal bool HaveMinAmountSteps => AmountSteps > 0;
    internal void ResetAmountSteps() => AmountSteps = default;
    internal void RefreshAmountSteps()
    {
        switch (UnitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                AmountSteps = STANDART_AMOUNT_STEPS_KING;
                break;

            case UnitTypes.Pawn:
                AmountSteps = STANDART_AMOUNT_STEPS_PAWN;
                break;

            case UnitTypes.PawnSword:
                AmountSteps = STANDART_AMOUNT_STEPS_PAWN_SWORD;
                break;

            case UnitTypes.Rook:
                AmountSteps = STANDART_AMOUNT_STEPS_ROOK;
                break;

            case UnitTypes.RookCrossbow:
                AmountSteps = STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;
                break;

            case UnitTypes.Bishop:
                AmountSteps = STANDART_AMOUNT_STEPS_BISHOP;
                break;

            case UnitTypes.BishopCrossbow:
                AmountSteps = STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
                break;

            default:
                throw new Exception();
        }
    }



    internal UnitTypes UnitType { get; set; }
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal bool IsUnitType(UnitTypes unitType) => UnitType == unitType;
    internal bool IsUnitType(UnitTypes[] unitTypes)
    {
        foreach (var curUnitType in unitTypes) if (IsUnitType(curUnitType)) return true;
        return false;
    }
    internal bool IsMelee
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    return true;

                case UnitTypes.Pawn:
                    return true;

                case UnitTypes.PawnSword:
                    return true;

                case UnitTypes.Rook:
                    return false;

                case UnitTypes.RookCrossbow:
                    return false;

                case UnitTypes.Bishop:
                    return false;

                case UnitTypes.BishopCrossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }
    }


    internal int AmountHealth { get; set; }
    internal void AddAmountHealth(int adding = 1) => AmountHealth += adding;
    internal void TakeAmountHealth(int taking = 1) => AmountHealth -= taking;

    internal int MaxAmountHealth
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return STANDART_AMOUNT_HEALTH_KING;

                case UnitTypes.Pawn:
                    return STANDART_AMOUNT_HEALTH_PAWN;

                case UnitTypes.PawnSword:
                    return STANDART_AMOUNT_HEALTH_PAWN_SWORD;

                case UnitTypes.Rook:
                    return STANDART_AMOUNT_HEALTH_ROOK;

                case UnitTypes.RookCrossbow:
                    return STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return STANDART_AMOUNT_HEALTH_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;

                default:
                    return default;
            }
        }
    }
    internal bool HaveMaxAmountHealth => AmountHealth >= MaxAmountHealth;
    internal bool HaveAmountHealth => AmountHealth > 0;
    internal void AddStandartHeal()
    {
        switch (UnitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_KING * PERCENT_FOR_HEALTH_KING));
                break;

            case UnitTypes.Pawn:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_PAWN * PERCENT_FOR_HEALTH_PAWN));
                break;

            case UnitTypes.PawnSword:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_PAWN_SWORD * PERCENT_FOR_HEALTH_PAWN_SWORD));
                break;

            case UnitTypes.Rook:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_ROOK * PERCENT_FOR_HEALTH_ROOK));
                break;

            case UnitTypes.RookCrossbow:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW * PERCENT_FOR_HEALTH_ROOK_CROSSBOW));
                break;

            case UnitTypes.Bishop:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_BISHOP * PERCENT_FOR_HEALTH_BISHOP));
                break;

            case UnitTypes.BishopCrossbow:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW * PERCENT_FOR_HEALTH_BISHOP_CROSSBOW));
                break;

            default:
                throw new Exception();
        }
    }



    internal int SimplePowerDamage
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SIMPLE_POWER_DAMAGE_KING;

                case UnitTypes.Pawn:
                    return SIMPLE_POWER_DAMAGE_PAWN;

                case UnitTypes.PawnSword:
                    return SIMPLE_POWER_DAMAGE_PAWN_SWORD;

                case UnitTypes.Rook:
                    return SIMPLE_POWER_DAMAGE_ROOK;

                case UnitTypes.RookCrossbow:
                    return SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW;

                case UnitTypes.Bishop:
                    return SIMPLE_POWER_DAMAGE_BISHOP;

                case UnitTypes.BishopCrossbow:
                    return SIMPLE_POWER_DAMAGE_BISHOP_CROSSBOW;

                default:
                    return default;
            }
        }
    }
    internal int UniquePowerDamage
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    return default;

                case UnitTypes.King:
                    return SimplePowerDamage;

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.PawnSword:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_PAWN_SWORD);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.RookCrossbow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_BISHOP);

                case UnitTypes.BishopCrossbow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW);

                default:
                    return default;
            }
        }
    }
    //internal int PowerProtection
    //{
    //    get
    //    {
    //        int powerProtection = 0;

    //        if (IsConditionType(ConditionUnitTypes.Protected))
    //        {
    //            switch (UnitType)
    //            {
    //                case UnitTypes.King:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
    //                    break;

    //                case UnitTypes.Pawn:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
    //                    break;

    //                case UnitTypes.PawnSword:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN_SWORD);
    //                    break;

    //                case UnitTypes.Rook:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
    //                    break;

    //                case UnitTypes.RookCrossbow:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
    //                    break;

    //                case UnitTypes.Bishop:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
    //                    break;

    //                case UnitTypes.BishopCrossbow:
    //                    powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
    //                    break;
    //            }
    //        }

    //        else if (IsConditionType(ConditionUnitTypes.Relaxed))
    //        {
    //            switch (UnitType)
    //            {
    //                case UnitTypes.King:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
    //                    break;

    //                case UnitTypes.Pawn:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
    //                    break;

    //                case UnitTypes.PawnSword:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN_SWORD);
    //                    break;

    //                case UnitTypes.Rook:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
    //                    break;

    //                case UnitTypes.RookCrossbow:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
    //                    break;

    //                case UnitTypes.Bishop:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
    //                    break;

    //                case UnitTypes.BishopCrossbow:
    //                    powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
    //                    break;
    //            }
    //        }


    //        switch (UnitType)
    //        {
    //            case UnitTypes.King:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_KING;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_KING;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_KING;
    //                break;

    //            case UnitTypes.Pawn:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_PAWN;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_PAWN;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_PAWN;
    //                break;


    //            case UnitTypes.PawnSword:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_PAWN_SWORD;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_PAWN_SWORD;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_PAWN_SWORD;
    //                break;


    //            case UnitTypes.Rook:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_ROOK;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_ROOK;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_ROOK;
    //                break;


    //            case UnitTypes.RookCrossbow:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_ROOK_CROSSBOW;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_ROOK_CROSSBOW;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_ROOK_CROSSBOW;
    //                break;


    //            case UnitTypes.Bishop:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_BISHOP;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_BISHOP;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_BISHOP;
    //                break;


    //            case UnitTypes.BishopCrossbow:
    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy))
    //                    powerProtection -= PROTECTION_FOOD_FOR_BISHOP_CROSSBOW;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
    //                    powerProtection += PROTECTION_TREE_FOR_BISHOP_CROSSBOW;

    //                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Hill, xy))
    //                    powerProtection += PROTECTION_HILL_FOR_BISHOP_CROSSBOW;
    //                break;
    //        }

    //        switch (CellBuildDataSystem.BuildTypeCom(xy).BuildingType)
    //        {
    //            case BuildingTypes.City:

    //                switch (UnitType)
    //                {
    //                    case UnitTypes.King:
    //                        powerProtection += PROTECTION_CITY_KING;
    //                        break;

    //                    case UnitTypes.Pawn:
    //                        powerProtection += PROTECTION_CITY_PAWN;
    //                        break;

    //                    case UnitTypes.PawnSword:
    //                        powerProtection += PROTECTION_CITY_PAWN_SWORD;
    //                        break;

    //                    case UnitTypes.Rook:
    //                        powerProtection += PROTECTION_CITY_ROOK;
    //                        break;

    //                    case UnitTypes.RookCrossbow:
    //                        powerProtection += PROTECTION_CITY_ROOK_CROSSBOW;
    //                        break;

    //                    case UnitTypes.Bishop:
    //                        powerProtection += PROTECTION_CITY_BISHOP;
    //                        break;

    //                    case UnitTypes.BishopCrossbow:
    //                        powerProtection += PROTECTION_CITY_BISHOP_CROSSBOW;
    //                        break;
    //                }

    //                break;

    //            case BuildingTypes.Farm:
    //                powerProtection += 5;
    //                break;

    //            case BuildingTypes.Woodcutter:
    //                powerProtection += 5;
    //                break;

    //            case BuildingTypes.Mine:
    //                break;
    //        }

    //        return powerProtection;

    //    }
    //}



    internal void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType)
    {
        UnitType = unitType;
        AmountHealth = amountHealth;
        AmountSteps = amountSteps;
        ConditionType = protectRelaxType;
    }
    internal void ResetStandartValuesUnit()
    {
        UnitTypes unitType = default;
        int amountHealth = default;
        int amountSteps = default;
        ConditionUnitTypes protectRelaxType = default;

        SetStandartValuesUnit(unitType, amountHealth, amountSteps, protectRelaxType);
    }
    internal void ChangePlayerUnit(UnitTypes newUnitType)
    {
        UnitType = newUnitType;

        switch (UnitType)
        {
            case UnitTypes.None:
                throw new Exception();

            case UnitTypes.King:
                throw new Exception();

            case UnitTypes.Pawn:
                break;

            case UnitTypes.PawnSword:
                AddAmountHealth(STANDART_AMOUNT_HEALTH_PAWN_SWORD - STANDART_AMOUNT_HEALTH_PAWN);
                break;

            case UnitTypes.Rook:
                break;

            case UnitTypes.RookCrossbow:
                AddAmountHealth(STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW - STANDART_AMOUNT_HEALTH_ROOK);
                break;

            case UnitTypes.Bishop:
                break;

            case UnitTypes.BishopCrossbow:
                AddAmountHealth(STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW - STANDART_AMOUNT_HEALTH_BISHOP);
                break;

            default:
                throw new Exception();
        }
    }


    internal CellUnitDataComponent(Dictionary<bool, bool> isVisibleDict) : this()
    {
        _isVisibleDict = isVisibleDict;

        _isVisibleDict.Add(true, default);
        _isVisibleDict.Add(false, default);


        _amountStepsInCondition = new Dictionary<ConditionUnitTypes, int>();
        _amountStepsInCondition.Add(ConditionUnitTypes.None, default);
        _amountStepsInCondition.Add(ConditionUnitTypes.Protected, default);
        _amountStepsInCondition.Add(ConditionUnitTypes.Relaxed, default);
    }
}

