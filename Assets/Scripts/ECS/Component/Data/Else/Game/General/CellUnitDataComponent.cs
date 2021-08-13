using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

internal struct CellUnitDataComponent
{
    internal UnitTypes UnitType;
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal void ResetUnitType() => UnitType = default;
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

                case UnitTypes.Pawn_Axe:
                    return true;

                case UnitTypes.Rook_Bow:
                    return false;

                case UnitTypes.Rook_Crossbow:
                    return false;

                case UnitTypes.Bishop_Bow:
                    return false;

                case UnitTypes.Bishop_Crossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }
    }


    internal PawnToolTypes ExtraPawnToolType;
    internal bool HaveExtraPawnTool => ExtraPawnToolType != PawnToolTypes.None;
    internal bool IsPawnSecondTool(PawnToolTypes pawnSecondToolType) => ExtraPawnToolType == pawnSecondToolType;
    internal void ResetPawnSecondTool() => ExtraPawnToolType = default;


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

                case UnitTypes.Pawn_Axe:
                    return AmountSteps == STANDART_AMOUNT_STEPS_PAWN;

                case UnitTypes.Rook_Bow:
                    return AmountSteps == STANDART_AMOUNT_STEPS_ROOK_BOW;

                case UnitTypes.Rook_Crossbow:
                    return AmountSteps == STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;

                case UnitTypes.Bishop_Bow:
                    return AmountSteps == STANDART_AMOUNT_STEPS_BISHOP_BOW;

                case UnitTypes.Bishop_Crossbow:
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

            case UnitTypes.Pawn_Axe:
                AmountSteps = STANDART_AMOUNT_STEPS_PAWN;
                break;

            case UnitTypes.Rook_Bow:
                AmountSteps = STANDART_AMOUNT_STEPS_ROOK_BOW;
                break;

            case UnitTypes.Rook_Crossbow:
                AmountSteps = STANDART_AMOUNT_STEPS_ROOK_CROSSBOW;
                break;

            case UnitTypes.Bishop_Bow:
                AmountSteps = STANDART_AMOUNT_STEPS_BISHOP_BOW;
                break;

            case UnitTypes.Bishop_Crossbow:
                AmountSteps = STANDART_AMOUNT_STEPS_BISHOP_CROSSBOW;
                break;

            default:
                throw new Exception();
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

                case UnitTypes.Pawn_Axe:
                    return STANDART_AMOUNT_HEALTH_PAWN_AXE;

                case UnitTypes.Rook_Bow:
                    return STANDART_AMOUNT_HEALTH_ROOK_BOW;

                case UnitTypes.Rook_Crossbow:
                    return STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;

                case UnitTypes.Bishop_Bow:
                    return STANDART_AMOUNT_HEALTH_BISHOP_BOW;

                case UnitTypes.Bishop_Crossbow:
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

            case UnitTypes.Pawn_Axe:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_PAWN_AXE * PERCENT_FOR_HEALTH_PAWN_AXE));
                break;

            case UnitTypes.Rook_Bow:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_ROOK_BOW * PERCENT_FOR_HEALTH_ROOK_BOW));
                break;

            case UnitTypes.Rook_Crossbow:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW * PERCENT_FOR_HEALTH_ROOK_CROSSBOW));
                break;

            case UnitTypes.Bishop_Bow:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_BISHOP_BOW * PERCENT_FOR_HEALTH_BISHOP_BOW));
                break;

            case UnitTypes.Bishop_Crossbow:
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

                case UnitTypes.Pawn_Axe:
                    return SIMPLE_POWER_DAMAGE_PAWN;

                case UnitTypes.Rook_Bow:
                    return SIMPLE_POWER_DAMAGE_ROOK;

                case UnitTypes.Rook_Crossbow:
                    return SIMPLE_POWER_DAMAGE_ROOK_CROSSBOW;

                case UnitTypes.Bishop_Bow:
                    return SIMPLE_POWER_DAMAGE_BISHOP;

                case UnitTypes.Bishop_Crossbow:
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

                case UnitTypes.Pawn_Axe:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_PAWN);

                case UnitTypes.Rook_Bow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_ROOK);

                case UnitTypes.Rook_Crossbow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_ROOK_CROSSBOW);

                case UnitTypes.Bishop_Bow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_BISHOP_BOW);

                case UnitTypes.Bishop_Crossbow:
                    return (int)(SimplePowerDamage * RATION_UNIQUE_POWER_DAMAGE_BISHOP_CROSSBOW);

                default:
                    return default;
            }
        }
    }
    internal int PowerProtection
    {
        get
        {
            int powerProtection = 0;

            if (IsConditionType(ConditionUnitTypes.Protected))
            {
                switch (UnitType)
                {
                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn_Axe:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook_Bow:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Rook_Crossbow:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop_Bow:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.Bishop_Crossbow:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            else if (IsConditionType(ConditionUnitTypes.Relaxed))
            {
                switch (UnitType)
                {
                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn_Axe:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook_Bow:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Rook_Crossbow:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK_CROSSBOW);
                        break;

                    case UnitTypes.Bishop_Bow:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    case UnitTypes.Bishop_Crossbow:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP_CROSSBOW);
                        break;
                }
            }

            return powerProtection;
        }
    }


    internal void SetStandartValuesUnit(UnitTypes unitType, int amountHealth, int amountSteps, ConditionUnitTypes protectRelaxType)
    {
        UnitType = unitType;
        AmountHealth = amountHealth;
        AmountSteps = amountSteps;
        ConditionType = protectRelaxType;
    }
    internal void ResetUnit()
    {
        UnitType = default;
        AmountHealth = default;
        AmountSteps = default;
        ConditionType = default;
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

            case UnitTypes.Pawn_Axe:
                throw new Exception();

            case UnitTypes.Rook_Bow:
                throw new Exception();

            case UnitTypes.Rook_Crossbow:
                AmountHealth += MaxAmountHealth - STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;
                break;

            case UnitTypes.Bishop_Bow:
                throw new Exception();

            case UnitTypes.Bishop_Crossbow:
                AmountHealth += MaxAmountHealth - STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;
                break;

            default:
                throw new Exception();
        }
    }
    internal void ReplaceUnit(CellUnitDataComponent newCellUnitDataCom)
    {
        UnitType = newCellUnitDataCom.UnitType;
        ConditionType = newCellUnitDataCom.ConditionType;
        AmountSteps = newCellUnitDataCom.AmountSteps;
        AmountHealth = newCellUnitDataCom.AmountHealth;
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

