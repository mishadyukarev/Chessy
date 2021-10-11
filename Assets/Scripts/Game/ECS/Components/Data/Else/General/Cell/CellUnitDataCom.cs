using System;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct CellUnitDataCom
    {
        internal UnitTypes UnitType;
        internal bool HaveUnit => UnitType != UnitTypes.None;
        internal void DefUnitType() => UnitType = default;
        internal bool Is(UnitTypes unitType) => UnitType.Is(unitType);
        internal bool Is(UnitTypes[] unitTypes) => UnitType.Is(unitTypes);
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


        private Dictionary<PlayerTypes, bool> _isVisibleDict;
        internal bool IsVisibleUnit(PlayerTypes key) => _isVisibleDict[key];
        internal void SetIsVisibleUnit(PlayerTypes key, bool value) => _isVisibleDict[key] = value;


        internal CondUnitTypes CondUnitType { get; set; }
        internal void ResetCondType() => CondUnitType = default;
        internal bool Is(CondUnitTypes conditionUnitType) => CondUnitType == conditionUnitType;
        internal bool Is(CondUnitTypes[] conditionUnitTypes)
        {
            foreach (var conditionUnitType in conditionUnitTypes)
                if (Is(conditionUnitType)) return true;
            return false;
        }


        internal ToolWeaponTypes ArcherWeapType { get; set; }
        internal bool HaveArcherWeapon => ArcherWeapType != default;

        internal ToolWeaponTypes ExtraTWPawnType { get; set; }
        internal bool HaveExtraToolWeaponPawn => ExtraTWPawnType != default;


        private Dictionary<CondUnitTypes, int> _amountStepsInCondition;
        internal int AmountStepsInProtectRelax(CondUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType];
        internal void SetAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int value) => _amountStepsInCondition[conditionUnitType] = value;
        internal void AddAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int adding = 1) => _amountStepsInCondition[conditionUnitType] += adding;
        internal void TakeAmountStepsInProtectRelax(CondUnitTypes conditionUnitType, int taking = 1) => _amountStepsInCondition[conditionUnitType] += taking;

        internal void ResetAmountStepsInProtectRelax(CondUnitTypes conditionUnitType) => _amountStepsInCondition[conditionUnitType] = default;


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
                        return AmountSteps == UnitValues.STANDART_AMOUNT_STEPS_KING;

                    case UnitTypes.Pawn:
                        return AmountSteps == UnitValues.STANDART_AMOUNT_STEPS_PAWN;

                    case UnitTypes.Rook:
                        return AmountSteps == UnitValues.STANDART_AMOUNT_STEPS_ROOK;

                    case UnitTypes.Bishop:
                        return AmountSteps == UnitValues.STANDART_AMOUNT_STEPS_BISHOP;

                    default:
                        throw new Exception();
                }
            }
        }
        internal bool HaveMinAmountSteps => AmountSteps >= 1;
        internal void ResetAmountSteps() => AmountSteps = default;
        internal void RefreshAmountSteps()
        {
            switch (UnitType)
            {
                case UnitTypes.None:
                    throw new Exception();

                case UnitTypes.King:
                    AmountSteps = UnitValues.STANDART_AMOUNT_STEPS_KING;
                    break;

                case UnitTypes.Pawn:
                    AmountSteps = UnitValues.STANDART_AMOUNT_STEPS_PAWN;
                    break;

                case UnitTypes.Rook:
                    AmountSteps = UnitValues.STANDART_AMOUNT_STEPS_ROOK;
                    break;

                case UnitTypes.Bishop:
                    AmountSteps = UnitValues.STANDART_AMOUNT_STEPS_BISHOP;
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
                        return UnitValues.STANDART_AMOUNT_HEALTH_KING;

                    case UnitTypes.Pawn:
                        return UnitValues.STANDART_AMOUNT_HEALTH_PAWN;

                    case UnitTypes.Rook:
                        return UnitValues.STANDART_AMOUNT_HEALTH_ROOK;

                    case UnitTypes.Bishop:
                        return UnitValues.STANDART_AMOUNT_HEALTH_BISHOP;

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
                    AddAmountHealth(UnitValues.FOR_ADD_HEALTH_KING);
                    break;

                case UnitTypes.Pawn:
                    AddAmountHealth(UnitValues.FOR_ADD_HEALTH_PAWN);
                    break;

                case UnitTypes.Rook:
                    AddAmountHealth(UnitValues.FOR_ADD_HEALTH_ROOK);
                    break;

                case UnitTypes.Bishop:
                    AddAmountHealth(UnitValues.FOR_ADD_HEALTH_BISHOP);
                    break;

                default:
                    throw new Exception();
            }
        }



        internal int PowerProtection
        {
            get
            {
                int powerProtection = 0;

                if (Is(CondUnitTypes.Protected))
                {
                    switch (UnitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            powerProtection += (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_KING);
                            break;

                        case UnitTypes.Pawn:
                            powerProtection += (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_PAWN);
                            break;

                        case UnitTypes.Rook:
                            powerProtection += (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_ROOK);
                            break;

                        case UnitTypes.Bishop:
                            powerProtection += (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_BISHOP);
                            break;

                        default:
                            throw new Exception();
                    }
                }

                else if (Is(CondUnitTypes.Relaxed))
                {
                    switch (UnitType)
                    {
                        case UnitTypes.None:
                            throw new Exception();

                        case UnitTypes.King:
                            powerProtection -= (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_KING);
                            break;

                        case UnitTypes.Pawn:
                            powerProtection -= (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_PAWN);
                            break;

                        case UnitTypes.Rook:
                            powerProtection -= (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_ROOK);
                            break;

                        case UnitTypes.Bishop:
                            powerProtection -= (int)(SimplePowerDamage * UnitValues.PERCENT_FOR_PROTECTION_BISHOP);
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
                    simplePowerDamege = UnitValues.SIMPLE_POWER_DAMAGE_KING;
                }

                else if (UnitType.Is(UnitTypes.Pawn))
                {
                    simplePowerDamege = UnitValues.SIMPLE_POWER_DAMAGE_PAWN;

                    switch (ExtraTWPawnType)
                    {
                        case ToolWeaponTypes.None:
                            break;

                        case ToolWeaponTypes.Hoe:
                            throw new Exception();

                        case ToolWeaponTypes.Axe:
                            throw new Exception();

                        case ToolWeaponTypes.Pick:
                            simplePowerDamege -= simplePowerDamege * 0.5f;
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
                    simplePowerDamege = UnitValues.SIMPLE_POWER_DAMAGE_ROOK_AND_BISHOP;

                    switch (ArcherWeapType)
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
                            simplePowerDamege += simplePowerDamege / 3;
                            break;

                        default:
                            throw new Exception();
                    }

                    switch (ExtraTWPawnType)
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


        internal void ResetUnit()
        {
            UnitType = default;
            ArcherWeapType = default;
            ExtraTWPawnType = default;
            AmountHealth = default;
            AmountSteps = default;
            CondUnitType = default;
        }
        internal void ReplaceUnit(CellUnitDataCom newCellUnitDataCom)
        {
            UnitType = newCellUnitDataCom.UnitType;
            ArcherWeapType = newCellUnitDataCom.ArcherWeapType;
            ExtraTWPawnType = newCellUnitDataCom.ExtraTWPawnType;
            AmountHealth = newCellUnitDataCom.AmountHealth;
            AmountSteps = newCellUnitDataCom.AmountSteps;
            CondUnitType = newCellUnitDataCom.CondUnitType;
        }

        internal CellUnitDataCom(bool needNew) : this()
        {
            if (needNew)
            {
                _isVisibleDict = new Dictionary<PlayerTypes, bool>();

                _isVisibleDict.Add(PlayerTypes.First, default);
                _isVisibleDict.Add(PlayerTypes.Second, default);


                _amountStepsInCondition = new Dictionary<CondUnitTypes, int>();
                _amountStepsInCondition.Add(CondUnitTypes.None, default);
                _amountStepsInCondition.Add(CondUnitTypes.Protected, default);
                _amountStepsInCondition.Add(CondUnitTypes.Relaxed, default);
            }
        }
    }
}
