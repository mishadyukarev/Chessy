using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.Supports;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConsts.UnitValues;

internal struct CellUnitDataComponent
{
    internal UnitTypes UnitType;
    internal bool HaveUnit => UnitType != UnitTypes.None;
    internal void ResetUnitType() => UnitType = default;
    internal bool IsUnitType(UnitTypes unitType) => UnitType.Is(unitType);
    internal bool IsUnitType(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
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

                case UnitTypes.Rook:
                    return false;

                case UnitTypes.Bishop:
                    return false;

                default:
                    throw new Exception();
            }
        }
    }


    private Dictionary<bool, bool> _isVisibleDict;
    internal bool IsVisibleUnit(bool key) => _isVisibleDict[key];
    internal void SetIsVisibleUnit(bool key, bool value) => _isVisibleDict[key] = value;


    internal ConditionUnitTypes ConditionUnitType { get; set; }
    internal void ResetConditionType() => ConditionUnitType = default;
    internal bool IsConditionType(ConditionUnitTypes conditionUnitType) => ConditionUnitType == conditionUnitType;
    internal bool IsConditionType(ConditionUnitTypes[] conditionUnitTypes)
    {
        foreach (var conditionUnitType in conditionUnitTypes)
            if (IsConditionType(conditionUnitType)) return true;
        return false;
    }


    internal ToolWeaponTypes MainToolWeaponType { get; set; }
    internal bool HaveMainToolWeapon => MainToolWeaponType != default;

    internal ToolWeaponTypes ExtraToolWeaponType { get; set; }
    internal bool HaveExtraToolWeapon => ExtraToolWeaponType != default;


    private Dictionary<ConditionUnitTypes, int> _amountStepsInCondition;
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

                case UnitTypes.Rook:
                    return AmountSteps == STANDART_AMOUNT_STEPS_ROOK_BOW;

                case UnitTypes.Bishop:
                    return AmountSteps == STANDART_AMOUNT_STEPS_BISHOP_BOW;

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

            case UnitTypes.Rook:
                AmountSteps = STANDART_AMOUNT_STEPS_ROOK_BOW;
                break;

            case UnitTypes.Bishop:
                AmountSteps = STANDART_AMOUNT_STEPS_BISHOP_BOW;
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

                case UnitTypes.Pawn:
                    return STANDART_AMOUNT_HEALTH_PAWN_AXE;

                case UnitTypes.Rook:
                    return STANDART_AMOUNT_HEALTH_ROOK_BOW;

                case UnitTypes.Bishop:
                    return STANDART_AMOUNT_HEALTH_BISHOP_BOW;

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
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_PAWN_AXE * PERCENT_FOR_HEALTH_PAWN_AXE));
                break;

            case UnitTypes.Rook:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_ROOK_BOW * PERCENT_FOR_HEALTH_ROOK_BOW));
                break;

            case UnitTypes.Bishop:
                AddAmountHealth((int)(STANDART_AMOUNT_HEALTH_BISHOP_BOW * PERCENT_FOR_HEALTH_BISHOP_BOW));
                break;

            default:
                throw new Exception();
        }
    }


    internal int UniquePowerDamage
    {
        get
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    throw new Exception();

                case UnitTypes.Pawn:
                    return (int)(SimplePowerDamage * 0.5f);

                case UnitTypes.Rook:
                    return (int)(SimplePowerDamage * 0.5f);

                case UnitTypes.Bishop:
                    return (int)(SimplePowerDamage * 0.5f);

                default:
                    throw new Exception();
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
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection += (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    default: 
                        throw new Exception();
                }
            }

            else if (IsConditionType(ConditionUnitTypes.Relaxed))
            {
                switch (UnitType)
                {
                    case UnitTypes.None:
                        throw new Exception();

                    case UnitTypes.King:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_KING);
                        break;

                    case UnitTypes.Pawn:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_PAWN);
                        break;

                    case UnitTypes.Rook:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_ROOK);
                        break;

                    case UnitTypes.Bishop:
                        powerProtection -= (int)(SimplePowerDamage * PERCENT_FOR_PROTECTION_BISHOP);
                        break;

                    default:
                        throw new Exception();
                }
            }

            return powerProtection;
        }
    }

    internal int SimplePowerDamage
    {
        get
        {
            float simplePowerDamege = 0;

            if (UnitType.Is(UnitTypes.King))
            {
                simplePowerDamege = SIMPLE_POWER_DAMAGE_KING;
            }

            else if (UnitType.Is(UnitTypes.Pawn))
            {
                simplePowerDamege = SIMPLE_POWER_DAMAGE_PAWN;

                switch (MainToolWeaponType)
                {
                    case ToolWeaponTypes.None:
                        throw new Exception();

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Axe:
                        simplePowerDamege += 0;
                        break;

                    case ToolWeaponTypes.Pick:
                        throw new Exception();

                    case ToolWeaponTypes.Sword:
                        throw new Exception();

                    case ToolWeaponTypes.Bow:
                        throw new Exception();

                    case ToolWeaponTypes.Crossbow:
                        throw new Exception();

                    default:
                        throw new Exception();
                }

                switch (ExtraToolWeaponType)
                {
                    case ToolWeaponTypes.None:
                        break;

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Axe:
                        throw new Exception();

                    case ToolWeaponTypes.Pick:
                        simplePowerDamege -= simplePowerDamege * 0.2f;
                        break;

                    case ToolWeaponTypes.Sword:
                        simplePowerDamege += simplePowerDamege * 0.5f;
                        break;

                    case ToolWeaponTypes.Bow:
                        throw new Exception();

                    case ToolWeaponTypes.Crossbow:
                        throw new Exception();

                    default:
                        throw new Exception();
                }
            }

            else if (UnitType.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
            {
                simplePowerDamege = SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP;

                switch (MainToolWeaponType)
                {
                    case ToolWeaponTypes.None:
                        throw new Exception();

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Axe:
                        throw new Exception();

                    case ToolWeaponTypes.Pick:
                        throw new Exception();

                    case ToolWeaponTypes.Sword:
                        throw new Exception();

                    case ToolWeaponTypes.Bow:
                        simplePowerDamege += 0;
                        break;

                    case ToolWeaponTypes.Crossbow:
                        simplePowerDamege += simplePowerDamege;
                        break;

                    default:
                        throw new Exception();
                }

                switch (ExtraToolWeaponType)
                {
                    case ToolWeaponTypes.None:
                        break;

                    case ToolWeaponTypes.Hoe:
                        throw new Exception();

                    case ToolWeaponTypes.Axe:
                        throw new Exception();

                    case ToolWeaponTypes.Pick:
                        throw new Exception();

                    case ToolWeaponTypes.Sword:
                        throw new Exception();

                    case ToolWeaponTypes.Bow:
                        throw new Exception();

                    case ToolWeaponTypes.Crossbow:
                        throw new Exception();

                    default:
                        throw new Exception();
                }
            }

            return (int)simplePowerDamege;
        }
    }


    internal void ResetUnit()
    {
        UnitType = default;
        MainToolWeaponType = default;
        ExtraToolWeaponType = default;
        AmountHealth = default;
        AmountSteps = default;
        ConditionUnitType = default;
    }
    internal void ReplaceUnit(CellUnitDataComponent newCellUnitDataCom)
    {
        UnitType = newCellUnitDataCom.UnitType;
        ConditionUnitType = newCellUnitDataCom.ConditionUnitType;
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

